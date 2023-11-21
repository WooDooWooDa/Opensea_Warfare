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
        public string Name;
        public WeaponType Type;
        public Sprite Icon;
        [Header("Projectile")]
        public Sprite ProjectileSprite;
        public float ProjectileSize;
        public float ProjectileSpeed;
        public List<ProjectileCharacteristic> ProjectileCharacteristics;
        [Header("Salvo")]
        public int SalvoCount;
        public float SalvoSpacing;
        public SalvoType SalvoType;
    }
}
