using System;
using Assets.Scripts.Common;
using Assets.Scripts.Helpers;
using Assets.Scripts.Ships.Common;
using UnityEngine;

namespace Assets.Scripts.Weapons.Projectiles
{
    public class ProjectileData {
        public ISender Sender;
        public Ammo Ammo;
        public Vector3 StartPos;
        public Vector3 TargetPoint;
    }
    
    public class Projectile: MonoBehaviour
    {
        [SerializeField] private SpriteRenderer m_projectileSprite;

        private ProjectileData m_projectileData;
        private float m_damage;

        private float m_targetEpsilon = 0.05f;
        private bool m_isParabolic;
        private float m_parabolicTrajectoryLerp;

        public void SetData(ProjectileData data, float firePower)
        {
            m_projectileData = data;
            m_damage = firePower;
            m_isParabolic = data.Ammo.ProjectileCharacteristics.Contains(ProjectileCharacteristic.Parabolic);

            m_projectileData.StartPos = transform.position;
            m_projectileSprite.sprite = data.Ammo.ProjectileSprite;
            transform.rotation = Helper.SpriteLookAt(transform, m_projectileData.TargetPoint);
            m_projectileSprite.transform.localScale *= data.Ammo.ProjectileSize;
        }

        private void Update()
        {
            //OSW-23
            /* if (m_isParabolic && m_parabolicTrajectoryLerp < 1)
            {
                transform.position = CalculateTrajectory(m_parabolicTrajectoryLerp);
                m_parabolicTrajectoryLerp += Time.deltaTime * (m_projectileData.Ammo.ProjectileSpeed / 4);
            } */
            transform.position = Vector3.MoveTowards(transform.position, m_projectileData.TargetPoint, Time.deltaTime * m_projectileData.Ammo.ProjectileSpeed);
            
            if (Vector3.Distance(transform.position, m_projectileData.TargetPoint) <= m_targetEpsilon)
            {
                Miss();
            }
        }

        private Vector3 CalculateTrajectory(float t)
        {
            Vector3 linearProgress = Vector3.Lerp(m_projectileData.StartPos, m_projectileData.TargetPoint, t);
            float perspectiveOffset = Mathf.Sin(t * Mathf.PI);

            Vector3 trajectoryPos = linearProgress + (Vector3.up * perspectiveOffset);
            return trajectoryPos;
        }

        private void Miss()
        {
            Destroy(gameObject);
            //OSW-18 miss anim
        }

        private void OnTriggerEnter2D(Collider2D other)
        {
            if (other.gameObject.GetComponentInParent<ISender>() == m_projectileData.Sender) return; //no return to sender
            
            var hittable = other.gameObject.GetComponentInParent<IHittable>();
            if (hittable is null) return;

            var partHit = other.gameObject.GetComponent<IHittablePart>();
            if (partHit is null) return;
            
            hittable?.Hit(new Impact()
            {
                HullPartHit = partHit,
                BaseDamage = m_damage,
                Sender = m_projectileData.Sender,
                AmmoUsed = m_projectileData.Ammo
            });
            
            //OSW-19 play explosion or hit animation at collision point
            
            Destroy(gameObject);
        }
    }
}
