using Assets.Scripts.Ships;
using UnityEngine;

namespace Assets.Scripts.Weapons
{
    public class MainGun : Weapon
    {
        [SerializeField] private Transform m_weaponTargetReticule;
        [SerializeField] private Transform m_turret;

        public override void SetTargetCoord(Vector3 position)
        {
            if (m_lockOnShip is not null) return;
            
            m_hasTarget = true;
            m_targetCoord = position;
        }

        public override void LockOn(Ship targetShip)
        {
            m_hasTarget = true;
            m_lockOnShip = targetShip;
        }
        
        protected override void InternalFire(float dispersion)
        {
            Debug.Log("Fire " + this + " at " + m_targetCoord);
            var dispersedTargetPoint = GetDispersionPoint(m_targetCoord, dispersion);
            Debug.Log("Dispersed at " + dispersedTargetPoint);

            var projectile = SpawnProjectile();
            projectile.SetData(m_attachedShip, m_stats.BaseProjectile, dispersedTargetPoint, FirePower);
            projectile.transform.LookAt(dispersedTargetPoint);
            
            if (m_lockOnShip is null) //reset target if not locked on
            {
                m_hasTarget = false;
            }
        }

        protected override void InternalPreUpdateWeapon(float deltaTime)
        {
            m_weaponTargetReticule.gameObject.SetActive(m_hasTarget);
        }

        protected override void InternalUpdateWeapon(float deltaTime)
        {
            if (m_lockOnShip is not null)
            {
                m_targetCoord = m_lockOnShip.transform.position; 
                                //todo GetDispersionPoint(..., m_stats.LockInAccuracy / 10) at an interval otherwise looks janky???
            }
            
            if (m_hasTarget)
            {
                Aim(deltaTime);
            }
        }

        private void Aim(float delta)
        {
            //todo change the aim to reflect what i wrote on paper (more of a rotation around turret)
            m_weaponTargetReticule.position = Vector3.MoveTowards(m_weaponTargetReticule.position, 
                m_targetCoord, delta * m_stats.turnSpeed / 10);
            RotateTurret(delta);
        }

        private void RotateTurret(float delta)
        {
            var vectorToTarget = m_weaponTargetReticule.position - transform.position;
            var angle = Mathf.Atan2(vectorToTarget.y, vectorToTarget.x) * Mathf.Rad2Deg - 90;
            var q = Quaternion.AngleAxis(angle, Vector3.forward);
            m_turret.rotation = Quaternion.Slerp(m_turret.rotation, q, delta * m_stats.turnSpeed / 5);
        }
        
        private static Vector3 GetDispersionPoint(Vector3 center, float radius) {
            return center + (Vector3)(Random.insideUnitCircle * radius);
        }
    }
}