using System.Collections;
using Assets.Scripts.Ships.Modules;
using UnityEngine;

namespace Assets.Scripts.Weapons
{
    public class Reloader
    {
        private int m_nbCurrentlyLoading;
        private float m_targetCooldown;
        private float m_currentCooldown;

        private Ammo m_ammoToLoad;
        private Ammo m_currentlyLoadingAmmo;
        private Ammo m_lastAmmoReloaded;
        private Weapon m_associatedWeapon;

        private AmmunitionBay m_ammunitionBay;

        public void Initialize(Weapon weapon, AmmunitionBay bay)
        {
            m_associatedWeapon = weapon;
            m_ammunitionBay = bay;
            m_lastAmmoReloaded = m_ammunitionBay.GetDefaultAmmoFor(m_associatedWeapon.Type);
        }
        
        public void Reload()
        {
            if (m_lastAmmoReloaded is null)
            {
                m_lastAmmoReloaded = m_ammunitionBay.GetDefaultAmmoFor(m_associatedWeapon.Type);
            }
            
            m_ammoToLoad = m_lastAmmoReloaded;
            InternalReload(m_associatedWeapon.Stats.Cooldown);
        }

        public void ReloadSwitch(Ammo newAmmoType)
        {
            if (m_associatedWeapon.WeaponState is WeaponState.Reloading)
            {
                //Reloading
                if (m_currentlyLoadingAmmo == newAmmoType || !CanReload(newAmmoType)) return; //trying to switch to same ammo type or cant reload new ammo
                
                if (m_currentlyLoadingAmmo is not null)
                    m_ammunitionBay.UnreserveForWeapon(m_currentlyLoadingAmmo, m_nbCurrentlyLoading);
                //Only change the currently loading ammo
                m_currentlyLoadingAmmo = newAmmoType;
            }
            else
            {
                //Is loaded or unable to load other type
                if (m_lastAmmoReloaded == newAmmoType) Reload();
                else
                {
                    //switching
                    m_ammoToLoad = newAmmoType;
                    InternalReload(m_associatedWeapon.Stats.SwitchCooldown);
                }
            }
        }
        
        private void InternalReload(float time)
        {
            if (!CanReload(m_ammoToLoad)) return;

            m_nbCurrentlyLoading = m_ammunitionBay.GetMaxQuantityFor(m_ammoToLoad, m_associatedWeapon.Stats.CannonCount);
            
            m_associatedWeapon.StartCoroutine(ReloadCoroutine(time));
        }

        private bool CanReload(Ammo ammoToLoad)
        {
            if (!m_ammunitionBay.CanLoadAmmoType(ammoToLoad))
            {
                Debug.Log("Reloader " + this + " cannot reload this type of ammo rn");
                m_associatedWeapon.WeaponState = WeaponState.UnableToReload;
                return false;
            }
            var nbPossibleToReload = m_ammunitionBay.GetMaxQuantityFor(ammoToLoad, m_associatedWeapon.Stats.CannonCount);
            if (nbPossibleToReload == -1)
            {
                Debug.Log("Reloader " + this + " cannot reload this type of ammo rn");
                m_associatedWeapon.WeaponState = WeaponState.UnableToReload;
                return false;
            }

            return true;
        }

        private IEnumerator ReloadCoroutine(float time)
        {
            StartReloading();
            m_targetCooldown = time;
            m_currentCooldown = 0;
            while (m_currentCooldown < m_targetCooldown)
            {
                m_currentCooldown += Time.deltaTime;
                yield return null;
            }
            FinishReloading();
        }

        private void StartReloading()
        {
            m_ammunitionBay.ReserveForWeapon(m_ammoToLoad, m_nbCurrentlyLoading);
            m_currentlyLoadingAmmo = m_ammoToLoad;
            m_ammoToLoad = null;
            m_associatedWeapon.WeaponState = WeaponState.Reloading;
        }

        private void FinishReloading()
        {
            m_associatedWeapon.LoadAmmo(m_currentlyLoadingAmmo, m_nbCurrentlyLoading);
            m_associatedWeapon.WeaponState = WeaponState.Loaded;
            
            m_lastAmmoReloaded = m_currentlyLoadingAmmo;
            m_currentlyLoadingAmmo = null;
        }
    }
}