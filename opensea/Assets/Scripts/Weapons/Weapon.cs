using System;
using System.Collections.Generic;
using Assets.Scripts.Ships;
using Assets.Scripts.Ships.Common;
using Assets.Scripts.Weapons.SOs;
using UnityEngine;

namespace Assets.Scripts.Weapons
{
    public abstract class Weapon: MonoBehaviour, IDestroyable
    {
        [SerializeField] protected List<Transform> m_firePoint;
        public float FirePower => m_stats.BaseFirepower * m_attachedShip.Stats.FP;
        public WeaponStats Stats => m_stats;
        [SerializeField] protected WeaponStats m_stats;
        [SerializeField] private SpriteRenderer m_turretSprite;
        [SerializeField] protected float m_rangeOfRotation = 90f;
        public WeaponType Type => m_stats.Type;
        public bool IsWorking => CurrentState is not DamageState.Disabled and not DamageState.Destroyed;
        public bool ReadyToFire => m_currentCooldown <= 0;
        public float CurrentHp { get; set; }
        public DamageState CurrentState { get; set; }
        public Action<IDamageable, DamageState> OnStateChanged { get; set; }
        public Action<IDamageable, float> OnDamageTaken { get; set; }
        public Action<IDestroyable> OnDestroyed { get; set; }

        protected Ship m_attachedShip;

        private bool m_autoFire;
        protected bool m_hasTarget;
        protected Vector3 m_targetCoord;
        protected Ship m_lockOnShip;

        private float m_currentCooldown;
        
        private void Start()
        {
            if (m_turretSprite != null)
                m_turretSprite.sprite = m_stats.WeaponSprite;

            m_attachedShip = GetComponentInParent<Ship>();
        }

        public void OnImpact(Impact impact)
        {
            
        }

        public void UpdateWeapon(float deltaTime)
        {
            InternalPreUpdateWeapon(deltaTime);
            if (!IsWorking) return;

            UpdateCooldown(deltaTime);
            InternalUpdateWeapon(deltaTime);
        }

        public abstract void LockOn(Ship ship);
        public abstract void SetTargetCoord(Vector3 position);

        public void TryFire(float dispersion)
        {
            //if can fire (reloading, ammunition ??)
            if (m_currentCooldown > 0)
            {
                //send notif weapon not ready
                return;
            }
            
            for (var i = 0; i < m_stats.SalvoCount; i++)
            {
                InternalFire(dispersion);
                //Wait for salvo time
            }

            m_currentCooldown = m_stats.Cooldown;
        }

        protected abstract void InternalFire(float dispersion);
        protected abstract void InternalPreUpdateWeapon(float deltaTime);
        protected abstract void InternalUpdateWeapon(float deltaTime);

        protected Projectile SpawnProjectile()
        {
            var projectile = Instantiate(Main.Instance.WeaponsPrefabConfig.ProjectilePrefab);
            //todo-P2 change so that fire point depends on weapon type nb cannons
            projectile.transform.SetPositionAndRotation(m_firePoint[0].position, m_firePoint[0].rotation);
            return projectile;
        }

        private void UpdateCooldown(float delta)
        {
            m_currentCooldown -= delta;
            if (m_currentCooldown <= 0) m_currentCooldown = 0;
        }
    }
}
