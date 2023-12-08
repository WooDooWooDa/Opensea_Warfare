using System.Collections.Generic;
using System.Linq;
using Assets.Scripts.Weapons;
using UnityEngine;

namespace Assets.Scripts.Ships.Modules
{
    public class Armaments: Module
    {
        [SerializeField] private List<Weapon> m_armamentSlots = new();

        public List<Weapon> Weapons => m_armamentSlots;

        public List<Weapon> GetWeaponsOfType(WeaponType type) => m_armamentSlots.Where(w => w.Type == type).ToList();
        
        protected override void InternalPreUpdateModule(float deltaTime)
        {
            
        }

        protected override void InternalUpdateModule(float deltaTime)
        {
            
        }
    }
}