using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

namespace Assets.Scripts.Ships.Modules
{
    public class Concealment
    {
        private Ship m_attachedShip;
        
        private float m_detectableRange;
        private float m_detectableTime;

        private bool m_isVisible;
        private Coroutine m_detectedFalloutTimer;
        
        private List<SpriteRenderer> m_shipSprites;

        public void Initialize(Ship attachedShip)
        {
            m_attachedShip = attachedShip;
            m_detectableRange = attachedShip.Stats.CON_RNG;
            m_detectableTime = attachedShip.Stats.CON_TIME;
            m_shipSprites = attachedShip.GetComponentsInChildren<SpriteRenderer>().ToList();
            
            attachedShip.OnTryDetect += (dist, dir) => TryDetected(dist, dir);
        }
        
        public bool TryDetected(float dist, Vector2 dir)
        {
            if (!(dist <= m_detectableRange)) return false;
            
            Detect(dist, dir);
            return true;
        }

        public void Detect(float dist, Vector2 dir)
        {           
            FadeIn();
            m_attachedShip.OnDetected?.Invoke(m_attachedShip, dist, dir);
            if (m_detectedFalloutTimer != null)
                m_attachedShip.StopCoroutine(m_detectedFalloutTimer);
            m_detectedFalloutTimer = m_attachedShip.StartCoroutine(DetectionFallout());
            m_isVisible = true;
        }

        public void Conceal()
        {
            FadeOut();
            m_attachedShip.OnConceal?.Invoke(m_attachedShip);
            m_isVisible = false;
        }
        
        private IEnumerator DetectionFallout()
        {
            yield return new WaitForSeconds(m_detectableTime);
            Conceal();
        }
        
        private void FadeIn()
        {
            if (m_isVisible) return;
            LeanTween.value(m_attachedShip.gameObject, SetSpritesAlpha, 0f, 1f, 0.5f);
        }
   
        private void FadeOut()
        {
            if (!m_isVisible) return;
            LeanTween.value(m_attachedShip.gameObject, SetSpritesAlpha, 1f, 0f, 0.5f);
        }
   
        private void SetSpritesAlpha( float val )
        {
            m_shipSprites.ForEach(s => s.color = new Color(1,1,1,val));
        }
    }
}
