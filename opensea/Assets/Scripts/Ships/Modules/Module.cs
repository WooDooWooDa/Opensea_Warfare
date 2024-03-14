using System;
using Assets.Scripts.Ships.Common;
using Assets.Scripts.Weapons;
using UnityEngine;

namespace Assets.Scripts.Ships.Modules
{
    public abstract class Module : MonoBehaviour, IDamageable
    {
        [SerializeField] private ModuleInformation m_info;
        public ModuleType Type => m_info.Type;
        public Ship ModuleShip => m_ship;
        public float CurrentHp { get; set; }
        public DamageState CurrentState { get; set; }
        public bool CanBeRepaired => CurrentState is DamageState.Damaged or DamageState.Disabled;
        public bool IsWorking => CurrentState is not DamageState.Destroyed and not DamageState.Disabled;
        
        public Action<IDamageable, DamageState> OnStateChanged { get; set; }
        public Action<IDamageable, float> OnDamageTaken { get; set; }

        protected Ship m_ship;
        
        public virtual void Initialize(Ship attachedShip)
        {
            m_ship = attachedShip;
        }

        public void UpdateModule(float deltaTime)
        {
            InternalPreUpdateModule(deltaTime);
            if (!IsWorking) return;
            
            InternalUpdateModule(deltaTime);
        }
        
        public virtual void ShipSelect() {}
        public virtual void ShipDeselect() {}
        
        public float DamageOnImpact(Impact impact)
        {
            if (!m_info.CanBeDamaged || !IsWorking) return 0;
            
            TakeDamage(impact);
            ApplyStatus();
            
            CheckState();
            ApplyState();
            return impact.BaseDamage;
        }

        public void Repair(float amount, bool full = false)
        {
            
        }

        /// <summary>
        /// Pre Update is call first even if the module is not working for any reason
        /// </summary>
        protected abstract void InternalPreUpdateModule(float deltaTime);
        /// <summary>
        /// Update is call only if the module is working in any way
        /// </summary>
        protected abstract void InternalUpdateModule(float deltaTime);

        protected virtual void TakeDamage(Impact impact)
        {
            CurrentHp -= impact.BaseDamage;
            if (CurrentHp <= 0) CurrentHp = 0;
        }
        
        protected virtual void ApplyState() { }
        
        protected virtual void ApplyStatus() { }
        
        private void CheckState() //Move this to helper function so 
        {
            var lastState = CurrentState;
            
            var newState = DamageState.Undamaged;
            if (CurrentHp < m_info.MaxHp) { 
                newState = CurrentHp switch
                {
                    <= 0 when this is not IDestroyable => DamageState.Disabled,
                    <= 0 => DamageState.Destroyed,
                    _ => DamageState.Damaged
                };
            }
            
            if (newState != lastState)
                OnStateChanged?.Invoke(this, newState);

            CurrentState = newState;
        }
    }
}

