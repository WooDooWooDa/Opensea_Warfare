using Assets.Scripts.Helpers;
using Assets.Scripts.Ships;
using System;
using System.Collections.Generic;
using UnityEngine;

namespace Assets.Scripts.Managers
{
    public class FleetManager : Manager
    {
        [SerializeField] private List<Ship> m_ships = new List<Ship>();

        private Ship m_selectedShip;

        private void Start()
        {
            Events.Inputs.OnNumPressed += FocusOn;
        }

        public void RegisterShipToFleet(Ship ship)
        {
            m_ships.Add(ship);
        }

        public void FocusOn(Ship ship)
        {
            var focusedShip = m_ships.Find(x => x == ship);
            if (focusedShip != null) {
                m_selectedShip = focusedShip;
                debugger.Log("Selected ship is now : " + focusedShip);
            }
        }

        public void FocusOn(int ship)
        {
            if (ship > m_ships.Count) {
                debugger.Log("No ship at this position");
                return; //Add no ship msg
            }
            var focusedShip = m_ships[ship - 1];
            if (focusedShip != null) {
                m_selectedShip = focusedShip;
                debugger.Log("Selected ship is now : " + focusedShip);
            }
        }
    }
}
