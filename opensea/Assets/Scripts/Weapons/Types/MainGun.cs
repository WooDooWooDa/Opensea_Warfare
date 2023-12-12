using Assets.Scripts.Ships;
using UnityEngine;

namespace Assets.Scripts.Weapons
{
    public class MainGun : Weapon
    {
        [SerializeField] private Transform m_weaponTargetReticule;
        [SerializeField] private Transform m_turret;
        
        private Vector3? m_targetCoord;
        private bool m_isLockOnTarget;
        private Ship LockOnShip = null;

        public override void SetTargetCoord(Vector3? position)
        {
            m_targetCoord = position;
        }
        
        protected override void InternalFire(float dispersion)
        {
            Debug.Log("Fire " + this + " at " + m_targetCoord);
            if (!m_isLockOnTarget)
            {
                m_targetCoord = null;
            }
        }

        protected override void InternalPreUpdateWeapon(float deltaTime)
        {
            m_weaponTargetReticule.gameObject.SetActive(m_targetCoord.HasValue);
        }

        protected override void InternalUpdateWeapon(float deltaTime)
        {
            if (LockOnShip is not null && m_isLockOnTarget)
            {
                m_targetCoord = LockOnShip.transform.position;
            }
            
            if (m_targetCoord.HasValue)
            {
                Aim(deltaTime);
            }
        }

        private void Aim(float delta)
        {
            m_weaponTargetReticule.position = Vector3.MoveTowards(m_weaponTargetReticule.position, 
                m_targetCoord!.Value, delta * m_stats.turnSpeed);
            RotateTurret(delta);
        }

        private void RotateTurret(float delta)
        {
            var vectorToTarget = m_weaponTargetReticule.position - transform.position;
            var angle = Mathf.Atan2(vectorToTarget.y, vectorToTarget.x) * Mathf.Rad2Deg - 90;
            var q = Quaternion.AngleAxis(angle, Vector3.forward);
            m_turret.rotation = Quaternion.Slerp(m_turret.rotation, q, delta * m_stats.turnSpeed);
        }
    }
}