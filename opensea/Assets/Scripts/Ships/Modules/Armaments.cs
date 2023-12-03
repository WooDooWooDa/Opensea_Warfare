using System.Collections.Generic;
using Assets.Scripts.Weapons;
using UnityEngine;

namespace Assets.Scripts.Ships.Modules
{
    public class Armaments: Module
    {
        [SerializeField] private List<Weapon> m_armamentSlots = new();

        protected override void OnEnableModule()
        {
            
        }

        protected override void OnDisableModule()
        {
            
        }

        protected override void InternalUpdateModule(float deltaTime)
        {
            
        }
    }
}