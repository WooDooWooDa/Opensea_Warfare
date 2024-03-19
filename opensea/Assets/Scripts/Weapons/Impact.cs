using Assets.Scripts.Common;
using Assets.Scripts.Ships.Common;
using Assets.Scripts.Weapons.SOs;

namespace Assets.Scripts.Weapons
{
    public class Impact
    {
        public IHittablePart HullPartHit;
        public float BaseDamage { get; set; }
        public Ammo AmmoUsed { get; set; }
        public WeaponStats FiredFrom { get; set; }
        public IDamageSource DamageSource { get; set; } //senderData -> to show indicator of direction of sender after hit
    }
}