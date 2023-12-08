using System;
using System.Collections.Generic;
using System.Linq;
using System.Numerics;
using Assets.Scripts.Weapons;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.Serialization;
using Vector2 = UnityEngine.Vector2;
using Vector3 = UnityEngine.Vector3;

namespace Assets.Scripts.Ships.Modules
{
    [RequireComponent(typeof(Armaments))]
    public class FireControl : Module, IPointerClickHandler
    {
        [SerializeField] private Transform m_targetReticule;
        [SerializeField] private Transform m_projectedReticule;
        [SerializeField] private CircleCollider2D m_rangeCollider;
        [SerializeField] private LayerMask m_obstacleLayer;
        [SerializeField] private LayerMask m_rangeLayer;
        private Armaments m_armamentsModule;

        private Vector3 m_startingReticuleScale;
        private float m_reticuleScaleMult;
        private float m_range;
        [SerializeField] private bool m_isAiming;
        private bool m_isLinked;
        private List<Weapon> m_currentControlledWeapons = new();
        
        public void OnPointerClick(PointerEventData eventData)
        {
            if (!m_currentControlledWeapons.Any()) return;
            
            if (m_isAiming)
            {
                //m_isAiming = !m_isAiming;
                Fire();
            }
            //temp to try
            m_isAiming = !m_isAiming;
        }
        
        public override void Initialize(Ship attachedShip)
        {
            base.Initialize(attachedShip);
            m_range = attachedShip.Stats.RNG;
            m_rangeCollider.radius = m_range * 2;
        }
        
        private void Start()
        {
            m_armamentsModule = GetComponentInChildren<Armaments>();
            m_startingReticuleScale = m_projectedReticule.localScale;
        }

        protected override void InternalPreUpdateModule(float deltaTime)
        {
            m_projectedReticule.gameObject.SetActive(m_isAiming);
        }

        protected override void InternalUpdateModule(float deltaTime)
        {
            if (!m_isAiming) return;

            var pointerPos = Camera.main.ScreenToWorldPoint(Input.mousePosition);
            m_projectedReticule.position = new Vector3(pointerPos.x, pointerPos.y, 0);
            var pointerDistance = Vector3.Distance(transform.position, m_projectedReticule.position);
            
            m_targetReticule.position = new Vector3(pointerPos.x, pointerPos.y, 0);
            m_targetReticule.localScale = m_startingReticuleScale * Mathf.Clamp(pointerDistance / m_range, 1, 2);
            
            var fromShip = Physics2D.Raycast(transform.position, pointerPos - transform.position, pointerDistance, m_obstacleLayer);
            if (fromShip.collider != null)
            {
                Debug.DrawRay(transform.position, pointerPos - transform.position, Color.blue);
                m_projectedReticule.position = new Vector3(fromShip.point.x, fromShip.point.y, 0);
            }
            else if (pointerDistance > m_range * 2)
            {
                m_projectedReticule.position = new Ray2D(transform.position, pointerPos - transform.position).GetPoint(m_range * 2);
            }

            var reticuleDistance = Vector3.Distance(transform.position, m_projectedReticule.position);
            m_reticuleScaleMult = Mathf.Clamp(reticuleDistance / m_range, 1, 2);
            m_projectedReticule.localScale = m_startingReticuleScale * m_reticuleScaleMult;
            
            //foreach (var weapon in m_currentControlledWeapons)
            //{
            //    weapon.SetTargetCoord(m_targetReticule.position);
            //}
        }

        //make public to be called from ui
        private void LinkGuns(WeaponType typeToLink)
        {
            m_currentControlledWeapons = m_armamentsModule.GetWeaponsOfType(typeToLink);
        }

        private void Fire()
        {
            //fire with of m_reticuleScaleMult (radius of dispersion circle = scale * acc
        }
    }
}