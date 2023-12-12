using System.Collections.Generic;
using System.Linq;
using Assets.Scripts.Ships;
using UI;
using UnityEngine;

namespace Assets.Scripts.Managers
{
    public class UIManager : Manager
    {
        [SerializeField] private List<ShipPanel> m_shipPanels = new();
        
        private FleetManager m_fleetManager;
        private Ship m_currentSelectedShip;
        
        private void Start()
        {
            Main.Instance.RegisterManager(this);

            m_fleetManager = Main.Instance.GetManager<FleetManager>();
            m_fleetManager.OnShipSelectedChanged += ShipChanged;
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
            //todo maybe make animation to ui when ship change (mainly for fleet panel)
            
            m_currentSelectedShip = newShip;
            if (newShip != null)
            {
                //open closed panel
                foreach (var shipPanel in m_shipPanels)
                {
                    var modulesNeeded = shipPanel.ModulesTypeFor;
                    shipPanel.UpdatePanelWithModules(m_currentSelectedShip.GetModuleOfType(modulesNeeded).ToList());
                    //animation
                }
            }
            else
            {
                foreach (var shipPanel in m_shipPanels.Where(shipPanel => shipPanel.NeedModule))
                {
                    shipPanel.UpdatePanelWithModules(null);
                    //shipPanel.enabled = false;
                    //close
                }
            }
        }

    }
}