using System;
using Assets.Scripts.Ships.Common;

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

        protected override float InternalCalculateDamage(float amount)
        {
            //todo-P1 calculate armor based on hull and percing charac
            return amount;
        }

        protected override void InternalPreUpdateModule(float deltaTime)
        {
            
        }

        protected override void InternalUpdateModule(float deltaTime)
        {
            
        }
    }
}