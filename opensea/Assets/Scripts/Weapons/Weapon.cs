using System;
using Assets.Scripts.Weapons.SOs;
using UnityEngine;

namespace Assets.Scripts.Weapons
{
    public abstract class Weapon: MonoBehaviour
    {
        [SerializeField] private WeaponStats m_stats;

        public WeaponType Type => m_stats.Type;
        
        public abstract void SetTargetCoord(Vector3 position);

    }
}
