using System;
using System.Collections.Generic;
using System.Linq;
using Assets.Scripts.Helpers;
using Assets.Scripts.Ships;
using Assets.Scripts.Ships.Common;
using Assets.Scripts.Ships.Modules;
using Assets.Scripts.Weapons.Projectiles;
using Assets.Scripts.Weapons.SOs;
using TMPro;
using UnityEngine;
using UnityEngine.Serialization;

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
        [SerializeField] public Transform WeaponTransform;
        [SerializeField] protected List<Transform> m_firePoint;
        [SerializeField] protected Transform m_weaponTargetReticule;
        [SerializeField] private TextMeshProUGUI m_targetReticuleNumber;
        [SerializeField] protected WeaponStats m_stats;
        [SerializeField] private SpriteRenderer m_turretSprite;
        [SerializeField] protected float m_rangeOfRotation = 90f;
        public float FirePower => m_stats.BaseFirepower * m_attachedShip.Stats.FP;
        public WeaponStats Stats => m_stats;
        public WeaponType Type => m_stats.Type;
        public int Number
        {
            get => m_weaponNumber;
            set
            {
                m_weaponNumber = value;
                m_targetReticuleNumber.text = m_weaponNumber.ToString();
            }
        }

        //States
        public bool Available => WeaponState is WeaponState.Loaded && !m_fireCommandReceived;
        public WeaponState WeaponState;
        public float CurrentHp { get; set; }
        public bool IsWorking => CurrentState is not DamageState.Disabled and not DamageState.Destroyed;
        public DamageState CurrentState { get; set; }
        //Action callback
        public Action<IDamageable, DamageState> OnStateChanged { get; set; }
        public Action<IDamageable, float> OnDamageTaken { get; set; }
        public Action<IDestroyable> OnDestroyed { get; set; }

        protected Ship m_attachedShip;
        protected Ammo m_loadedAmmo;
        private Reloader m_reloader;
        protected TargetingSystem m_targetingSystem;
        private AmmunitionBay m_ammunitionBay;

        private int m_weaponNumber;
        private int m_nbLoaded;
        private bool ReadyToFire => WeaponState is WeaponState.Loaded && m_loadedAmmo is not null && m_targetingSystem.HasTarget
                                   && m_fireCommandReceived && InternalReadyToFire();
        private bool m_fireCommandReceived;
        
        public void Initialize(Ship attachedShip)
        {
            if (m_turretSprite != null)
                m_turretSprite.sprite = m_stats.WeaponSprite;

            m_attachedShip = attachedShip;
            
            m_ammunitionBay = attachedShip.GetModuleOfType<AmmunitionBay>();
            
            m_reloader = new Reloader();
            m_reloader.Initialize(this, m_ammunitionBay);

            m_targetingSystem = new TargetingSystem();
            m_targetingSystem.Initialize(this);
            
            Events.Ship.IsAiming += (ship, value) =>
            {
                if (ship != m_attachedShip) return;
                m_weaponTargetReticule.gameObject.SetActive(value);
            };
        }

        public float DamageOnImpact(Impact impact)
        {
            return impact.BaseDamage;
        }

        public void UpdateWeapon(float deltaTime)
        {
            InternalPreUpdateWeapon(deltaTime);
            if (!IsWorking) return;

            m_targetingSystem.Update(deltaTime);
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
        
        public bool CanFireAt(Vector3 target) => IsInRangeOfRotation(target);
        
        public virtual void FireAt(Vector3 position)
        {
            m_targetingSystem.SetTarget(position);
            m_fireCommandReceived = true;
        }

        public virtual void LockOn(Ship targetShip)
        {
            if (!m_stats.CanLockOnEnemy) return;
            
            m_targetingSystem.LockOn(targetShip);
            m_fireCommandReceived = true;
        }

        public virtual void Follow(Vector3 position)
        {
            if (m_fireCommandReceived) return;
            
            m_targetingSystem.SetTarget(position);
        }
        
        protected abstract void InternalFire(Projectile projectile, Vector3 at);
        protected abstract void InternalPreUpdateWeapon(float deltaTime);
        protected abstract void InternalUpdateWeapon(float deltaTime);
        protected abstract bool InternalReadyToFire(); //Determine if weapon is ready to fire (main -> reticule on target, torpedo -> aligned)

        protected void LimitRangeOfRotation(Transform turret)
        {
            var currentRotation = turret.localEulerAngles;
            if(currentRotation.z > 180) { currentRotation.z -= 360; }
            currentRotation.z = Mathf.Clamp(currentRotation.z, -m_rangeOfRotation, m_rangeOfRotation);
            turret.localEulerAngles = currentRotation;
        }
        
        private bool IsInRangeOfRotation(Vector3 target)
        {
            var vectorToTarget = target - WeaponTransform.position;
            var targetRotation = Mathf.Abs(Mathf.Atan2(vectorToTarget.y, vectorToTarget.x) * Mathf.Rad2Deg - 90);
            return targetRotation > 180 ? 360 - targetRotation < m_rangeOfRotation : targetRotation < m_rangeOfRotation;
        }
        
        private void TryFire()
        {
            if (!ReadyToFire) return;

            Fire();
        }

        private void TryAutoReload()
        {
            if (WeaponState is not WeaponState.Empty) return;
            
            m_reloader.Reload();
        }
        
        private void Fire()
        {
            m_ammunitionBay.UnreserveForWeapon(m_loadedAmmo, m_nbLoaded);
            for (var i = 0; i < m_nbLoaded; i++)
            {
                var projectile = SpawnProjectile(i);
                InternalFire(projectile, m_targetingSystem.Target);
                m_ammunitionBay.UnloadAmmunition(m_loadedAmmo);
            }

            WeaponState = WeaponState.Empty;
            m_loadedAmmo = null;
            m_targetingSystem.CancelTarget();
            if (!m_targetingSystem.IsLocked) m_fireCommandReceived = false;
        }
        
        private Projectile SpawnProjectile(int firePoint)
        {
            var projectile = Instantiate(m_loadedAmmo.ProjectilePrefab);
            projectile.transform.SetPositionAndRotation(m_firePoint[firePoint].position, m_firePoint[firePoint].rotation);
            return projectile;
        }
    }
}
