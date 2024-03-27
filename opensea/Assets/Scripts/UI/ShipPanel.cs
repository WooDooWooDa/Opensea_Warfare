using System.Collections.Generic;
using System.Linq;
using Assets.Scripts.Ships.Modules;
using UnityEngine;

namespace UI
{
    public class ShipPanel : MonoBehaviour
    {
        public bool NeedModule => m_needModule;
        [SerializeField] private bool m_needModule;
        public ModuleType[] ModulesTypeFor => m_modulesTypeFor;
        [SerializeField] private ModuleType[] m_modulesTypeFor;

        private bool m_isOpen;
        
        public virtual void UpdatePanelWithModules(List<Module> modules)
        {
            if (modules == null || !modules.Any()) ClosePanel();
            else OpenPanel();
        }

        protected virtual void OpenPanel()
        {
            if (m_isOpen) return;
        }
        
        protected virtual void ClosePanel()
        {
            if (!m_isOpen) return;
        }
    }
}