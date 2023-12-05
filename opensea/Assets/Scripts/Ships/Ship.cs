﻿using Assets.Scripts.Inputs;
using Assets.Scripts.Managers;
using Assets.Scripts.Ships.Modules;
using Assets.Scripts.Ships.SOs;
using System.Collections.Generic;
using Assets.Scripts.Ships.Common;
using UnityEngine;

namespace Assets.Scripts.Ships
{
    [RequireComponent(typeof(Engine), typeof(SteeringGear))]
    public abstract class Ship: MonoBehaviour, ISelectable
    {
        [SerializeField] private ShipInformations m_informations;
        public ShipStats Stats => m_stats;
        [SerializeField] private ShipStats m_stats;
        [SerializeField] private List<Module> m_modules = new();

        private FleetManager m_fleetManager;

        private void Start()
        {
            m_fleetManager = Main.Instance.GetManager<FleetManager>();
            m_fleetManager.RegisterShipToFleet(this);
            RegisterModules();
        }

        private void Update()
        {
            foreach (var mod in m_modules) {
                mod.UpdateModule(Time.deltaTime);
            }
        }

        public void OnDeselect()
        {
            
        }

        public void OnSelect()
        {
            m_fleetManager.FocusOn(this);
        }
        
        public Module GetModuleOfType(ModuleType type)
        {
            return m_modules.Find(module => module.Type == type);
        }

        private void RegisterModules()
        {
            //Attached
            var modulesAttached = GetComponentsInChildren<Module>();
            m_modules.AddRange(modulesAttached);
            
            //Saved
            
            //BindListeners and init
            foreach (var module in m_modules)
            {
                module.Initialize(this);
                module.OnStateChanged += (damageable, state) => { };
                if (module is IDestroyable isDestroyableModule)
                {
                    
                }
            }
        }
    }
}
