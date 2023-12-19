using System;
using System.Collections.Generic;
using System.Linq;
using Assets.Scripts.Helpers;
using Assets.Scripts.Ships.Modules;
using Assets.Scripts.Weapons;
using UnityEngine;
using UnityEngine.Serialization;

namespace UI
{
    public class ArmamentShipPanel : ShipPanel
    {
        [SerializeField] private List<WeaponSlotWidget> m_weaponSlotWidgets = new();
        [SerializeField] private WeaponStatusWidget m_weaponStatusWidgetPrefab;
        
        private Armaments m_armamentsModule;
        private FireControl m_fireControlModule;

        private void Start()
        {
            foreach (var slotWidget in m_weaponSlotWidgets)
            {
                slotWidget.SelectionButton.onClick.AddListener(
                    () => SelectSlot(slotWidget.WeaponType));
            }
            
            Events.Inputs.OnSpaceBarPressed += ToggleAiming;
        }

        public override void UpdatePanelWithModules(List<Module> modules)
        {
            base.UpdatePanelWithModules(modules);

            if (modules == null || !modules.Any()) return;
            
            m_armamentsModule = (Armaments)modules.Find(m => m.Type == ModuleType.Armament);
            m_fireControlModule = (FireControl)modules.Find(m => m.Type == ModuleType.FireControl);

            if (m_armamentsModule == null) return;
            
            var moduleWeaponTypes = m_armamentsModule.AllWeapons.Select(m => m.Type).Distinct();
            m_weaponSlotWidgets.ForEach(slot =>
            {
                slot.gameObject.SetActive(moduleWeaponTypes.Contains(slot.WeaponType));
                CreateStatusWidget(
                    m_armamentsModule.AllWeapons.Where(w => w.Type == slot.WeaponType).ToList(), 
                    slot.m_weaponStatusParent);
            });
            
            //select the first one automatically
            SelectSlot(m_weaponSlotWidgets[0].WeaponType);
            
            UpdateSeparator();
        }

        private void CreateStatusWidget(List<Weapon> weapons, Transform parent)
        {
            weapons.ForEach(w =>
            {
                var newWidget = Instantiate(m_weaponStatusWidgetPrefab, parent);
                newWidget.SetWeaponRef(w);
            });
        }

        private void UpdateSeparator()
        {
            for (var i = 0; i < m_weaponSlotWidgets.Count; i++)
            {
                if (m_weaponSlotWidgets[i].Separator != null)
                {
                    m_weaponSlotWidgets[i].Separator.SetActive(m_weaponSlotWidgets[i].IsActive
                                                         && m_weaponSlotWidgets[i-1].IsActive);
                }
            }
        }

        private void SelectSlot(WeaponType selectedWeaponType)
        {
            m_armamentsModule.SelectWeapon(selectedWeaponType);
            
            m_weaponSlotWidgets.ForEach(slot =>
            {
                slot.SetActive(selectedWeaponType);
            });
        }
        
        private void ToggleAiming()
        {
            if (m_fireControlModule)
                m_fireControlModule.ToggleAiming();
        }
    }
}