using System;
using System.Collections.Generic;
using System.Linq;
using Assets.Scripts.Helpers;
using Assets.Scripts.Ships.Modules;
using Assets.Scripts.Weapons;
using UnityEngine;
using UnityEngine.Serialization;
using UnityEngine.UI;

namespace UI
{
    [Serializable]
    internal struct WeaponTypeWidgetsPanelGroup
    {
        public WeaponType[] WeaponTypes;
        public Transform TypeGroup;
        public Button LinkButton;
        public GameObject Separator;
    }
    
    public class ArmamentShipPanel : ShipPanel
    {
        [SerializeField] private List<WeaponTypeWidgetsPanelGroup> m_panelGroups = new();
        [SerializeField] private WeaponSlotWidget m_weaponSlotPrefab;

        private Armaments m_armamentsModule;
        private FireControl m_fireControlModule;

        private void Start()
        {
            foreach (var panelGroup in m_panelGroups)
            {
                if (panelGroup.LinkButton != null)
                    panelGroup.LinkButton.onClick.AddListener(() => LinkWeaponType(panelGroup.WeaponTypes[0]));
            }
            
            Events.Inputs.OnSpaceBarPressed += ToggleAiming;
        }

        public override void UpdatePanelWithModules(List<Module> modules)
        {
            base.UpdatePanelWithModules(modules);

            m_armamentsModule = (Armaments)modules.Find(m => m.Type == ModuleType.Armament);
            m_fireControlModule = (FireControl)modules.Find(m => m.Type == ModuleType.FireControl);

            if (m_armamentsModule == null) return;
            //delete or deactivate all widgets
            
            foreach (var weapon in m_armamentsModule.AllWeapons)
            {
                AddWeaponToGroup(weapon);
            }

            UpdateSeparator();
        }

        private void AddWeaponToGroup(Weapon weapon)
        {
            var panelGroup = m_panelGroups.Last(pg => pg.WeaponTypes.Contains(weapon.Type));
            var newWeaponSlot = Instantiate(m_weaponSlotPrefab, panelGroup.TypeGroup);
            newWeaponSlot.WeaponStats = weapon.Stats;
            newWeaponSlot.SelectionButton.onClick.AddListener(() => SelectWeapon(weapon));
        }

        private void UpdateSeparator()
        {
            for (var i = 0; i < m_panelGroups.Count; i++)
            {
                if (m_panelGroups[i].Separator != null)
                {
                    m_panelGroups[i].Separator.SetActive(m_panelGroups[i].TypeGroup.childCount > 0 
                                                         && m_panelGroups[i-1].TypeGroup.childCount > 0);
                }
            }
        }
        
        private void ToggleAiming()
        {
            if (m_fireControlModule)
                m_fireControlModule.ToggleAiming();
        }

        private void SelectWeapon(Weapon weapon)
        {
            m_fireControlModule.SelectWeapon(weapon);
        }
        
        private void LinkWeaponType(WeaponType type)
        {
            if (m_fireControlModule != null) 
                m_fireControlModule.ToggleLinkWeaponType(type);
        }
    }
}