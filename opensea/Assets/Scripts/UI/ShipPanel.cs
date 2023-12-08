using Assets.Scripts.Ships.Modules;
using UnityEngine;

namespace UI
{
    public class ShipPanel : MonoBehaviour
    {
        public bool NeedModule => m_needModule;
        [SerializeField] private bool m_needModule;
        public ModuleType ModuleTypeFor => m_moduleTypeFor;
        [SerializeField] private ModuleType m_moduleTypeFor;
        
        public virtual void UpdatePanelWithModule(Module module)
        {
            
        }
    }
}