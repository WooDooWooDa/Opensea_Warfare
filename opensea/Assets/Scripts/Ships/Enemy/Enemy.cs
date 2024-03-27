using Assets.Scripts.Managers;
using Assets.Scripts.Ships.Modules;
using UnityEngine;

namespace Assets.Scripts.Ships.Enemy
{
    public class Enemy : MonoBehaviour
    {
        [SerializeField] private Ship m_ship;

        public Ship Ship => m_ship;
        
        private EnemyFleet m_fleet;
        private Concealment m_shipConcealment;
        
        private void Start()
        {
            if (m_ship == null) m_ship = GetComponent<Ship>();
            
            m_shipConcealment = new Concealment();
            m_shipConcealment.Initialize(m_ship);
        }
    }
}