using System.Collections.Generic;
using System.Linq;
using Assets.Scripts.Weapons;
using UnityEngine;

namespace Assets.Scripts.Ships.Modules
{
    public class Armaments: Module
    {
        public List<Weapon> AllWeapons => m_armamentSlots;
        [SerializeField] private List<Weapon> m_armamentSlots = new();

        private WeaponType m_selectedWeaponType;
        private List<Weapon> m_selectedWeapons = new();

        public override void Initialize(Ship attachedShip)
        {
            base.Initialize(attachedShip);
            m_armamentSlots.ForEach(w => w.Initialize(attachedShip));
            SelectWeapon(m_armamentSlots.First().Type);
            AssignNumberToWeapons();
        }

        public bool SelectedWeaponTypeCanLockOn()
        {
            return m_selectedWeapons.Count > 0 && m_selectedWeapons.First().Stats.CanLockOnEnemy;
        }

        public void LockOnto(Ship targetedShip)
        {
            foreach (var weapon in m_selectedWeapons)
            {
                weapon.LockOn(targetedShip);
            }
        }
        
        public void FireNextWeaponAt(Vector3 coords)
        {
            foreach (var weapon in m_selectedWeapons.Where(w => w.Available && w.CanFireAt(coords)))
            {
                Debug.Log($"{m_selectedWeaponType} #{weapon.Number} fire single !");
                weapon.FireAt(coords);
                return;
            }
            Debug.Log("No available/ready weapons...");
        }

        public void FireAllWeaponAt(Vector3 coords)
        {
            Debug.Log("Fire Salvo !!");
            foreach (var weapon in m_selectedWeapons.Where(w => w.Available && w.CanFireAt(coords)))
            {
                weapon.FireAt(coords);
            }
        }

        public void FollowPosition(Vector3 position)
        {
            foreach (var weapon in m_selectedWeapons)
            {
                weapon.Follow(position);
            }
        }
        
        public void SelectWeapon(WeaponType type)
        {
            //unselect all selected
            m_selectedWeaponType = type;
            m_selectedWeapons = m_armamentSlots.Where(w => w.Type == type).ToList();
            //call select on newly selected
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

        private void AssignNumberToWeapons()
        {
            foreach (var weaponGroup in m_armamentSlots.GroupBy(w => w.Type))
            {
                var number = 1;
                foreach (var weapon in weaponGroup)
                {
                    weapon.Number = number;
                    number++;
                }
            }
        }
    }
}