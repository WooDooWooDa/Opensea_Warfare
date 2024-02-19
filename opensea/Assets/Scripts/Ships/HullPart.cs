using System;
using System.Collections.Generic;
using Assets.Scripts.Ships.Common;
using Assets.Scripts.Ships.Modules;
using Assets.Scripts.Weapons;
using UnityEngine;

namespace Assets.Scripts.Ships
{
    public class HullPart : MonoBehaviour, IDamageable
    {
        [SerializeField] private float m_hullProportion;
        [SerializeField] private List<ModuleType> m_relatedModules;
        
        public float CurrentHp { get; set; }
        public DamageState CurrentState { get; set; }
        public List<ModuleType> RelatedModules => m_relatedModules;
        public Action<IDamageable, DamageState> OnStateChanged { get; set; }
        public Action<IDamageable, float> OnDamageTaken { get; set; }

        private Hull m_shipHull;
        
        public void InitPart(Hull hull, float hpPart)
        {
            m_shipHull = hull;
            CurrentHp = hpPart * m_hullProportion;
        }
        
        public float DamageOnImpact(Impact impact)
        {
            var health = CurrentHp;
            CurrentHp -= impact.BaseDamage;
            if (CurrentHp <= 0)
            {
                CurrentHp = 0;
                CurrentState = DamageState.Destroyed;
            }
            return health - CurrentHp;
        }
    }
}