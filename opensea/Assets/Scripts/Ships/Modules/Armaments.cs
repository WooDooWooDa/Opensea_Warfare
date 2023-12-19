using System.Collections.Generic;
using System.Linq;
using Assets.Scripts.Weapons;
using JetBrains.Annotations;
using UnityEngine;

namespace Assets.Scripts.Ships.Modules
{
    public class Armaments: Module
    {
        [SerializeField] private List<Transform> m_mainGunPositions = new();
        public List<Weapon> AllWeapons => m_armamentSlots;
        [SerializeField] private List<Weapon> m_armamentSlots = new();

        private List<Weapon> m_selectedWeapons = new();
        
        public override void Initialize(Ship attachedShip)
        {
            base.Initialize(attachedShip);
            AddWeapons(attachedShip.Stats.weaponSlots);
        }

        public void SetTargetTo(Ship targetedShip)
        {
            foreach (var weapon in m_selectedWeapons)
            {
                weapon.LockOn(targetedShip);
            }
        }
        
        public void SetTargetTo(Vector3 coords)
        {
            foreach (var weapon in m_selectedWeapons)
            {
                weapon.SetTargetCoord(coords);
            }
        }
        
        public void SelectWeapon(WeaponType type)
        {
            //unselect all selected
            m_selectedWeapons = m_armamentSlots.Where(w => w.Type == type).ToList();
            //call select on newly selected
        }

        public void TryFireSelectedWeapons(float dispersion)
        {
            foreach (var weapon in m_selectedWeapons)
            {
                weapon.TryFire(dispersion);
            }
        }

        protected override void InternalPreUpdateModule(float deltaTime)
        {
            
        }

        protected override void InternalUpdateModule(float deltaTime)
        {
            foreach (var weapon in m_armamentSlots)
            {
                weapon.UpdateWeapon(deltaTime);
            }
        }
        
        private void AddWeapons(IEnumerable<WeaponType> statsWeaponSlots)
        {
            var config = Main.Instance.WeaponsPrefabConfig;
            var mainPos = 0;
            foreach (var newWeapon in statsWeaponSlots.Select(weaponSlot => config.TryGetWeaponOfType(weaponSlot)))
            {
                if (newWeapon == null) continue;
                
                var weaponGo = Instantiate(newWeapon, transform);
                if (newWeapon.Type == WeaponType.Main)
                {
                    weaponGo.transform.SetParent(m_mainGunPositions[mainPos++]);
                    weaponGo.transform.localPosition = Vector3.zero;
                }
                m_armamentSlots.Add(weaponGo);
            }
        }
    }
}