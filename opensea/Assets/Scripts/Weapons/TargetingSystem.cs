using Assets.Scripts.Ships;
using UnityEngine;

namespace Assets.Scripts.Weapons
{
    public class TargetingSystem
    {
        public bool HasTarget => m_hasTarget;
        public Vector3 Target => m_target;
        public bool IsLocked => m_isLockedOnShip;
        
        private Weapon m_associatedWeapon;
        private Ship m_ship;
        
        private bool m_isLockedOnShip;
        private Ship m_lockedOnShip;
        private bool m_hasTarget;
        private Vector3 m_target;

        private float m_range;
        private float m_maxRange => m_range * 2;
        private float m_accuracy;

        public void Initialize(Weapon weapon, Ship ship)
        {
            m_associatedWeapon = weapon;
            m_ship = ship;
            m_range = ship.Stats.RNG;
            m_accuracy = ship.Stats.ACC / 10;
        }

        public void Update(float delta)
        {
            if (m_isLockedOnShip)
            {
                SetTarget(m_lockedOnShip.transform.position);
            }

            //Target out of range ?
            if (m_isLockedOnShip && DistanceToTarget() > m_maxRange)
            {
                CancelLockOn(m_lockedOnShip);
            }
        }

        public float DistanceToTarget()
        {
            return (m_associatedWeapon.WeaponTransform.position - m_target).magnitude;
        }
        
        public void SetTarget(Vector3 position)
        {
            m_hasTarget = true;
            m_target = position;
        }

        public void LockOn(Ship targetShip)
        {
            m_hasTarget = m_isLockedOnShip = targetShip is not null;
            m_lockedOnShip = targetShip;
            if (targetShip is not null)
            {
                m_lockedOnShip.OnShipDestroyed += CancelLockOn;
                SetTarget(targetShip.transform.position);
            }
        }

        public void CancelTarget()
        {
            m_hasTarget = false;
            //m_target = Vector3.zero;
        }

        public void CancelLockOn()
        {
            m_isLockedOnShip = false;
            m_lockedOnShip.OnShipDestroyed -= CancelLockOn;
            m_lockedOnShip = null;
        }
        
        public Vector3 GetDispersedTargetPoint() {
            return m_target + (Vector3)(Random.insideUnitCircle * GetDispersionValue());
        }

        public float GetDispersionValue() => (m_accuracy * DistanceToTarget()) / m_range;

        private void CancelLockOn(Ship ship)
        {
            if (ship != m_lockedOnShip) return;

            m_isLockedOnShip = false;
            m_lockedOnShip.OnShipDestroyed -= CancelLockOn;
            m_lockedOnShip = null;
        }
    }
}