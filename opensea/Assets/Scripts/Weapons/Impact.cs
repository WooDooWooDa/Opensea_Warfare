using System.Collections.Generic;
using Assets.Scripts.Ships;

namespace Assets.Scripts.Weapons
{
    public class Impact
    {
        public float BaseDamage { get; set; }
        public List<ProjectileCharacteristic> Characteristics { get; set; }
        public Ship Sender { get; set; } //senderData -> to show indicator of direction of sender after hit
    }
}