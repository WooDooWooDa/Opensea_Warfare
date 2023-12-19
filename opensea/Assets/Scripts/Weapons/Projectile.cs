using System;
using System.Collections.Generic;
using Assets.Scripts.Ships;
using Assets.Scripts.Ships.Common;
using Assets.Scripts.Weapons.SOs;
using UnityEngine;
using UnityEngine.UI;

namespace Assets.Scripts.Weapons
{
    public class Projectile: MonoBehaviour
    {
        [SerializeField] private SpriteRenderer m_projectileSprite; 
        
        private Ship m_sender;
        private ProjectileStats m_stats;
        private Vector3 m_targetPoint;
        private float m_damage;

        private float m_targetEpsilon = 0.1f;

        private void Start()
        {
            //get engine ???
            // get steering gear ???
            // get auto nav ????
        }

        public void SetData(Ship sender, ProjectileStats stats, Vector3 targetPoint, float firePower)
        {
            m_sender = sender;
            m_stats = stats;
            m_targetPoint = targetPoint;
            m_damage = firePower;

            m_projectileSprite.sprite = m_stats.ProjectileSprite;
            m_projectileSprite.transform.localScale *= m_stats.ProjectileSize;
        }

        private void Update()
        {
            //replace with an engine ?! doesnt make sense but
            Vector3.MoveTowards(transform.position, m_targetPoint, Time.deltaTime * m_stats.ProjectileSpeed);
            
            if (Vector3.Distance(transform.position, m_targetPoint) <= m_targetEpsilon)
            {
                Miss();
            }
        }

        private void Miss()
        {
            //get the water or island tile below,
            Destroy(this);
            //play splash/hit animation
        }

        private void OnCollisionEnter2D(Collision2D other)
        {
            if (other.gameObject == m_sender.gameObject) return;
            
            (other as IHittable)?.Hit(new Impact()
            {
                BaseDamage = m_damage,
                Sender = m_sender,
                Characteristics = m_stats.ProjectileCharacteristics
            });
            
            //play explosion or hit animation
            
            Destroy(this);
        }
    }
}
