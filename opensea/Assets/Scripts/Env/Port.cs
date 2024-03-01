using System;
using System.Collections.Generic;
using Assets.Scripts.Managers;
using Assets.Scripts.Ships.Modules;
using Assets.Scripts.Weapons;
using UnityEngine;

namespace Env
{
    public class Port : MonoBehaviour
    {
        [SerializeField] private List<Ammo> m_availableAmmoAtPort;

        private void TempLoadAllShip()
        {
            var fleetManager = Main.Instance.GetManager<PlayerFleet>();
            foreach (var ship in fleetManager.Ships)
            {
                var bay = ship.GetModuleOfType<AmmunitionBay>();
                bay.LoadAmmunition(m_availableAmmoAtPort[0], 5);
                bay.LoadAmmunition(m_availableAmmoAtPort[1], 5);
            }
        }

        private void OnGUI()
        {
            if (GUI.Button(new Rect(10, 10, 150, 100), "Load all ship (TEMP)"))
            {
                TempLoadAllShip();
            }
        }
    }
}