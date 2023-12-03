using System;
using System.Collections.Generic;
using Assets.Scripts.Managers;
using Assets.Scripts.Ships;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

namespace UI
{
    public class EngineShipPanel : ShipPanel
    {
        [SerializeField] private List<Button> m_speedButtons;
        [SerializeField] private Transform m_currentSpeedIndicator;
        [SerializeField] private TextMeshProUGUI m_currentSpeedText;

        [SerializeField] private float m_maxSpeedPos;
        [SerializeField] private float m_zeroSpeedPos;
        [SerializeField] private float m_maxReverseSpeedPos;
        
        private UIManager m_uiManager;
        
        private void Start()
        {
            m_uiManager = Main.Instance.GetManager<UIManager>();
            m_uiManager.RegisterPanel(this);
        }

        public override void UpdatePanel(Ship currentSelectedShip)
        {
            base.UpdatePanel(currentSelectedShip);
            
            
        }
    }
}