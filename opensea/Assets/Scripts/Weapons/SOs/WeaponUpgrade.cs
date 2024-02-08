using System;
using System.Collections.Generic;
using UnityEngine;

namespace Assets.Scripts.Weapons.SOs
{
    [CreateAssetMenu(fileName = "NewWeaponUpgrade", menuName = "SO/Weapon/Upgrade")]
    public class WeaponUpgrade : ScriptableObject
    {
        public string Name;
        public WeaponType ForType;
        public List<WeaponStats> Options;
        public WeaponUpgrade NextUpgrade;
    }
}
