using Assets.Scripts.Helpers;
using UnityEngine;

namespace Assets.Scripts.Ships.Modules
{
    public class SteeringGear : Module
    {
        [SerializeField] private float m_turnSpeed = 4;
        
        private float m_targetAngle;
        private const int TurnSpeedStep = 2;

        protected override void OnEnableModule()
        {
            Events.Inputs.OnSideChanged += ChangeAngle;
        }

        protected override void OnDisableModule()
        {
            Events.Inputs.OnSideChanged -= ChangeAngle;
        }
        
        private void ChangeAngle(float delta)
        {
            var nextTargetAngle = m_targetAngle + (-delta * TurnSpeedStep);
            if (nextTargetAngle <= 0)
            {
                nextTargetAngle = 360 + (-delta * TurnSpeedStep);
            }
            m_targetAngle = nextTargetAngle;
        }

        public void ResetCourse()
        {
            transform.rotation.ToAngleAxis(out var shipAngle, out var axis);
            m_targetAngle = shipAngle;
        }

        protected override void InternalUpdateModule(float deltaTime)
        {
            if (m_targetAngle >= 0) {
                Quaternion angleAxis = Quaternion.AngleAxis(m_targetAngle, Vector3.forward);
                transform.rotation = Quaternion.RotateTowards(transform.rotation, angleAxis, deltaTime * m_turnSpeed);
            }
        }
    }
}
