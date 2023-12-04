using UnityEngine;

namespace Assets.Scripts.Ships.Modules
{
    public class Engine : Module
    {
        [SerializeField] private float m_maxSpeed;

        public float CurrentSpeedPercentage => m_currentSpeed / m_maxSpeed;
        public float CurrentSpeed => m_currentSpeed * 100;
        public int CurrentSpeedIndex { get; private set; }

        private const int NbSpeedStep = 4;
        private const int AccelerationTime = 4;
        private const int DecelerationTime = 6;
        
        private Transform m_shipTransform;
        private float m_currentSpeed;
        private float m_currentTargetSpeed;
        private float m_currentMaxSpeed;

        private void Awake()
        {
            CurrentSpeedIndex = 4;
            m_shipTransform = transform;
            m_currentMaxSpeed = m_maxSpeed;
        }

        protected override void OnEnableModule()
        {
            
        }

        protected override void OnDisableModule()
        {
            
        }

        public void SetTargetSpeedTo(float speedPart, int speedIndex)
        {
            CurrentSpeedIndex = speedIndex;
            m_currentTargetSpeed = m_maxSpeed * speedPart;
            m_currentTargetSpeed = ClampTargetSpeed();
        }
        
        private void ChangeTargetSpeed(float delta)
        {
            var step = (m_maxSpeed / NbSpeedStep) * delta;
            m_currentTargetSpeed += step;
            m_currentTargetSpeed = ClampTargetSpeed();
        }

        protected override void InternalUpdateModule(float deltaTime)
        {
            var lerpValue = (m_currentTargetSpeed >= m_currentSpeed) ? AccelerationTime : DecelerationTime;
            m_currentSpeed = Mathf.Lerp(m_currentSpeed, ClampTargetSpeed(), deltaTime / lerpValue);
            
            m_shipTransform.position += (deltaTime * m_currentSpeed * transform.up);
        }

        protected override void ApplyState()
        {
            
        }

        private float ClampTargetSpeed()
        {
            return Mathf.Clamp(m_currentTargetSpeed, -m_maxSpeed / NbSpeedStep, m_currentMaxSpeed);
        }
    }
}