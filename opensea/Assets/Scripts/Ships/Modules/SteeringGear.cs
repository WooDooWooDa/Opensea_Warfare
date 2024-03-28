using Assets.Scripts.Helpers;
using UnityEngine;

namespace Assets.Scripts.Ships.Modules
{
    public class SteeringGear : ActionModule
    {
        public float CurrentAngle => m_shipTransform.rotation.eulerAngles.z;

        private float m_targetAngle;
        
        private float m_currentPercentage;

        private Transform m_shipTransform;
        private Engine m_engine;
        private float m_shipMan;

        public override void Initialize(Ship attachedShip)
        {
            base.Initialize(attachedShip);
            m_shipTransform = attachedShip.transform;
            m_engine = attachedShip.GetModuleOfType<Engine>();
            m_shipMan = attachedShip.Stats.MAN / 5;
        }

        protected override void RegisterActions()
        {
            m_inputActions.BattleMap.Move.performed += ctx => ProcessInput();
        }

        public void SetTargetAngle(float angle)
        {
            
        }

        public void ResetCourse()
        {
            m_currentPercentage = 0;
        }

        protected override void InternalPreUpdateModule(float deltaTime)
        {
        }

        protected override void InternalUpdateModule(float deltaTime)
        {
            m_shipTransform.Rotate(Vector3.forward * (m_shipMan * m_currentPercentage * m_engine.CurrentSpeedPercentage * deltaTime), Space.Self);
        }
        
        private void ChangeSpeedV2(float value)
        {
            m_currentPercentage += 0.25f * Mathf.Sign(value);
            m_currentPercentage = Mathf.Clamp(m_currentPercentage, -1, 1);
        }
        
        private void ProcessInput()
        {
            var vec = m_inputActions.BattleMap.Move.ReadValue<Vector2>();
            if (vec.x != 0)
            {
                ChangeSpeedV2(-vec.x);
            }
        }
    }
}
