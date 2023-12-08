using Assets.Scripts.Weapons;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnityEngine;

namespace Assets.Scripts.Ships.SOs
{
    [CreateAssetMenu(fileName = "NewShipStats", menuName = "SO/Ship/Stats")]
    public class ShipStats : ScriptableObject
    {
        public float HP;
        public float FP;
        public HullType Hull;
        [Tooltip("Divide this value by 10 to obtain ship max speed")]
        public float SPD;
        [Tooltip("Range in unit with 100% accuracy")]
        public float RNG;
        [Tooltip("Divide this value by 10 to obtain radius of dispersion circle at range")]
        public float ACC;
        public List<WeaponType> weaponSlots;
    }
}
