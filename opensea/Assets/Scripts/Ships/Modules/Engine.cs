using Assets.Scripts.Helpers;
using UnityEngine;

namespace Assets.Scripts.Ships.Modules
{
    public class Engine : Module
    {
        [SerializeField] private float m_maxSpeed;

        private const int NbSpeedStep = 4;
        private const int LerpTime = 6;
        
        private Transform m_shipTransform;
        private float m_currentSpeed;
        private float m_currentTargetSpeed;
        private float m_currentMaxSpeed;
        
        private void Awake()
        {
            m_shipTransform = transform;
            m_currentMaxSpeed = m_maxSpeed;
        }

        protected override void OnEnableModule()
        {
            Events.Inputs.OnUpDownChanged += ChangeSpeed;
        }

        protected override void OnDisableModule()
        {
            Events.Inputs.OnUpDownChanged -= ChangeSpeed;
        }

        public void SetTargetSpeedTo()
        {
            
        }
        
        private void ChangeSpeed(float delta)
        {
            var step = (m_maxSpeed / NbSpeedStep) * delta;
            m_currentTargetSpeed += step;
            m_currentTargetSpeed = Mathf.Clamp(m_currentTargetSpeed, 0, m_currentMaxSpeed);
        }

        protected override void InternalUpdateModule(float deltaTime)
        {
            var lerpValue = (m_currentTargetSpeed >= m_currentSpeed) ? LerpTime / 2 : LerpTime;
            
            m_currentSpeed = Mathf.Lerp(m_currentSpeed, Mathf.Clamp(m_currentTargetSpeed, 0, m_currentMaxSpeed), deltaTime / lerpValue);
            if (m_currentSpeed > 0) {
                m_shipTransform.position += (deltaTime * m_currentSpeed * transform.up);
            }
        }

        protected override void ApplyState()
        {
            
        }
    }
}