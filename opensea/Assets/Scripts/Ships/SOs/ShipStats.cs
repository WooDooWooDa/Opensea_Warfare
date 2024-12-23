﻿using Assets.Scripts.Weapons;
using System.Collections.Generic;
using UnityEngine;

namespace Assets.Scripts.Ships.SOs
{
    [CreateAssetMenu(fileName = "NewShipStats", menuName = "SO/Ship/Stats")]
    public class ShipStats : ScriptableObject
    {
        public float HP;
        public float FP; //maybe remove this stat
        public HullType Hull;
        [Tooltip("Divide this value by 100 to obtain ship max speed")]
        public float SPD;
        [Tooltip("Maneuvrability of the ship (turning speed, etc)")]
        public float MAN;
        [Tooltip("Range in unit with 100% accuracy")]
        public float RNG;
        [Tooltip("Divide this value by 10 to obtain radius of dispersion circle at range")]
        public float ACC;
        [Tooltip("Effective range of radar")]
        public float REC;
        [Tooltip("Concealment value, range at which the ship can be detected")]
        public float CON_RNG;
        public float CON_TIME;
        public List<WeaponType> weaponSlots;
    }
}
