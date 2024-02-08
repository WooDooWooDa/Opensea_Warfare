using System;
using System.Collections.Generic;
using System.Linq;
using Assets.Scripts.Ships.Common;
using Assets.Scripts.Weapons;
using UnityEngine;

namespace Assets.Scripts.Ships.Modules
{
    public class LoadedAmmunition
    {
        public bool IsDefault;
        public Ammo Ammo;
        public int Quantity;
        public int LoadedInCannons;
    }
    
    public class AmmunitionBay : Module, IDestroyable
    {
        [SerializeField] private List<Ammo> m_defaultAmmoByWeaponType;
        [SerializeField] private int m_bayCapacity;
        
        public int CurrentCapacityUsage => m_weaponsAmmunition.Values.Aggregate(0, (current, ammunitionList) 
            => current + ammunitionList.Aggregate(0, (i, ammunition) 
                => i + ammunition.Value.Quantity * ammunition.Value.Ammo.BaySpace));

        public Action<IDestroyable> OnDestroyed { get; set; }

        private Dictionary<WeaponType, Dictionary<Ammo, LoadedAmmunition>> m_weaponsAmmunition;

        public override void Initialize(Ship attachedShip)
        {
            base.Initialize(attachedShip);
            m_weaponsAmmunition = new Dictionary<WeaponType, Dictionary<Ammo, LoadedAmmunition>>();
            foreach (var weaponType in attachedShip.Stats.weaponSlots.Distinct())
            {
                m_weaponsAmmunition.Add(weaponType, new Dictionary<Ammo, LoadedAmmunition>());
                var defaultAmmoForThisType = m_defaultAmmoByWeaponType.Find(x => x.WeaponTypeFor == weaponType && x.IsDefaultForWeaponType);
                if (defaultAmmoForThisType == null)
                    Debug.LogWarning("[DEV] No default ammo configured for weapon type : " + weaponType);
                else
                    m_weaponsAmmunition[weaponType].Add(defaultAmmoForThisType, new LoadedAmmunition()
                    {
                        IsDefault = true,
                        Ammo = defaultAmmoForThisType
                    });
            }
        }

        public Ammo GetDefaultAmmoFor(WeaponType weaponType)
        {
            if (!m_weaponsAmmunition.TryGetValue(weaponType, out var loadedAmmunitionDict)) return null;

            return loadedAmmunitionDict.Values.First(x => x.IsDefault).Ammo;
        }

        public int GetMaxQuantityFor(Ammo ammo, int quantity)
        {
            if (ammo is null || quantity <= 0) return -1;
            
            var ammunition = GetLoadedAmmunition(ammo);
            if (ammunition == null) return -1;

            if (ammunition.IsDefault) return quantity; //Infinite default ammo

            var availableAmmo = ammunition.Quantity - ammunition.LoadedInCannons;
            if (availableAmmo <= 0) return -1;
                
            return availableAmmo < quantity ? ammunition.Quantity : quantity;
        }

        public void ReserveForWeapon(Ammo ammo, int quantity)
        {
            var ammunition = GetLoadedAmmunition(ammo);
            if (ammunition != null)
            {
                ammunition.LoadedInCannons += quantity;
            }
        }

        public void UnreserveForWeapon(Ammo ammo, int quantity)
        {
            ReserveForWeapon(ammo, -quantity);
        }

        public bool CanLoadAmmoType(Ammo ammo)
        {
            return m_weaponsAmmunition.ContainsKey(ammo.WeaponTypeFor);
        }

        public bool LoadAmmunition(Ammo ammo, int quantity)
        {
            var spaceLeft = m_bayCapacity - CurrentCapacityUsage;
            if (spaceLeft <= 0) return false;
            
            var amountPossibleToLoad = spaceLeft / ammo.BaySpace;
            Load(ammo, quantity < amountPossibleToLoad ? quantity : amountPossibleToLoad);

            return true;
        }

        public bool UnloadAmmunition(Ammo ammo, int quantity = 1)
        {
            var ammunition = GetLoadedAmmunition(ammo);
            if (ammunition is not null && !ammunition.IsDefault)
            {
                ammunition.Quantity -= quantity;
                Debug.Log("Ammunition left : " + ammunition.Quantity);
            }

            return true;
        }
        
        protected override void InternalPreUpdateModule(float deltaTime)
        {
            
        }

        protected override void InternalUpdateModule(float deltaTime)
        {
            
        }

        private bool Load(Ammo ammo, int quantityToLoad)
        {
            var ammunition = GetLoadedAmmunition(ammo);
            if (ammunition is not null)
            {
                if (quantityToLoad < 0 && ammunition.Quantity > quantityToLoad) return false;
                
                ammunition.Quantity += quantityToLoad;
            }
            else
            {
                if (!m_weaponsAmmunition.TryGetValue(ammo.WeaponTypeFor, out var loadedAmmunitionDict)) return false;
                if (quantityToLoad <= 0) return false;
                
                loadedAmmunitionDict.Add(ammo, new LoadedAmmunition() {
                    Ammo = ammo,
                    Quantity = quantityToLoad
                });
            }
            return true;
        }

        private LoadedAmmunition GetLoadedAmmunition(Ammo ammo)
        {
            if (!m_weaponsAmmunition.TryGetValue(ammo.WeaponTypeFor, out var loadedAmmunitionDict)) return null;

            return !loadedAmmunitionDict.TryGetValue(ammo, out var ammunition) ? null : ammunition;
        }
    }
}