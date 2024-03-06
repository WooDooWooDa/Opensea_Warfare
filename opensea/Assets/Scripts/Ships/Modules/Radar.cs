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

        public Action<Ship, float, Vector2> OnSpottedShip;

        private Dictionary<Ship, Coroutine> m_detectedShipFallout = new Dictionary<Ship, Coroutine>();

        private float m_rotationSpeed = 180f;
        private float m_range;

        private bool m_isScanning;
        private Coroutine m_scanningRoutine;

        public override void Initialize(Ship attachedShip)
        {
            base.Initialize(attachedShip);
            m_range = attachedShip.Stats.REC;
            StartScan();
            OnSpottedShip += (ship, dist, dir) =>
            {
                Debug.Log(ship + " spotted at " + dist + "m");
            };
        }

        protected override void InternalPreUpdateModule(float deltaTime) { }

        protected override void InternalUpdateModule(float deltaTime)
        {
            //small range detection of ships
            var colliders = Physics2D.OverlapCircleAll(transform.position, m_range / 2);
            if (colliders.Length > 1)
            {
                foreach (var col in colliders)
                {
                    var shipHit = col.gameObject.GetComponent<Ship>();
                    if (shipHit == null) continue;

                    var positionDiff = transform.position - col.transform.position;
                    //DetectShip(shipHit, positionDiff.magnitude, positionDiff);
                }
            }
        }

        protected override void ApplyState()
        {
            if (CurrentState is DamageState.Destroyed)
            {
                StopScan();
                StopAllCoroutines();
                m_detectedShipFallout.Clear();
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
            var hit = Physics2D.Raycast(transform.position, direction, m_range);
            if (hit.collider != null)
            {
                var shipHit = hit.collider.gameObject.GetComponent<Ship>();
                if (shipHit != null && shipHit != m_ship)
                    DetectShip(shipHit, hit.distance, direction);
            }
        }

        private void DetectShip(Ship ship, float distance, Vector2 direction)
        {
            if (!m_detectedShipFallout.ContainsKey(ship))
            {
                OnSpottedShip?.Invoke(ship, distance, direction);
            }
            else
            {
                StopCoroutine(m_detectedShipFallout[ship]);
                m_detectedShipFallout.Remove(ship);
            }
            m_detectedShipFallout.Add(ship, StartCoroutine(ClearDetectedShip(ship)));
        }

        private IEnumerator ClearDetectedShip(Ship ship)
        {
            yield return new WaitForSeconds(5f);
            m_detectedShipFallout.Remove(ship);
        }

        private static Vector3 GetVectorFromAngle(float rotation)
        {
            return Quaternion.AngleAxis(rotation, Vector3.forward) * Vector3.right; ;
        }
    }
}
