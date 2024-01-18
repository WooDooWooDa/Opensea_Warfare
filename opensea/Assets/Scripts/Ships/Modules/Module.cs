using System;
using Assets.Scripts.Ships.Common;
using Assets.Scripts.Weapons;
using UnityEngine;

namespace Assets.Scripts.Ships.Modules
{
    public abstract class Module : MonoBehaviour, IDamageable
    {
        public bool IsActive = true;
        [SerializeField] private ModuleInformation m_info;
        public ModuleType Type => m_info.Type;
        public float CurrentHp { get; set; }
        public DamageState CurrentState { get; set; }
        public bool CanBeRepaired => CurrentState is DamageState.Damaged or DamageState.Disabled;
        public bool IsWorking => CurrentState is not DamageState.Disabled and DamageState.Destroyed;
        
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
            if (IsWorking) return;
            
            InternalUpdateModule(deltaTime);
        }
        
        public virtual void ShipSelect() {}
        public virtual void ShipDeselect() {}
        
        public void OnImpact(Impact impact)
        {
            if (!m_info.CanBeDamaged) return;
            
            TakeDamage(impact);
            CheckState();
            ApplyState();
        }

        public void Repair(float amount, bool full = false)
        {
            
        }

        protected abstract void InternalPreUpdateModule(float deltaTime);
        protected abstract void InternalUpdateModule(float deltaTime);

        protected virtual float InternalCalculateDamage(float dmg)
        {
            return dmg;
        }

        protected virtual void ApplyState() { }
        
        protected virtual void ApplyStatus()
        {
            //todo-P2 check for fire here or elsewhere in hull module
        }

        private void TakeDamage(Impact impact)
        {
            var damageTaken = InternalCalculateDamage(impact.BaseDamage);
            CurrentHp -= damageTaken;
            OnDamageTaken?.Invoke(this, damageTaken);
            if (CurrentHp <= 0) CurrentHp = 0;
        }
        
        private void CheckState()
        {
            var lastState = CurrentState;
            
            DamageState newState;
            if (CurrentHp < m_info.MaxHp) {
                newState =  DamageState.Damaged;
            }
            else
            {
                newState = CurrentHp switch
                {
                    <= 0 when this is not IDestroyable or  => DamageState.Disabled,
                    <= 0 => DamageState.Destroyed,
                    _ => DamageState.Undamaged
                };
            }
            
            if (newState != lastState)
                OnStateChanged?.Invoke(this, newState);

            CurrentState = newState;
        }
    }
}

