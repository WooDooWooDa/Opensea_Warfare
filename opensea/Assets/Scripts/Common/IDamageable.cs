using System;
using Assets.Scripts.Weapons;

namespace Assets.Scripts.Ships.Common
{
    public interface IDamageable
    {
        public float CurrentHp { get; set; }
        public DamageState CurrentState { get; set; }

        public Action<IDamageable, DamageState> OnStateChanged { get; set; }
        public Action<IDamageable, float> OnDamageTaken { get; set; }

        public float DamageOnImpact(Impact impact);
    }
}
