using System;

namespace Assets.Scripts.Ships.Common
{
    public interface IDestroyable : IDamageable
    {
        public Action<IDestroyable> OnDestroyed { get; set; }
    }
}