using System;
using System.Collections.Generic;
using Assets.Scripts.Ships;
using UI;
using UnityEngine;

namespace Assets.Scripts.Managers
{
    public class UIManager : Manager
    {
        private List<ShipPanel> m_shipPanels;
        
        private FleetManager m_fleetManager;
        private Ship m_currentSelectedShip;
        
        private void Start()
        {
            Main.Instance.RegisterManager(this);

            m_fleetManager = Main.Instance.GetManager<FleetManager>();
            m_fleetManager.OnShipSelectedChanged += ShipChanged;
        }

        private void Update()
        {
            foreach (var panel in m_shipPanels)
            {
                panel.UpdatePanel(m_currentSelectedShip);
            }
        }
        
        private void OnDisable()
        {
            m_fleetManager.OnShipSelectedChanged -= ShipChanged;
        }

        public void RegisterPanel(ShipPanel shipPanel)
        {
            if (!m_shipPanels.Contains(shipPanel))
                m_shipPanels.Add(shipPanel);
        }

        private void ShipChanged(Ship newShip)
        {
            if (newShip != null && m_currentSelectedShip != newShip) 
                m_currentSelectedShip = newShip;
        }

    }
}