using System;
using System.Collections.Generic;
using System.Linq;
using Assets.Scripts.Ships.Common;
using Assets.Scripts.Weapons;
using UnityEngine;

namespace Assets.Scripts.Ships.Modules
{
    public class Hull : Module, IDestroyable
    {
        [SerializeField] private List<HullPart> m_hullParts;
        public Action<IDestroyable> OnDestroyed { get; set; }

        private HullType m_hullType;
        
        public override void Initialize(Ship attachedShip)
        {
            base.Initialize(attachedShip);
            m_hullType = attachedShip.Stats.Hull;
            
            CurrentHp = attachedShip.Stats.HP;
            m_hullParts.ForEach(part => part.InitPart(this, attachedShip.Stats.HP));
        }

        public List<ModuleType> GetRelatedModuleToPart(HullPart part)
        {
            return m_hullParts.Find(x => x == part).RelatedModules;
        }

        protected override void TakeDamage(Impact impactData)
        {
            var dmgReducedByArmor = impactData.BaseDamage * impactData.AmmoUsed.HullTypeDamageModifier[(int)m_hullType];
            var totalDmgTaken = (dmgReducedByArmor / 2);
            totalDmgTaken += impactData.HullPartHit.DamageOnImpact(new Impact()
            {
                BaseDamage = (dmgReducedByArmor / 2)
            });
            
            CurrentHp -= totalDmgTaken;
            if (CurrentHp <= 0) CurrentHp = 0;
            
            OnDamageTaken?.Invoke(this, totalDmgTaken);
        }

        protected override void ApplyStatus()
        {
            //OSW-9 check for status effect application
        }

        protected override void ApplyState()
        {
            if (CurrentState is DamageState.Destroyed)
            {
                OnDestroyed?.Invoke(this);
            }
        }

        protected override void InternalPreUpdateModule(float deltaTime)
        {
            
        }

        protected override void InternalUpdateModule(float deltaTime)
        {
            
        }
    }
}