using UnityEngine;

namespace Assets.Scripts.Ships.Modules
{
    public class SteeringGear : Module
    {
        [SerializeField] private float m_turnSpeed = 4;

        public float CurrentAngle => transform.rotation.eulerAngles.z;
        public float TargetAngle => m_targetAngle;
        
        private float m_targetAngle;
        private const float TurnSpeedStep = 11.25f;

        public void SetTargetAngle(float angle)
        {
            m_targetAngle = angle;
            
        }

        public void ResetCourse()
        {
            transform.rotation.ToAngleAxis(out var shipAngle, out var axis);
            m_targetAngle = shipAngle;
        }
        
        public void ChangeAngle(float delta)
        {
            var nextTargetAngle = m_targetAngle + (-delta * TurnSpeedStep);
            if (nextTargetAngle <= 0)
            {
                nextTargetAngle = 360 + (-delta * TurnSpeedStep);
            }
            m_targetAngle = nextTargetAngle;
        }

        protected override void InternalPreUpdateModule(float deltaTime)
        {
            
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
