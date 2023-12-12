using System;
using Assets.Scripts.Ships.Common;
using Assets.Scripts.Weapons.SOs;
using UnityEngine;

namespace Assets.Scripts.Weapons
{
    public abstract class Weapon: MonoBehaviour, IDestroyable
    {
        public WeaponStats Stats => m_stats;
        [SerializeField] protected WeaponStats m_stats;
        public WeaponType Type => m_stats.Type;
        public bool IsWorking => CurrentState is not DamageState.Disabled and DamageState.Destroyed;
        public bool ReadyToFire { get; set; }
        public float CurrentHp { get; set; }
        public DamageState CurrentState { get; set; }
        public Action<IDamageable, DamageState> OnStateChanged { get; set; }
        public Action<IDamageable, float> OnDamageTaken { get; set; }
        public Action<IDestroyable> OnDestroyed { get; set; }
        
        public void OnImpact(Impact impact)
        {
            throw new NotImplementedException();
        }

        public void UpdateWeapon(float deltaTime)
        {
            InternalPreUpdateWeapon(deltaTime);
            if (IsWorking) return;
            
            InternalUpdateWeapon(deltaTime);
        }

        public abstract void SetTargetCoord(Vector3? position);

        public void TryFire(float dispersion)
        {
            //if can fire (reloading, ammunition ??)
            
            for (var i = 0; i < m_stats.SalvoCount; i++)
            {
                InternalFire(dispersion);
                //Wait for salvo time
            }
        }

        protected abstract void InternalFire(float dispersion);
        protected abstract void InternalPreUpdateWeapon(float deltaTime);
        protected abstract void InternalUpdateWeapon(float deltaTime);
    }
}
