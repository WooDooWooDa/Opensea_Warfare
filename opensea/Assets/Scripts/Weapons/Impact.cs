using System.Collections.Generic;
using Assets.Scripts.Ships;
using Assets.Scripts.Weapons.SOs;
using UnityEngine;

namespace Assets.Scripts.Weapons
{
    public class Impact
    {
        public HullPart HullPartHit;
        public float BaseDamage { get; set; }
        public Ammo AmmoUsed { get; set; }
        public WeaponStats FiredFrom { get; set; }
        public Ship Sender { get; set; } //senderData -> to show indicator of direction of sender after hit
    }
}