using Assets.Scripts.Inputs;
using Assets.Scripts.Managers;
using Assets.Scripts.Ships.SOs;
using Assets.Scripts.Weapons;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnityEngine;

namespace Assets.Scripts.Ships
{
    public abstract class Ship: MonoBehaviour, ISelectable
    {
        [SerializeField] private ShipInformations m_informations;
        [SerializeField] private ShipStats m_stats;
        [SerializeField] private List<Weapon> m_armementSlots = new();

        private FleetManager m_fleetManager;

        private void Start()
        {
            m_fleetManager = Main.Instance.GetManager<FleetManager>();
            m_fleetManager.RegisterShipToFleet(this);
        }

        public void OnDeselect()
        {
            
        }

        public void OnSelect()
        {
            m_fleetManager.FocusOn(this);
        }
    }
}
