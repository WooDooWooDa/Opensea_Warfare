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

        private bool m_aimingOutOfRange;
        private bool m_isLockedOnShip;
        private Ship m_lockedOnShip;
        private bool m_hasTarget;
        private Vector3 m_target;

        private Vector3 m_weaponPosition;
        private float m_effectiveRange;
        private float m_maxRange;
        private float m_accuracy;

        public void Initialize(Weapon weapon)
        {
            m_associatedWeapon = weapon;
            m_weaponPosition = weapon.WeaponTransform.position; 
            m_effectiveRange = weapon.Stats.EffectiveRange;
            m_maxRange = weapon.Stats.MaxRange;
            m_accuracy = weapon.Stats.Accuracy;
        }

        public void Update(float delta)
        {
            if (m_isLockedOnShip)
            {
                SetTarget(m_lockedOnShip.transform.position);
            }

            //Target out of range ?
            if (OutOfRange(m_target))
            {
                CancelTarget();
                if (m_isLockedOnShip)
                    CancelLockOn(m_lockedOnShip);
            }
        }

        public float DistanceToTarget()
        {
            return (m_weaponPosition - m_target).magnitude;
        }
        
        public void SetTarget(Vector3 position)
        {
            m_aimingOutOfRange = false;
            m_hasTarget = true;
            m_target = position;
            if (OutOfRange(position))
            {
                m_aimingOutOfRange = true;
                var dir = (position - m_weaponPosition).normalized;
                m_target = m_weaponPosition + (dir * m_maxRange);
            }
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
        }
        
        public Vector3 GetDispersedTargetPoint() {
            return m_target + (Vector3)(Random.insideUnitCircle * DispersionValue());
        }

        private float DispersionValue() {
            return Mathf.InverseLerp(m_effectiveRange, m_maxRange, DistanceToTarget());
        }

        private void CancelLockOn(Ship ship)
        {
            if (ship != m_lockedOnShip) return;

            m_isLockedOnShip = false;
            m_lockedOnShip.OnShipDestroyed -= CancelLockOn;
            m_lockedOnShip = null;
        }

        private bool OutOfRange(Vector3 targetPoint)
        {
            return (targetPoint - m_weaponPosition).magnitude > m_maxRange;
        }
    }
}