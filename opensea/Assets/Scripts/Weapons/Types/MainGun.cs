using Assets.Scripts.Weapons.Projectiles;
using UnityEngine;

namespace Assets.Scripts.Weapons
{
    public class MainGun : Weapon
    {
        protected override void InternalFire(Projectile projectile, Vector3 at)
        {
            var dispersedTargetPoint = m_targetingSystem.GetDispersedTargetPoint();
            
            projectile.SetData(new ProjectileData()
            {
                DamageSource = m_attachedShip,
                Ammo = m_loadedAmmo,
                TargetPoint = dispersedTargetPoint
            }, FirePower);
        }

        protected override void InternalPreUpdateWeapon(float deltaTime)
        {
            
        }

        protected override void InternalUpdateWeapon(float deltaTime)
        {
            Aim(deltaTime);
        }

        protected override bool InternalReadyToFire()
        {
            return Vector3.Distance(m_targetingSystem.Target, m_weaponTargetReticule.position) <= m_stats.Accuracy;
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
                m_weaponTargetReticule.localPosition += Vector3.up * (delta * Mathf.Sign(diff) * m_stats.turnSpeed);
        }

        private void RotateTurret(float delta)
        {
            var vectorToTarget = m_targetingSystem.Target - WeaponTransform.position;
            var angle = Mathf.Atan2(vectorToTarget.y, vectorToTarget.x) * Mathf.Rad2Deg - 90;
            var q = Quaternion.AngleAxis(angle, Vector3.forward);
            WeaponTransform.rotation = Quaternion.RotateTowards(WeaponTransform.rotation, q, delta * m_stats.turnSpeed * 5);
            LimitRangeOfRotation(WeaponTransform);
        }

        private float GetDistanceDiffToTarget()
        {
            var distanceToTarget = m_targetingSystem.DistanceToTarget();
            var distanceToReticule = Vector3.Distance(WeaponTransform.position, m_weaponTargetReticule.position);
            return distanceToTarget - distanceToReticule;
        }
    }
}