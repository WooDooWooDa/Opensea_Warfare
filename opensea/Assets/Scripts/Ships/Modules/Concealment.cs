using Assets.Scripts.Common;
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Assets.Scripts.Ships.Modules
{
    public class Concealment : Module, IDetectable
    {
        [SerializeField] private List<Sprite> m_shipSprite;
        
        public Action<float, Vector3> OnDetected { get; set; }

        private float m_detectableRange;
        private float m_detectableTime;

        private bool m_isDetected;
        private Coroutine m_detectedFalloutTimer;

        public override void Initialize(Ship attachedShip)
        {
            m_detectableRange = attachedShip.Stats.CON_RNG;
            m_detectableTime = attachedShip.Stats.CON_TIME;
        }
        public bool TryDetected(float dist, Vector2 dir)
        {
            if (!(dist <= m_detectableRange)) return false;
            
            Detect(dist, dir);
            return true;
        }

        protected override void InternalPreUpdateModule(float deltaTime)
        {
            
        }

        protected override void InternalUpdateModule(float deltaTime)
        {
            
        }

        private void Detect(float dist, Vector2 dir)
        {
            OnDetected?.Invoke(dist, dir);
            m_detectedFalloutTimer = StartCoroutine(DetectionFallout());
        }

        private IEnumerator DetectionFallout()
        {
            yield return new WaitForSeconds(m_detectableTime);
            m_isDetected = false;
        }
    }
}
