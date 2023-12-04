using System.Collections.Generic;
using System.Linq;
using Assets.Scripts.Ships;
using UI;

namespace Assets.Scripts.Managers
{
    public class UIManager : Manager
    {
        private List<ShipPanel> m_shipPanels = new();
        
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
            
            if (newShip != null && m_currentSelectedShip != newShip)
            {
                //open closed panel
                m_currentSelectedShip = newShip;
                foreach (var panel in m_shipPanels)
                {
                    var moduleNeeded = panel.ModuleTypeFor;
                    panel.UpdatePanelWithModule(m_currentSelectedShip.GetModuleOfType(moduleNeeded));
                    //animation
                }
            }
            else
            {
                foreach (var shipPanel in m_shipPanels.Where(shipPanel => shipPanel.NeedModule))
                {
                    //shipPanel.enabled = false;
                    //close
                }
            }
        }

    }
}