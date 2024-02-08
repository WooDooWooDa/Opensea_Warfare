using System;
using System.Collections.Generic;
using System.Linq;
using Assets.Scripts.Ships;
using Assets.Scripts.Ships.Common;
using Assets.Scripts.Ships.Modules;
using Assets.Scripts.Weapons.SOs;
using UnityEngine;

namespace Assets.Scripts.Weapons
{
    public enum WeaponState
    {
        Empty,
        UnableToReload,
        Reloading,
        Loaded
    }
    
    public abstract class Weapon: MonoBehaviour, IDestroyable
    {
        [SerializeField] protected List<Transform> m_firePoint;
        [SerializeField] protected Transform m_weaponTargetReticule;
        [SerializeField] protected WeaponStats m_stats;
        [SerializeField] private SpriteRenderer m_turretSprite;
        [SerializeField] protected float m_rangeOfRotation = 90f;
        public float FirePower => m_stats.BaseFirepower * m_attachedShip.Stats.FP;
        public WeaponStats Stats => m_stats;
        public WeaponType Type => m_stats.Type;
        //States
        public bool ReadyToFire => WeaponState is WeaponState.Loaded && m_loadedAmmo is not null && InternalReadyToFire();
        public WeaponState WeaponState;
        public float CurrentHp { get; set; }
        public bool IsWorking => CurrentState is not DamageState.Disabled and not DamageState.Destroyed;
        public DamageState CurrentState { get; set; }
        //Action callback
        public Action<IDamageable, DamageState> OnStateChanged { get; set; }
        public Action<IDamageable, float> OnDamageTaken { get; set; }
        public Action<IDestroyable> OnDestroyed { get; set; }

        protected Ship m_attachedShip;
        protected bool m_hasTarget;
        protected Vector3 m_targetCoord;
        protected Ship m_lockOnShip;
        protected Ammo m_loadedAmmo;

        private int m_nbLoaded;
        private Reloader m_reloader;
        private AmmunitionBay m_ammunitionBay;
        
        public void Initialize(Ship attachedShip)
        {
            if (m_turretSprite != null)
                m_turretSprite.sprite = m_stats.WeaponSprite;

            m_attachedShip = attachedShip;

            m_targetCoord = transform.position;
            m_ammunitionBay = (AmmunitionBay)attachedShip.GetModuleOfType(ModuleType.AmmunitionBay);
            m_reloader = new Reloader();
            m_reloader.Initialize(this, m_ammunitionBay);
        }

        public void DamageOnImpact(Impact impact) { }

        public void UpdateWeapon(float deltaTime)
        {
            InternalPreUpdateWeapon(deltaTime);
            if (!IsWorking) return;

            m_weaponTargetReticule.gameObject.SetActive(m_attachedShip.IsSelected);
            InternalUpdateWeapon(deltaTime);
            //VV Move those 2 out of update ? VV
            TryFire();
            TryAutoReload();
        }

        public void LoadAmmo(Ammo loadedAmmo, int nbLoaded)
        {
            m_loadedAmmo = loadedAmmo;
            m_nbLoaded = nbLoaded;
        }

        public void SwitchAmmo(Ammo newAmmo)
        {
            if (m_stats.PossibleAmmo.Contains(newAmmo))
            {
                Debug.Log("Cant load this ammo type in this weapon");
            }
            
            m_reloader.ReloadSwitch(newAmmo);
        }

        public abstract void LockOn(Ship ship);
        public abstract void SetFireTargetCoord(Vector3 position);
        public abstract void Follow(Vector3 position);

        protected abstract void InternalFire(Projectile projectile, float dispersionFactor);
        protected abstract void InternalPreUpdateWeapon(float deltaTime);
        protected abstract void InternalUpdateWeapon(float deltaTime);
        protected abstract bool InternalReadyToFire(); //Determine if weapon is ready to fire (main -> reticule on target, torpedo -> aligned)
        
        private void TryFire()
        {
            if (!m_hasTarget || !ReadyToFire) return;

            Fire(1);
        }

        private void TryAutoReload()
        {
            if (WeaponState is not WeaponState.Empty) return;
            
            m_reloader.Reload();
        }
        
        private void Fire(float dispersionFactor)
        {
            m_ammunitionBay.UnreserveForWeapon(m_loadedAmmo, m_nbLoaded);
            for (var i = 0; i < m_nbLoaded; i++)
            {
                var projectile = SpawnProjectile(i);
                InternalFire(projectile, dispersionFactor);
                m_ammunitionBay.UnloadAmmunition(m_loadedAmmo);
            }

            WeaponState = WeaponState.Empty;
            m_loadedAmmo = null;
        }
        
        private Projectile SpawnProjectile(int firePoint)
        {
            var projectile = Instantiate(m_loadedAmmo.ProjectilePrefab);
            //todo-P2 change so that fire point depends on weapon type nb cannons
            projectile.transform.SetPositionAndRotation(m_firePoint[firePoint].position, m_firePoint[firePoint].rotation);
            return projectile;
        }
    }
}
