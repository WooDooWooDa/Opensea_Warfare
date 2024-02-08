using System.Collections.Generic;
using UnityEngine;

namespace Assets.Scripts.Weapons
{
    [CreateAssetMenu(fileName = "AmmoInformation", menuName = "SO/Weapon/Ammo", order = 0)]
    public class Ammo : ScriptableObject
    {
        public string Name;
        public WeaponType WeaponTypeFor;
        public Projectile ProjectilePrefab;
        public Sprite ProjectileSprite;
        public Material TrailMaterial;
        public float ProjectileSize;
        public float ProjectileSpeed;
        public List<ProjectileCharacteristic> ProjectileCharacteristics;
        public bool IsDefaultForWeaponType;
        public int BaySpace = 1;
        [Header("Port")] 
        public float PortBasePrice;
    }
}