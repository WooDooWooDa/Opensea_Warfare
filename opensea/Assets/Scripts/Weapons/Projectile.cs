using System;
using System.Collections.Generic;
using UnityEngine;

namespace Assets.Scripts.Weapons
{
    public class Projectile: MonoBehaviour
    {
        public float Damage { get; set; }
        public List<ProjectileCharacteristic> Characteristics { get; set; }
    }
}
