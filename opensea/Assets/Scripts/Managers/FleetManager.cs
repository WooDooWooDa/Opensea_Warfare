using Assets.Scripts.Helpers;
using Assets.Scripts.Ships;
using System;
using System.Collections.Generic;
using Assets.Scripts.Inputs;
using UnityEngine;

namespace Assets.Scripts.Managers
{
    public class FleetManager : Manager
    {
        [SerializeField] private List<Ship> m_ships = new List<Ship>();

        public Action<Ship> OnShipSelectedChanged;
        
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
            Focus(focusedShip);
        }

        private void FocusOn(int ship)
        {
            if (ship > m_ships.Count) 
            {
                debugger.Log("No ship at this position");
                return; //Add no ship msg
            }
            
            var focusedShip = m_ships[ship - 1];
            focusedShip.OnSelect(); //calls focusOn
        }

        private void Focus(Ship ship)
        {
            debugger.Log("Unselecting ship : " + m_selectedShip);
            if (m_selectedShip != null) m_selectedShip.OnDeselect();
            m_selectedShip = null; 
            
            OnShipSelectedChanged?.Invoke(ship);
            
            if (ship != null) 
            {
                m_selectedShip = ship;
                Events.Actions.FireOnSelected(m_selectedShip.GetComponent<Selectable>());
                debugger.Log("Selected ship is now : " + ship);
            }
        }
    }
}
