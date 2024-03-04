using System.Collections.Generic;
using System.Linq;
using Assets.Scripts.Helpers;
using Assets.Scripts.Ships.Modules;
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

        [SerializeField] private int[] m_fontSizes;
        [SerializeField] private Color[] m_fontColors;
        
        private Engine m_engineModule;
        private float m_currentSpeedPourcentage;

        private void Start()
        {
            Events.Ship.ChangedSpeed += (ship, value) =>
            {
                m_currentSpeedPourcentage = value;
                ChangeSpeedButton();
            };
        }

        public override void UpdatePanelWithModules(List<Module> modules)
        {
            base.UpdatePanelWithModules(modules);
            
            if (modules == null || !modules.Any()) return;
            
            m_engineModule = (Engine)modules.Find(m => m.Type == ModuleType.Engine);
            
            if (m_engineModule == null) return;

            m_currentSpeedPourcentage = m_engineModule.TargetPourcentageOfSpeed;
            ChangeSpeedButton();
        }

        private void Update()
        {
            if (m_engineModule is null) return;
            
            m_currentSpeedText.text = $"{m_engineModule.CurrentSpeed:F1} kts";

            var indicatorY = m_engineModule.CurrentSpeed >= 0 
                ? Mathf.Lerp(m_zeroSpeedPos, m_maxSpeedPos, m_engineModule.CurrentSpeedPercentage) 
                : Mathf.Lerp(m_zeroSpeedPos, m_maxReverseSpeedPos, (-m_engineModule.CurrentSpeedPercentage) * 4);
            m_currentSpeedIndicator.localPosition = new Vector3(m_currentSpeedIndicator.localPosition.x, indicatorY, 0);
        }

        private void ChangeSpeedButton()
        {
            UnSelectAllSpeedButton();
            var btnIndex = (int)(m_currentSpeedPourcentage * 4) switch
            {
                -1 => 5,
                0 => 4,
                1 => 3,
                2 => 2,
                3 => 1,
                4 => 0,
                _ => 4,
            };
            SelectSpeedButton(m_speedButtons[btnIndex]);
        }

        private void SelectSpeedButton(Button btn)
        {
            var text = btn.GetComponentInChildren<TextMeshProUGUI>();
            text.color = m_fontColors[1];
            text.fontSize = m_fontSizes[1];
        }

        private void UnSelectAllSpeedButton()
        {
            foreach (var speedButton in m_speedButtons)
            {
                var text = speedButton.GetComponentInChildren<TextMeshProUGUI>();
                text.color = m_fontColors[0];
                text.fontSize = m_fontSizes[0];
            }
        }
    }
}