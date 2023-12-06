using Assets.Scripts.Helpers;
using Assets.Scripts.Ships.Modules;
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
        
        private SteeringGear m_steeringGear;
        
        protected  void Start()
        {
            
        }

        private void OnEnable()
        {
            Events.Inputs.OnSideChanged += ChangeAngle;
        }

        private void OnDisable()
        {
            Events.Inputs.OnSideChanged -= ChangeAngle;
        }

        public void OnPointerDown(PointerEventData eventData)
        {
            var direction = (Vector2)Camera.main.WorldToScreenPoint(m_sliderCenterPoint.position) - eventData.position;
            Debug.Log("direction : " + -direction);
            var angle = Vector2.Angle(Vector2.right, direction) - 90;

            // Output the angle
            Debug.Log("Angle between points: " + angle);
            SetTargetAngle(angle);
        }

        public override void UpdatePanelWithModule(Module module)
        {
            base.UpdatePanelWithModule(module);
            
            m_steeringGear = (SteeringGear)module;
            
            if (module is null) return;
            
            
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