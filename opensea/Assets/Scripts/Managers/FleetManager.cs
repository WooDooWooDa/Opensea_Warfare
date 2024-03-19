using System;
using System.Collections.Generic;
using Assets.Scripts.Ships;
using UnityEngine;

namespace Assets.Scripts.Managers
{
    public abstract class FleetManager : Manager
    {
        [SerializeField] protected List<Ship> m_ships = new List<Ship>();
        
        public List<Ship> Ships => m_ships;
        public List<Ship> DestroyedShips => m_markedAsDestroyedShips;
        public Action<Ship> ShipIsDestroyed;
        public Action FleetIsDestroyed;
        
        private List<Ship> m_markedAsDestroyedShips = new List<Ship>();
        private List<Ship> m_onceSpottedShip = new List<Ship>();

        public void RegisterShipToFleet(Ship ship)
        {
            if (m_ships.Contains(ship)) return;
            
            m_ships.Add(ship);
            ship.OnShipDestroyed += FleetShipDestroyed;
            InternalRegister(ship);
        }
        
        protected void FleetShipDestroyed(Ship ship)
        {
            m_markedAsDestroyedShips.Add(ship);
            ShipIsDestroyed?.Invoke(ship);
            CheckFleetIntegrity();
        }

        protected virtual void InternalRegister(Ship ship) {}
        
        private void CheckFleetIntegrity()
        {
            if (m_markedAsDestroyedShips.Count == m_ships.Count)
            {
                FleetIsDestroyed?.Invoke();
            }
        }
    }
}