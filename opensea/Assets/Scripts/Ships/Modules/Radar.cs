using Assets.Scripts.Common;
using Assets.Scripts.Ships.Common;
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Assets.Scripts.Ships.Modules
{
    public class Radar : Module
    {
        [SerializeField] private Transform m_radarDirection;

        public Action<IDetectable, float, Vector2> OnSpotted;

        private Dictionary<IDetectable, Coroutine> m_detectedFallout = new Dictionary<IDetectable, Coroutine>();

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
            if (CurrentState is DamageState.Destroyed)
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
            }
        }

        protected override void InternalUpdateModule(float deltaTime) { }

        protected override void ApplyState()
        {
            if (CurrentState is DamageState.Destroyed)
            {
                StopScan();
                StopAllCoroutines();
                m_detectedFallout.Clear();
            }
        }

        private void StartScan()
        {
            if (m_scanningRoutine != null && m_isScanning) return;

            m_scanningRoutine = StartCoroutine(Scanning());
        }

        private void StopScan()
        {
            StopCoroutine(m_scanningRoutine);
            m_isScanning = false;
        }

        private IEnumerator Scanning()
        {
            m_isScanning = true;
            var rotation = 0f;
            while (rotation != 360f)
            {
                Scan(rotation);
                rotation += m_rotationSpeed * Time.deltaTime;
                m_radarDirection.eulerAngles -= new Vector3(0, 0, rotation);
                yield return null;
            }

            StartScan();
        }

        private void Scan(float rotation)
        {
            var direction = GetVectorFromAngle(rotation);
            Debug.DrawRay(transform.position, direction, Color.green, 0.5f);
            var hits = Physics2D.RaycastAll(transform.position, direction, m_range);
            foreach (var hit in hits)
            {
                var detectable = hit.collider.gameObject.GetComponent<IDetectable>();
                if (detectable != null)
                {
                    if (detectable as Ship != m_ship) //add a team filter
                        Detect(detectable, hit.distance, direction);
                }
            }
        }

        private void Detect(IDetectable detected, float distance, Vector2 direction)
        {
            if (!m_detectedFallout.ContainsKey(detected))
            {
                OnSpotted?.Invoke(detected, distance, direction);
                detected.OnDetected?.Invoke(distance, direction);
                Debug.Log("After Spotted event!!");
            }
            else
            {
                StopCoroutine(m_detectedFallout[detected]);
                m_detectedFallout.Remove(detected);
            }
            m_detectedFallout.Add(detected, StartCoroutine(ClearDetected(detected)));
        }

        private IEnumerator ClearDetected(IDetectable deteccted)
        {
            yield return new WaitForSeconds(5f);
            m_detectedFallout.Remove(deteccted);
        }

        private static Vector3 GetVectorFromAngle(float rotation)
        {
            return Quaternion.AngleAxis(rotation, Vector3.forward) * Vector3.right; ;
        }
    }
}
