using System.Collections.Generic;
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
        private float[] m_speedPart;
        private int m_currentSpeedIndex;
        
        protected  void Start()
        {
            var speedPart = 1f;
            m_speedPart = new float[m_speedButtons.Count];
            var i = 0;
            foreach (var speedButton in m_speedButtons)
            {
                m_speedPart[i] = speedPart;
                var respectivePart = speedPart;
                speedButton.onClick.AddListener(() => SelectSpeed(speedButton, respectivePart));
                if (speedPart == 0)
                    m_currentSpeedIndex = i;
                speedPart -= 0.25f;
                i++;
            }
        }

        private void OnEnable()
        {
            Events.Inputs.OnUpDownChanged += ChangeSpeed;
        }

        private void OnDisable()
        {
            Events.Inputs.OnUpDownChanged -= ChangeSpeed;
        }

        public override void UpdatePanelWithModule(Module module)
        {
            base.UpdatePanelWithModule(module);
            
            m_engineModule = (Engine)module;
            
            if (module is null) return;
            
            m_currentSpeedIndex = m_engineModule.CurrentSpeedIndex;
            UnSelectAllSpeedButton();
            SelectSpeedButton(m_speedButtons[m_currentSpeedIndex]);
        }

        private void Update()
        {
            if (m_engineModule is null) return;
            
            m_currentSpeedText.text = $"{m_engineModule.CurrentSpeed:F1} kts";

            var indicatorY = m_engineModule.CurrentSpeed >= 0 
                ? Mathf.Lerp(m_zeroSpeedPos, m_maxSpeedPos, m_engineModule.CurrentSpeedPercentage) 
                : Mathf.Lerp(m_zeroSpeedPos, m_maxReverseSpeedPos,  (-m_engineModule.CurrentSpeedPercentage) * 4);
            m_currentSpeedIndicator.localPosition = new Vector3(m_currentSpeedIndicator.localPosition.x, indicatorY, 0);
        }

        private void ChangeSpeed(float delta)
        {
            if (m_engineModule is null) return;
            
            m_currentSpeedIndex = Mathf.Clamp(m_currentSpeedIndex -= (int)delta, 0, m_speedButtons.Count - 1);
            SelectSpeed(m_speedButtons[m_currentSpeedIndex], m_speedPart[m_currentSpeedIndex]);
        }

        private void SelectSpeed(Button btn, float speedPart)
        {
            if (m_engineModule is null) return;
            
            m_engineModule.SetTargetSpeedTo(speedPart, m_currentSpeedIndex);
            m_currentSpeedIndex = m_speedButtons.IndexOf(btn);
            UnSelectAllSpeedButton();
            SelectSpeedButton(btn);
        }

        private void SelectSpeedButton(Button btn)
        {
            btn.interactable = false;
            var text = btn.GetComponentInChildren<TextMeshProUGUI>();
            text.color = m_fontColors[1];
            text.fontSize = m_fontSizes[1];
        }

        private void UnSelectAllSpeedButton()
        {
            foreach (var speedButton in m_speedButtons)
            {
                speedButton.interactable = true;
                var text = speedButton.GetComponentInChildren<TextMeshProUGUI>();
                text.color = m_fontColors[0];
                text.fontSize = m_fontSizes[0];
            }
        }
    }
}