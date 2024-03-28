using System.Collections.Generic;
using System.Linq;
using Assets.Scripts.Helpers;
using Assets.Scripts.Ships.Modules;
using TMPro;
using UnityEngine;

namespace UI
{
    public class SteeringShipPanel : ShipPanel
    {
        [SerializeField] private Transform m_shipSilhouette;
        [SerializeField] private TextMeshProUGUI m_directionText;
        
        private SteeringGear m_steeringGear;
        
        private void Update()
        {
            if (m_steeringGear is null) return;

            var currentAngle = m_steeringGear.CurrentAngle;
            m_shipSilhouette.rotation = Quaternion.Euler(0, 0, currentAngle);
            m_directionText.text = "> " + Helper.GetStringDirection(currentAngle);
        }

        public override void UpdatePanelWithModules(List<Module> modules)
        {
            base.UpdatePanelWithModules(modules);
            
            if (modules == null || !modules.Any()) return;
            
            m_steeringGear = (SteeringGear)modules.Find(m => m.Type == ModuleType.SteeringGear);
        }
    }
}