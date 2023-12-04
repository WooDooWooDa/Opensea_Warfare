using Assets.Scripts.Inputs;
using Assets.Scripts.Managers;
using Assets.Scripts.Ships.Modules;
using Assets.Scripts.Ships.SOs;
using System.Collections.Generic;
using Assets.Scripts.Helpers;
using UnityEngine;

namespace Assets.Scripts.Ships
{
    [RequireComponent(typeof(Engine), typeof(SteeringGear))]
    public abstract class Ship: MonoBehaviour, ISelectable
    {
        [SerializeField] private ShipInformations m_informations;
        [SerializeField] private ShipStats m_stats;
        [SerializeField] private List<Module> m_modules = new();

        private FleetManager m_fleetManager;

        private void Start()
        {
            m_fleetManager = Main.Instance.GetManager<FleetManager>();
            m_fleetManager.RegisterShipToFleet(this);
        }

        private void Update()
        {
            foreach (Module mod in m_modules) {
                mod.UpdateModule(Time.deltaTime);
            }
        }

        public void OnDeselect()
        {
            foreach (Module mod in m_modules) {
                mod.Activate(false);
            }
        }

        public void OnSelect()
        {
            m_fleetManager.FocusOn(this);
            foreach (Module mod in m_modules) {
                mod.Activate(true);
            }
        }

        public Module GetModuleOfType(ModuleType type)
        {
            return m_modules.Find(module => module.Type == type);
        }

        public void RegisterModule(Module module)
        {
            if (!m_modules.Contains(module))
                m_modules.Add(module);
        }
    }
}
