using System;
using System.Collections.Generic;
using Assets.Scripts.Ships.Common;
using UnityEngine;

namespace Assets.Scripts.Weapons
{
    public class Projectile: MonoBehaviour
    {
        public float Damage { get; set; }
        public List<ProjectileCharacteristic> Characteristics { get; set; }

        private void OnCollisionEnter2D(Collision2D other)
        {
            (other as IHittable)?.Hit(new Impact(), () => { });
        }
    }
}
