using System.Collections.Generic;
using UnityEngine;

namespace Assets.Scripts.Weapons.SOs
{
    [CreateAssetMenu(fileName = "weaponsConfig", menuName = "SO/Weapon/config", order = 0)]
    public class WeaponsPrefabConfig : ScriptableObject
    {
        public Weapon TryGetWeaponOfType(WeaponType type)
        {
            return m_weaponsPrefab.Find(prefab => prefab.Type == type);
        }
        [SerializeField] private List<Weapon> m_weaponsPrefab;
        [SerializeField] private List<Projectile> m_projectilesPrefab;
    }
}