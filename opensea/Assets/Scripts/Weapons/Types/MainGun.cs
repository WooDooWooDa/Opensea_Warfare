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
                                //todo-P2 GetDispersionPoint(..., m_stats.LockInAccuracy / 10) at an interval otherwise looks janky???
            }
            
            if (m_hasTarget)
            {
                Aim(deltaTime);
                //todo-3 add an evelation and rotation line/circle to reticule
            }
        }

        private void Aim(float delta)
        {
            ElevateTurret(delta);
            RotateTurret(delta);
        }

        private void ElevateTurret(float delta)
        {
            var turretPosition = m_turret.position;
            var distanceToTarget = Vector3.Distance(turretPosition, m_targetCoord);
            var distanceToReticule = Vector3.Distance(turretPosition, m_weaponTargetReticule.position);
            if (distanceToTarget > m_attachedShip.Stats.RNG * 2)
                distanceToTarget = m_attachedShip.Stats.RNG * 2;
            var diff = distanceToTarget - distanceToReticule;
            if (diff is > 0.05f or < -0.05f)
                m_weaponTargetReticule.localPosition += Vector3.up * (delta * Mathf.Sign(diff) * m_stats.turnSpeed / 2);
        }

        private void RotateTurret(float delta)
        {
            var vectorToTarget = m_targetCoord - m_turret.position;
            var angle = Mathf.Atan2(vectorToTarget.y, vectorToTarget.x) * Mathf.Rad2Deg - 90;
            var q = Quaternion.AngleAxis(angle, Vector3.forward);
            m_turret.rotation = Quaternion.Lerp(m_turret.rotation, q, delta * m_stats.turnSpeed / 10);
            //todo-1 check for range of rotation
        }
        
        private static Vector3 GetDispersionPoint(Vector3 center, float radius) {
            return center + (Vector3)(Random.insideUnitCircle * radius);
        }
    }
}