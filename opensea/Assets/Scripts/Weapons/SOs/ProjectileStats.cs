using System.Collections.Generic;
using UnityEngine;

namespace Assets.Scripts.Weapons.SOs
{
    [CreateAssetMenu(fileName = "NewProjectileStats", menuName = "SO/Weapon/Projectile", order = 0)]
    public class ProjectileStats : ScriptableObject
    {
        public string Name;
        public Sprite ProjectileSprite;
        public float ProjectileSize;
        public float ProjectileSpeed;
        public List<ProjectileCharacteristic> ProjectileCharacteristics;
    }
}