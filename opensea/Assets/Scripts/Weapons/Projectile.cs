using Assets.Scripts.Helpers;
using Assets.Scripts.Ships;
using Assets.Scripts.Ships.Common;
using UnityEngine;

namespace Assets.Scripts.Weapons
{
    public class ProjectileData {
        public Ship Sender;
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
        private float m_totalDist;
        private bool m_isParabolic;
        private float m_parabolicTrajectoryLerp;

        public void SetData(ProjectileData data, float firePower)
        {
            m_projectileData = data;
            m_damage = firePower;
            m_isParabolic = data.Ammo.ProjectileCharacteristics.Contains(ProjectileCharacteristic.Parabolic);

            m_projectileData.StartPos = transform.position;
            m_totalDist = Vector3.Distance(transform.position, m_projectileData.TargetPoint);
            m_projectileSprite.sprite = data.Ammo.ProjectileSprite;
            transform.rotation = Helper.SpriteLookAt(transform, m_projectileData.TargetPoint);
            m_projectileSprite.transform.localScale *= data.Ammo.ProjectileSize;
        }

        private void Update()
        {
            if (m_isParabolic && m_parabolicTrajectoryLerp < 1) //todo-2 Not quite right yet
            {
                transform.position = CalculateTrajectory(m_parabolicTrajectoryLerp);
                m_parabolicTrajectoryLerp += Time.deltaTime * (m_projectileData.Ammo.ProjectileSpeed / 4);
            }
            else
            {
                transform.position = Vector3.MoveTowards(transform.position, m_projectileData.TargetPoint, Time.deltaTime * m_projectileData.Ammo.ProjectileSpeed);
            }
            
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
            //get the water or island tile below,
            Destroy(gameObject);
            //play splash/hit animation
        }

        private void OnCollisionEnter2D(Collision2D other)
        {
            if (other.gameObject == m_projectileData.Sender.gameObject) return;
            
            (other as IHittable)?.Hit(new Impact()
            {
                BaseDamage = m_damage,
                Sender = m_projectileData.Sender,
                Characteristics = m_projectileData.Ammo.ProjectileCharacteristics
            });
            
            //play explosion or hit animation
            
            Destroy(gameObject);
        }
    }
}
