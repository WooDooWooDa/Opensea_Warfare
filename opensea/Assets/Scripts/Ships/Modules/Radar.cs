using Assets.Scripts.Common;
using Assets.Scripts.Ships.Common;
using System;
using System.Collections;
using System.Linq;
using UnityEngine;

namespace Assets.Scripts.Ships.Modules
{
    public class Radar : Module
    {
        [SerializeField] private Transform m_radarDirection;

        public Action<IDetectable, float, Vector2> OnSpottedShip;

        private float m_rotationSpeed = 180f;
        private float m_range;

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
            /*if (CurrentState is DamageState.Destroyed)
            {
                //small range detection of ships
                var colliders = Physics2D.OverlapCircleAll(transform.position, m_range / 2);
                foreach (var hit in colliders)
                {
                    var detectable = hit.gameObject.GetComponent<IDetectable>();
                    if (detectable != null)
                    {
                        if (detectable as Ship != m_ship) //add a team filter
                        {
                            var positionDiff = transform.position - hit.transform.position;
                            Detect(detectable, positionDiff.magnitude, positionDiff);
                        }
                    }
                }
            }*/
        }

        protected override void InternalUpdateModule(float deltaTime) { }

        protected override void ApplyState()
        {
            if (CurrentState is DamageState.Destroyed)
            {
                StopScan();
            }
        }

        private void StartScan()
        {
            if (m_scanningRoutine != null || m_isScanning) return;

            m_scanningRoutine = StartCoroutine(Scanning());
            m_isScanning = true;
        }

        private void StopScan()
        {
            StopCoroutine(m_scanningRoutine);
            m_isScanning = false;
        }

        private IEnumerator Scanning()
        {
            var rotation = 0f;
            while (m_isScanning)
            {
                Scan(rotation);
                rotation += m_rotationSpeed * Time.deltaTime;
                m_radarDirection.eulerAngles -= new Vector3(0, 0, rotation);
                yield return null;

                if (rotation >= 360) rotation = 0;
            }
        }

        private void Scan(float rotation)
        {
            var direction = GetVectorFromAngle(rotation);
            var hits = Physics2D.RaycastAll(transform.position, direction, m_range);
            foreach (var hit in hits)
            {
                if (hit.collider.gameObject.GetComponentInParent<Ship>() == m_ship) continue;
                
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
    }
}
