using System;
using System.Collections.Generic;
using System.Linq;
using Assets.Scripts.Helpers;
using Assets.Scripts.Ships.Modules;
using TMPro;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

namespace UI
{
    public class SteeringShipPanel : ShipPanel, IPointerDownHandler
    {
        [SerializeField] private Transform m_shipSilhouette;
        
        [SerializeField] private Slider m_targetAngleSlider;
        [SerializeField] private Transform m_sliderCenterPoint;
        [SerializeField] private Transform m_sliderFill;
        [SerializeField] private Image m_fillImage;
        
        [SerializeField] private TextMeshProUGUI m_directionText;
        
        private SteeringGear m_steeringGear;

        private void OnEnable()
        {
            Events.Inputs.OnSideChanged += ChangeAngle;
        }

        private void OnDisable()
        {
            Events.Inputs.OnSideChanged -= ChangeAngle;
        }

        private void Update()
        {
            if (m_steeringGear is null) return;

            var currentAngle = m_steeringGear.CurrentAngle;
            var targetAngle = m_steeringGear.TargetAngle;
            m_shipSilhouette.rotation = Quaternion.Euler(0, 0, currentAngle);
            m_sliderFill.rotation = Quaternion.Euler(0, 0, targetAngle);
            var diff = targetAngle - currentAngle;
            m_targetAngleSlider.value = diff;
            m_directionText.text = "> " + Helper.GetStringDirection(currentAngle);
        }

        public void OnPointerDown(PointerEventData eventData)
        {
            var direction = (Vector2)Camera.main.WorldToScreenPoint(m_sliderCenterPoint.position) - eventData.position;
            var angle = -(Vector2.Angle(Vector2.right, -direction) - 90);
            float result;
            if (angle > 0)
            {
                if (direction.y > 0)
                {
                    result = 270 + angle;
                }
                else
                {
                    result = 180 + angle;
                }
            }
            else
            {
                if (direction.y > 0)
                {
                    result = 180 + angle;
                }
                else
                {
                    result = -angle;
                }
            }
            SetTargetAngle(result);
        }

        public override void UpdatePanelWithModules(List<Module> modules)
        {
            base.UpdatePanelWithModules(modules);
            
            if (modules == null || !modules.Any()) return;
            
            m_steeringGear = (SteeringGear)modules.Find(m => m.Type == ModuleType.SteeringGear);
        }
        
        private void ChangeAngle(float delta)
        {
            if (m_steeringGear is null) return;
            
            m_steeringGear.ChangeAngle(delta);
        }

        private void SetTargetAngle(float angle)
        {
            if (m_steeringGear is null) return;
            
            m_steeringGear.SetTargetAngle(angle);
        }
    }
}