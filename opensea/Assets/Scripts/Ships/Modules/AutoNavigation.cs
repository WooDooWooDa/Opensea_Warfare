using System;
using System.Collections.Generic;
using System.Linq;
using Assets.Scripts.Helpers;
using Unity.VisualScripting;
using UnityEngine;

namespace Assets.Scripts.Ships.Modules
{
    [RequireComponent(typeof(SteeringGear), typeof(Engine))]
    public class AutoNavigation : ActionModule
    {
        [Serializable]
        private struct Waypoint
        {
            public Vector3 Destination;
            public int LineIndex;
        }

        [SerializeField] private LineRenderer m_lineRenderer;
        [SerializeField] private LayerMask m_oceanLayer;
        
        private const float RemoveThreshold = 0.25f;

        private SteeringGear m_steeringGear;
        private Engine m_engine;
        
        private List<Waypoint> m_navigationWaypoints = new();
        
        public override void Initialize(Ship attachedShip)
        {
            base.Initialize(attachedShip);
            
            m_inputActions.BattleMap.RightTap.performed += ctx => AddNewWaypoint();

            m_steeringGear = (SteeringGear)m_ship.GetModuleOfType(ModuleType.SteeringGear);
            m_engine = (Engine)m_ship.GetModuleOfType(ModuleType.Engine);

            //m_lineRenderer.enabled = false;
        }

        protected override void InternalPreUpdateModule(float deltaTime)
        {
            if (m_navigationWaypoints.Any())
                m_lineRenderer.SetPosition(0, m_ship.transform.position);
        }

        protected override void InternalUpdateModule(float deltaTime)
        {
            
        }

        private void AddNewWaypoint()
        {
            var pointerPos = Helper.PointerPosition;
            pointerPos = new Vector3(pointerPos.x, pointerPos.y, -1);
            
            if (!Physics2D.OverlapCircle(pointerPos, 0.01f, m_oceanLayer)) return;
            
            if (m_navigationWaypoints.Any())
            {
                var toRemoved = m_navigationWaypoints.FirstOrDefault(w => Vector3.Distance(w.Destination, pointerPos) < RemoveThreshold);

                if (toRemoved.LineIndex > 0)
                {
                    //todo-P3 remove marker animation
                    m_navigationWaypoints.Remove(toRemoved);
                    m_lineRenderer.positionCount--;
                    m_lineRenderer.SetPositions(m_navigationWaypoints.Select(w => w.Destination).ToArray());
                    return;
                }
            }
            
            //todo-P3 add new waypoint animation
            var newWaypoint = new Waypoint()
            {
                Destination = pointerPos,
                LineIndex = m_navigationWaypoints.Count + 1
            };
            m_navigationWaypoints.Add(newWaypoint);
            m_lineRenderer.positionCount++;
            m_lineRenderer.SetPosition(newWaypoint.LineIndex, newWaypoint.Destination);
        }
        private void OnDrawGizmos()
        {
            m_navigationWaypoints.ForEach(w => 
                Gizmos.DrawWireSphere(w.Destination, 0.1f));
        }
    }
}