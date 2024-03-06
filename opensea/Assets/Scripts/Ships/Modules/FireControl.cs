using Assets.Scripts.Helpers;
using UnityEngine;
using UnityEngine.InputSystem;
using Vector3 = UnityEngine.Vector3;

namespace Assets.Scripts.Ships.Modules
{
    [RequireComponent(typeof(Armaments))]
    public class FireControl : ActionModule
    {
        [SerializeField] private Sprite m_targetReticuleSprite;
        [SerializeField] private Sprite m_lockReticuleSprite;

        [SerializeField] private Transform m_targetReticule;
        [SerializeField] private Transform m_projectedReticule;
        [SerializeField] private LayerMask m_obstacleLayer;
        [SerializeField] private LayerMask m_shipLayer;

        private Transform m_attachedShipPosition;
        private Armaments m_armamentsModule;

        private SpriteRenderer m_projectedReticuleImage;
        private Vector3 m_startingReticuleScale;
        private float m_dispersion;
        private float m_range;
        private float m_maxRange;
        private bool m_isAiming;
        private Ship m_tryLockOnShip;

        public override void Initialize(Ship attachedShip)
        {
            base.Initialize(attachedShip);

            m_attachedShipPosition = attachedShip.transform;

            m_range = attachedShip.Stats.RNG;
            m_maxRange = m_range * 2;

            m_armamentsModule = GetComponentInChildren<Armaments>();
            m_startingReticuleScale = m_projectedReticule.localScale;
            m_projectedReticuleImage = m_projectedReticule.GetComponentInChildren<SpriteRenderer>();
        }

        public override void ShipDeselect()
        {
            base.ShipSelect();
            m_isAiming = false;
            m_projectedReticule.gameObject.SetActive(false);
            m_targetReticule.gameObject.SetActive(false);
            Events.Ship.FireIsAiming(m_ship, false);
        }

        public override void ShipSelect()
        {
            base.ShipSelect();
            m_isAiming = true;
            m_projectedReticule.gameObject.SetActive(true);
            m_targetReticule.gameObject.SetActive(true);
            Events.Ship.FireIsAiming(m_ship, true);
        }

        protected override void RegisterActions()
        {
            m_inputActions.BattleMap.SpaceBar.performed += ctx => ToggleAiming();
            m_inputActions.BattleMap.FireCommand.performed += ctx => TryFireSingle();
            m_inputActions.BattleMap.FireCommand.canceled += ctx => TryFireSalvo();
        }

        protected override void InternalPreUpdateModule(float deltaTime)
        {
        }

        protected override void InternalUpdateModule(float deltaTime)
        {
            if (!m_isAiming) return;

            MoveReticule();
            MoveArmaments();
            CheckForEnemyLock();
        }

        private void ToggleAiming()
        {
            if (m_tryLockOnShip is not null)
            {
                m_tryLockOnShip = null;
                m_armamentsModule.LockOnto(m_tryLockOnShip);
                //notif lock on ship unlocked
                Debug.Log("Lock on ship unlocked");
                return;
            }

            m_isAiming = !m_isAiming;

            m_projectedReticule.gameObject.SetActive(m_isAiming);
            m_targetReticule.gameObject.SetActive(m_isAiming);
            Events.Ship.FireIsAiming(m_ship, m_isAiming);
        }

        private void TryFireSingle()
        {
            if (!m_isAiming) return;
            
            if (m_tryLockOnShip)
            {
                m_armamentsModule.LockOnto(m_tryLockOnShip);
            }
            else
            {
                m_armamentsModule.FireNextWeaponAt(m_targetReticule.position);
            }
        }

        private void TryFireSalvo()
        {
            if (!m_isAiming) return;
            
            m_armamentsModule.FireAllWeaponAt(m_targetReticule.position);
        }
        
        private void MoveReticule()
        {
            
            var pointerPos = Helper.PointerPosition;
            m_projectedReticule.position = new Vector3(pointerPos.x, pointerPos.y, 0);
            var pointerDistance = Vector3.Distance(transform.position, m_projectedReticule.position);
            
            m_targetReticule.position = new Vector3(pointerPos.x, pointerPos.y, 0);
            m_targetReticule.localScale = m_startingReticuleScale * Mathf.Clamp(pointerDistance / m_range, 1, 2);
            
            var fromShip = Physics2D.Raycast(transform.position, pointerPos - transform.position, pointerDistance, m_obstacleLayer);
            if (fromShip.collider is not null)
            {
                Debug.DrawRay(transform.position, pointerPos - transform.position, Color.blue);
                m_projectedReticule.position = new Vector3(fromShip.point.x, fromShip.point.y, 0);
            }
            else if (pointerDistance > m_maxRange)
            {
                m_projectedReticule.position = new Ray2D(transform.position, pointerPos - transform.position).GetPoint(m_maxRange);
            }

            var reticuleDistance = Vector3.Distance(transform.position, m_projectedReticule.position);
            m_dispersion = Mathf.Clamp(reticuleDistance / m_range, 1, 2);
            m_projectedReticule.localScale = m_startingReticuleScale * m_dispersion;
        }

        private void MoveArmaments()
        {
            m_armamentsModule.FollowPosition(m_targetReticule.position);
        }

        private void CheckForEnemyLock()
        {
            if (!m_armamentsModule.SelectedWeaponTypeCanLockOn()) return;
            
            var shipHit = Physics2D.OverlapCircle(m_targetReticule.position, 0.1f, m_shipLayer);
            if (shipHit is not null && shipHit.CompareTag("Enemy")) 
            {
                m_tryLockOnShip ??= shipHit.gameObject.GetComponent<Ship>();
                if (m_tryLockOnShip is not null && m_tryLockOnShip.Team == ShipTeam.Enemy && m_tryLockOnShip.Alive)
                    m_projectedReticuleImage.sprite = m_lockReticuleSprite;
                else
                    m_tryLockOnShip = null;
            }
            else
            {
                m_projectedReticuleImage.sprite = m_targetReticuleSprite;
                m_tryLockOnShip = null;
            }
        }
    }
}