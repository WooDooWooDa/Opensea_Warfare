using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnityEngine;

namespace Assets.Scripts.Weapons.SOs
{
    [CreateAssetMenu(fileName = "NewWeaponStats", menuName = "SO/Weapon/Stats")]
    public class WeaponStats : ScriptableObject
    {
        [Header("Information")]
        public string Name;
        public WeaponType Type;
        public Sprite WeaponSprite;
        public Sprite Icon;
        [Space]
        [Header("Salvo")]
        public ProjectileStats BaseProjectile;
        public float BaseFirepower;
        public float Cooldown;
        public int SalvoCount;
        public float SalvoTimeSpacing;
        [Header("Param")] 
        public float turnSpeed;
        public bool CanAutoFire;
        public float LockInAccuracy;
    }
}
