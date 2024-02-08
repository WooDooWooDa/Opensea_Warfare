using Assets.Scripts.Ships;
using UnityEngine;

namespace Assets.Scripts.Weapons
{
    public class MainGun : Weapon
    {
        [SerializeField] private Transform m_turret;

        public override void SetFireTargetCoord(Vector3 position)
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

        public override void Follow(Vector3 position)
        {
            if (m_hasTarget) return;
            
            m_targetCoord = position;
        }
        
        protected override void InternalFire(Projectile projectile, float dispersionFactor)
        {
            Debug.Log("Fire " + this + " at " + m_targetCoord);
            var dispersedTargetPoint = GetDispersionPoint(m_targetCoord, dispersionFactor);
            Debug.Log("Dispersed at " + dispersedTargetPoint);
            
            projectile.SetData(new ProjectileData()
            {
                Sender = m_attachedShip,
                Ammo = m_loadedAmmo,
                TargetPoint = dispersedTargetPoint
            }, FirePower);
            
            if (m_lockOnShip is null) //reset target if not locked on, single shot
            {
                m_hasTarget = false;
            }
        }

        protected override void InternalPreUpdateWeapon(float deltaTime)
        {
            
        }

        protected override void InternalUpdateWeapon(float deltaTime)
        {
            if (m_lockOnShip is not null)
            {
                m_targetCoord = m_lockOnShip.transform.position; 
                //todo-P2 GetDispersionPoint(..., m_stats.LockInAccuracy / 10) at an interval otherwise looks janky???
            }
            
            Aim(deltaTime);
            //todo-3 add an evelation and rotation line/circle to reticule
        }

        protected override bool InternalReadyToFire()
        {
            return Vector3.Distance(m_targetCoord, m_weaponTargetReticule.position) <= 0.5;
        }

        private void Aim(float delta)
        {
            ElevateTurret(delta);
            RotateTurret(delta);
        }

        private void ElevateTurret(float delta)
        {
            var diff = GetDistanceDiffToTarget();
            if (diff is > 0.05f or < -0.05f)
                m_weaponTargetReticule.localPosition += Vector3.up * (delta * Mathf.Sign(diff) * m_stats.turnSpeed / 2);
        }

        private void RotateTurret(float delta)
        {
            var vectorToTarget = m_targetCoord - m_turret.position;
            var angle = Mathf.Atan2(vectorToTarget.y, vectorToTarget.x) * Mathf.Rad2Deg - 90;
            var q = Quaternion.AngleAxis(angle, Vector3.forward);
            m_turret.rotation = Quaternion.RotateTowards(m_turret.rotation, q, delta * m_stats.turnSpeed * 5);
            //todo-1 check for range of rotation
        }

        private float GetDistanceDiffToTarget()
        {
            var turretPosition = m_turret.position;
            var distanceToTarget = Vector3.Distance(turretPosition, m_targetCoord);
            var distanceToReticule = Vector3.Distance(turretPosition, m_weaponTargetReticule.position);
            if (distanceToTarget > m_attachedShip.Stats.RNG * 2)
                distanceToTarget = m_attachedShip.Stats.RNG * 2;
            return distanceToTarget - distanceToReticule;
        }
        
        private static Vector3 GetDispersionPoint(Vector3 center, float radius) {
            return center + (Vector3)(Random.insideUnitCircle * radius);
        }
    }
}