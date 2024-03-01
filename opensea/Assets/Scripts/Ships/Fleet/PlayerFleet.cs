using Assets.Scripts.Helpers;
using Assets.Scripts.Ships;
using System;
using Assets.Scripts.Inputs;
using UnityEngine;

namespace Assets.Scripts.Managers
{
    public class PlayerFleet : FleetManager
    {
        public Action<Ship> OnShipSelectedChanged;
        
        private Ship m_selectedShip;

        private void Start()
        {
            Events.Inputs.OnNumPressed += FocusOn;
        }

        protected override void InternalRegister(Ship ship)
        {
            if (m_selectedShip == ship) m_selectedShip.OnDeselect();
        }

        #region Focus On Ship
        public void FocusOn(Ship ship)
        {
            if (ship == m_selectedShip) return;
            
            var focusedShip = m_ships.Find(x => x == ship);
            Focus(focusedShip);
        }

        public void FocusOn(int ship)
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
        #endregion
    }
}
