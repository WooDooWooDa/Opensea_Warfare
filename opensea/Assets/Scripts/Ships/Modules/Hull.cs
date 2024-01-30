using System;
using Assets.Scripts.Ships.Common;
using Assets.Scripts.Weapons;

namespace Assets.Scripts.Ships.Modules
{
    public class Hull : Module, IDestroyable
    {
        public Action<IDestroyable> OnDestroyed { get; set; }

        private HullType m_hullType;
        
        public override void Initialize(Ship attachedShip)
        {
            base.Initialize(attachedShip);
            m_hullType = attachedShip.Stats.Hull;
        }

        protected override void ApplyStatus()
        {
            //todo-P2 check for fire
        }

        protected override float InternalCalculateDamage(Impact impactData)
        {
            //todo-P1 calculate armor based on hull and percing charac
            return impactData.BaseDamage;
        }

        protected override void InternalPreUpdateModule(float deltaTime)
        {
            
        }

        protected override void InternalUpdateModule(float deltaTime)
        {
            
        }
    }
}