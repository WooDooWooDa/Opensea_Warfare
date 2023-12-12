using System;
using System.Collections.Generic;
using System.Linq;
using Assets.Scripts.Helpers;
using Assets.Scripts.Weapons;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UIElements;
using Vector3 = UnityEngine.Vector3;

namespace Assets.Scripts.Ships.Modules
{
    [RequireComponent(typeof(Armaments))]
    public class FireControl : Module, IPointerClickHandler
    {
        [SerializeField] private Transform m_targetReticule;
        [SerializeField] private Transform m_projectedReticule;
        [SerializeField] private LayerMask m_obstacleLayer;
        
        private Armaments m_armamentsModule;

        private Vector3 m_startingReticuleScale;
        private float m_dispersion;
        private float m_range;
        [SerializeField] private bool m_isAiming;
        private Dictionary<WeaponType, bool> m_weaponTypeLinks = new();
        private WeaponType? m_currentControlledWeaponType;

        public void OnPointerClick(PointerEventData eventData)
        {
            if (m_currentControlledWeaponType == null || !m_isAiming) return;
            
            //verify if all link weapon are ready to fire

            m_armamentsModule.TryFireSelectedWeapons(m_dispersion);
        }
        
        public override void Initialize(Ship attachedShip)
        {
            base.Initialize(attachedShip);
            
            m_range = attachedShip.Stats.RNG;
            
            foreach(WeaponType type in Enum.GetValues(typeof(WeaponType))) {
                m_weaponTypeLinks.Add(type, false);
            }
            
            m_armamentsModule = GetComponentInChildren<Armaments>();
            m_startingReticuleScale = m_projectedReticule.localScale;
        }
        
        //called from ui
        public void ToggleAiming()
        {
            m_isAiming = !m_isAiming;
            
            m_projectedReticule.gameObject.SetActive(m_isAiming);
            m_targetReticule.gameObject.SetActive(m_isAiming);
        }
        
        public void SelectWeaponType(WeaponType type)
        {
            m_currentControlledWeaponType = type;
            m_armamentsModule.SelectWeapon(type);
        }

        public void SelectWeapon(Weapon weapon)
        {
            m_currentControlledWeaponType = weapon.Type;
            m_armamentsModule.SelectWeapon(weapon);
        }
        
        public void ToggleLinkWeaponType(WeaponType typeToLink)
        {
            var isLinked = m_weaponTypeLinks[typeToLink];
            if (!isLinked)
            {
                SelectWeaponType(typeToLink);
            }
            else
            {
                //select the first of the list of selected type otherwise none
            }
            m_weaponTypeLinks[typeToLink] = !isLinked;
        }

        protected override void InternalPreUpdateModule(float deltaTime)
        {
            
        }

        protected override void InternalUpdateModule(float deltaTime)
        {
            if (!m_isAiming) return;

            MoveReticule();
            
            if (m_currentControlledWeaponType == null) return;
            
            // set target for each selected weapon
            m_armamentsModule.SetTargetTo(m_targetReticule.position);
        }
        
        private void MoveReticule()
        {
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
            m_dispersion = Mathf.Clamp(reticuleDistance / m_range, 1, 2);
            m_projectedReticule.localScale = m_startingReticuleScale * m_dispersion;
        }
    }
}