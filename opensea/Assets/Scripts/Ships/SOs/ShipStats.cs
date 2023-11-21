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
        public float SPD;
        public float RNG;
        public float ACC;
        public List<WeaponType> weaponSlots;
    }
}
