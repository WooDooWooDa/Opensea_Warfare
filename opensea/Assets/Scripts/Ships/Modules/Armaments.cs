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
            foreach (var weapon in m_selectedWeapons.Where(weapon => weapon.Available))
            {
                Debug.Log("Fire single !");
                weapon.FireAt(coords);
                break;
            }
            Debug.Log("No available/ready weapons...");
        }

        public void FireAllWeaponAt(Vector3 coords)
        {
            Debug.Log("Fire Salvo !!");
            foreach (var weapon in m_selectedWeapons)
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
    }
}