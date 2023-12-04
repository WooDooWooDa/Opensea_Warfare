using UnityEngine;

namespace Assets.Scripts.Ships.Modules
{
    public abstract class Module : MonoBehaviour
    {
        public ModuleType Type => m_type;
        [SerializeField] private ModuleType m_type;
        [SerializeField] private float m_moduleHp;
        [SerializeField] private bool m_canBeDamaged = false;
        [SerializeField] private bool m_canBeDestroyed = false;
        
        public ModuleState State {
            get
            {
                if (!m_canBeDamaged) return ModuleState.Working;
                
                if (m_moduleHp > m_currentModuleHp) {
                    return ModuleState.Damaged;
                }

                return m_currentModuleHp switch
                {
                    <= 0 when !m_canBeDestroyed => ModuleState.HS,
                    <= 0 => ModuleState.Destroyed,
                    _ => ModuleState.Working
                };
            }
        }
        
        public bool CanBeRepaired { get => State is ModuleState.Damaged or ModuleState.HS; }
        
        private Ship m_ship;
        private float m_currentModuleHp;

        private void Start()
        {
            m_ship = GetComponentInParent<Ship>();
            m_ship.RegisterModule(this);
            m_currentModuleHp = m_moduleHp;
        }

        public void Activate(bool value)
        {
            if (value)
            {
                OnEnableModule();
            }
            else
            {
                OnDisableModule();
            }
        }

        protected abstract void OnEnableModule();
        protected abstract void OnDisableModule();

        public void UpdateModule(float deltaTime)
        {
            if (State is ModuleState.Destroyed or ModuleState.HS) return;
            ApplyState();
            InternalUpdateModule(deltaTime);
        }

        protected abstract void InternalUpdateModule(float deltaTime);

        protected virtual void ApplyState()
        {
            
        }
    }
}

