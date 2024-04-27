using System;
using Assets.Scripts.Ships;
using UnityEngine;

namespace Assets.Scripts.Missions.Objectives.ObjectiveComponents
{
    [RequireComponent(typeof(Collider2D), typeof(SpriteRenderer))]
    public class ObjectiveWaypoint : MonoBehaviour
    {
        [SerializeField] private bool m_showBox;

        public Action OnVisited;
        public bool Visited { get; private set; }

        private bool m_isActive;
        
        private void Start()
        {
            ShowBox(m_showBox);
        }

        public void Activate()
        {
            m_isActive = true;
        }

        public void ShowBox(bool value = true)
        {
            m_showBox = value;
            GetComponent<SpriteRenderer>().enabled = m_showBox;
            if (m_showBox)
            {
                LeanTween.scale(gameObject, Vector3.one * 1.05f, 2.0f).setLoopPingPong();
            }
            else
            {
                LeanTween.cancel(gameObject);
            }
        }
        
        private void OnTriggerEnter2D(Collider2D other)
        {
            if (!m_isActive) return;
            
            var ship = other.gameObject.GetComponent<Ship>();
            if (ship != null && ship.Team == ShipTeam.Player)
            {
                Visited = true;
                OnVisited?.Invoke();
                GetComponent<Collider2D>().enabled = false;
            }
        }
    }
}