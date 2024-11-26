using System;
using System.Collections.Generic;
using Assets.Scripts.Ships.Common;
using Assets.Scripts.Ships.Modules;
using Assets.Scripts.Weapons;
using UnityEngine;

namespace Assets.Scripts.Ships
{
    public class HullPart : MonoBehaviour, IDamageable, IHittablePart
    {
        [SerializeField] private float m_hullProportion;
        [SerializeField] private AnimationCurve m_armorMultProbability;
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
            //var rand = Random.value; //todo based on angle of incidence???
            //var dmgMult = m_armorMultProbability.Evaluate(rand);
            CurrentHp -= impact.BaseDamage; //* dmgMult;
            if (CurrentHp <= 0)
            {
                CurrentHp = 0;
                CurrentState = DamageState.Destroyed;
            }
            return health - CurrentHp;
        }

        private void OnTriggerEnter2D(Collider2D other)
        {
            if (other.gameObject.layer == LayerMask.GetMask("Land"))
            {
                
            }
        }
        
        private void OnTriggerExit2D(Collider2D other)
        {
            if (other.gameObject.layer == LayerMask.GetMask("Land"))
            {
                
            }
        }
    }
}