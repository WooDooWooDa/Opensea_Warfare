using Assets.Scripts.Managers;
using UnityEngine;

namespace Assets.Scripts.Ships.Enemy
{
    public class Enemy : MonoBehaviour
    {
        [SerializeField] private Ship m_ship;

        public Ship Ship => m_ship;
        
        private EnemyFleet m_fleet;
        
        private void Start()
        {
            if (m_ship == null) m_ship = GetComponent<Ship>();
        }
    }
}