using Assets.Scripts.Helpers;
using UnityEngine;

namespace Assets.Scripts.Ships.Modules
{
    public class Engine : ActionModule
    {
        public float CurrentSpeedPercentage => m_currentSpeed / m_shipMaxSpeed;
        public float CurrentSpeed => m_currentSpeed * 100;
        public float TargetPourcentageOfSpeed => m_currentPourcentage;

        private const int AccelerationTime = 8;
        private const int DecelerationTime = 12;
        
        private Transform m_shipTransform;
        private float m_shipMaxSpeed;
        private float m_currentMaxSpeed;
        private float m_currentPourcentage;
        private float m_currentSpeed;
        private float m_currentTargetSpeed;

        public override void Initialize(Ship attachedShip)
        {
            base.Initialize(attachedShip);
            m_shipTransform = attachedShip.transform;
            m_currentMaxSpeed = m_shipMaxSpeed = attachedShip.Stats.SPD / 10;
        }

        public void Stop()
        {
            m_currentPourcentage = 0;
            EvaluateTargetSpeed();
            Events.Ship.FireChangedSpeed(m_ship, m_currentPourcentage);
        }

        public void ChangeSpeed(float value)
        {
            if (value == 0) return;

            m_currentPourcentage += 0.25f * Mathf.Sign(value);
            m_currentPourcentage = Mathf.Clamp(m_currentPourcentage, -0.25f, 1);
            EvaluateTargetSpeed();
            Events.Ship.FireChangedSpeed(m_ship, m_currentPourcentage);
        }

        protected override void RegisterActions()
        {
            m_inputActions.BattleMap.Move.performed += ctx => ChangeSpeed(ctx.ReadValue<Vector2>().y);
        }

        protected override void InternalUpdateModule(float deltaTime)
        {
            var lerpValue = (m_currentTargetSpeed >= m_currentSpeed) ? AccelerationTime : DecelerationTime;
            //todo maybe change the lerp values to eliminate the slow lerp at the end and the fast i nthe beginning
            m_currentSpeed = Mathf.Lerp(m_currentSpeed, ClampSpeed(), deltaTime / lerpValue);

            m_shipTransform.position += (deltaTime * m_currentSpeed * transform.up);
        }

        private void EvaluateTargetSpeed()
        {
            m_currentTargetSpeed = m_shipMaxSpeed * m_currentPourcentage;
            m_currentTargetSpeed = ClampSpeed();
        }

        private float ClampSpeed()
        {
            return Mathf.Clamp(m_currentTargetSpeed, -(m_currentMaxSpeed * 0.25f), m_currentMaxSpeed);
        }
    }
}