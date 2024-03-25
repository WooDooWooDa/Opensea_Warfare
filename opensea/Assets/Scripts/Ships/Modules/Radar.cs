using Assets.Scripts.Common;
using Assets.Scripts.Ships.Common;
using System;
using UnityEngine;

namespace Assets.Scripts.Ships.Modules
{
    public class Radar : Module
    {
        [SerializeField] private Transform m_radarDirection;

        public Action<IDetectable, float, Vector2> OnSpottedShip;

        private float m_rotationSpeed = 180f;
        private float m_range;

        private float m_currentRotation;
        private bool m_isScanning;
        private Coroutine m_scanningRoutine;

        public override void Initialize(Ship attachedShip)
        {
            base.Initialize(attachedShip);
            m_range = attachedShip.Stats.REC;

            //Temp start in init... move it
            StartScan();
        }

        protected override void InternalPreUpdateModule(float deltaTime) 
        {
            
        }

        protected override void InternalUpdateModule(float deltaTime)
        {
            if (!m_isScanning) return;
            
            Scan(m_currentRotation);
            m_currentRotation += m_rotationSpeed * deltaTime;
            m_radarDirection.eulerAngles -= new Vector3(0, 0, m_currentRotation);

            if (m_currentRotation >= 360) m_currentRotation = 0;
        }

        protected override void ApplyState()
        {
            if (CurrentState is DamageState.Destroyed)
            {
                StopScan();
            }
        }

        private void StartScan()
        {
            m_isScanning = true;
        }

        private void StopScan()
        {
            m_isScanning = false;
        }

        private void Scan(float rotation)
        {
            var direction = GetVectorFromAngle(rotation);
            Debug.DrawRay(transform.position, direction, Color.green, 0.2f);
            var hits = Physics2D.RaycastAll(transform.position, direction, m_range);
            foreach (var hit in hits)
            {
                var detectable = hit.collider.gameObject.GetComponent<IDetectable>();
                if (detectable != null)
                {
                    Detect(detectable, hit.distance, direction);
                }
            }
        }

        private void Detect(IDetectable detected, float distance, Vector2 direction)
        {
            if (detected.TryDetected(distance, direction))
            {
                OnSpottedShip?.Invoke(detected, distance, direction);
            }
        }
        
        private static Vector3 GetVectorFromAngle(float rotation)
        {
            return Quaternion.AngleAxis(rotation, Vector3.forward) * Vector3.right; ;
        }

        private void OnDrawGizmos()
        {
            Gizmos.color = Color.green;
            Gizmos.DrawWireSphere(transform.position, m_range);
        }
    }
}
