using Assets.Scripts.Helpers;
using UnityEngine;
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
        
        private Armaments m_armamentsModule;

        private SpriteRenderer m_projectedReticuleImage;
        private Vector3 m_startingReticuleScale;
        private float m_dispersion;
        private float m_range;
        private bool m_isAiming;
        private Ship m_tryLockOnShip;
        
        public override void Initialize(Ship attachedShip)
        {
            base.Initialize(attachedShip);
            
            m_inputActions.BattleMap.LeftClick.performed += ctx => SetTargetTo();
            
            m_range = attachedShip.Stats.RNG;
            
            m_armamentsModule = GetComponentInChildren<Armaments>();
            m_startingReticuleScale = m_projectedReticule.localScale;
            m_projectedReticuleImage = m_projectedReticule.GetComponentInChildren<SpriteRenderer>();
        }

        public override void ShipDeselect()
        {
            m_isAiming = false;
            m_projectedReticule.gameObject.SetActive(false);
            m_targetReticule.gameObject.SetActive(false);
            Events.Ship.FireIsAiming(m_ship, false);
        }

        //called from ui
        public void ToggleAiming()
        {
            m_isAiming = !m_isAiming;
            m_tryLockOnShip = null;
            
            m_projectedReticule.gameObject.SetActive(m_isAiming);
            m_targetReticule.gameObject.SetActive(m_isAiming);
            Events.Ship.FireIsAiming(m_ship, m_isAiming);
        }

        protected override void InternalPreUpdateModule(float deltaTime)
        {
            
        }

        protected override void InternalUpdateModule(float deltaTime)
        {
            if (!m_isAiming) return;

            MoveReticule();
            CheckForEnemyLock();
        }
        
        private void SetTargetTo()
        {
            //todo-P3 on click lock on this point anim
            
            if (!m_isAiming) return;

            if (m_tryLockOnShip)
            {
                m_armamentsModule.SetTargetTo(m_tryLockOnShip);
            }
            else
            {
                m_armamentsModule.SetTargetTo(m_targetReticule.position);
            }
            
            ToggleAiming();
        }
        
        private void MoveReticule()
        {
            var pointerPos = Helper.PointerPosition;
            m_projectedReticule.position = new Vector3(pointerPos.x, pointerPos.y, 0);
            var pointerDistance = Vector3.Distance(transform.position, m_projectedReticule.position);
            
            m_targetReticule.position = new Vector3(pointerPos.x, pointerPos.y, 0);
            m_targetReticule.localScale = m_startingReticuleScale * Mathf.Clamp(pointerDistance / m_range, 1, 2);
            
            var fromShip = Physics2D.Raycast(transform.position, pointerPos - transform.position, pointerDistance, m_obstacleLayer);
            if (fromShip.collider != null)
            {
                Debug.DrawRay(transform.position, pointerPos - transform.position, Color.blue);
                m_projectedReticule.position = new Vector3(fromShip.point.x, fromShip.point.y, 0);
            }
            else if (pointerDistance > m_range * 2)
            {
                m_projectedReticule.position = new Ray2D(transform.position, pointerPos - transform.position).GetPoint(m_range * 2);
            }

            var reticuleDistance = Vector3.Distance(transform.position, m_projectedReticule.position);
            m_dispersion = Mathf.Clamp(reticuleDistance / m_range, 1, 2);
            m_projectedReticule.localScale = m_startingReticuleScale * m_dispersion;
        }

        private void CheckForEnemyLock()
        {
            var shipHit = Physics2D.OverlapCircle(m_targetReticule.position, 0.1f, m_shipLayer);
            //todo-P1 change way to lock on enemy, not using tag, maybe add feedback ex : X cannot lock on ally ship, X enemy ship is too far
            if (shipHit is not null && shipHit.CompareTag("Enemy")) 
            {
                m_projectedReticuleImage.sprite = m_lockReticuleSprite;
                m_tryLockOnShip ??= shipHit.gameObject.GetComponent<Ship>();
            }
            else
            {
                m_projectedReticuleImage.sprite = m_targetReticuleSprite;
                m_tryLockOnShip = null;
            }
        }
    }
}