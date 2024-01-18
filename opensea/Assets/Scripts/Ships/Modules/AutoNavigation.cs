using System;
using System.Collections.Generic;
using System.Linq;
using Assets.Scripts.Helpers;
using TMPro;
using UnityEngine;

namespace Assets.Scripts.Ships.Modules
{
    [RequireComponent(typeof(SteeringGear), typeof(Engine))]
    public class AutoNavigation : ActionModule
    {
        [Serializable]
        private class Waypoint
        {
            public Vector3 Destination;
            public int LineIndex;
            public GameObject UIInstance;
        }
        
        private float NextWaypointDistance => Vector3.Distance(m_ship.transform.position, m_nextWaypoint.Destination);

        [SerializeField] private GameObject m_waypointUI;
        [SerializeField] private GameObject m_waypointUIPrefab;
        [SerializeField] private LineRenderer m_lineRenderer;
        [SerializeField] private LayerMask m_oceanLayer;

        private const float WaypointDistanceThreshold = 2f;
        private const float RemoveThreshold = 0.25f;

        private SteeringGear m_steeringGear;
        private Engine m_engine;
        
        private List<Waypoint> m_navigationWaypoints = new();
        private Waypoint m_nextWaypoint;
        private Vector3 m_startPointFromNextWaypoint;
        
        public override void Initialize(Ship attachedShip)
        {
            base.Initialize(attachedShip);
            
            m_inputActions.BattleMap.RightTap.performed += ctx => AddNewWaypoint();

            m_steeringGear = (SteeringGear)m_ship.GetModuleOfType(ModuleType.SteeringGear);
            m_engine = (Engine)m_ship.GetModuleOfType(ModuleType.Engine);
        }

        protected override void InternalPreUpdateModule(float deltaTime)
        {
            if (m_navigationWaypoints.Any())
                m_lineRenderer.SetPosition(0, m_ship.transform.position);
        }

        protected override void InternalUpdateModule(float deltaTime)
        {
            if (!m_navigationWaypoints.Any()) return;
            
            if (m_nextWaypoint == null)
            {
                SetNextWaypoint();
            }
            else
            {
                RotateTowardsWaypoint();
                MoveTowardsWaypoint();
                
                if (NextWaypointDistance <= WaypointDistanceThreshold)
                {
                    RemoveWaypoint(m_nextWaypoint);
                }
            }
        }

        private void SetNextWaypoint()
        {
            m_nextWaypoint = m_navigationWaypoints[0];
            m_startPointFromNextWaypoint = m_ship.transform.position;
        }

        private void MoveTowardsWaypoint()
        {
            var distanceOfShip = NextWaypointDistance;
            var distanceOfWaypointFromStartPoint = Vector3.Distance(m_nextWaypoint.Destination, m_startPointFromNextWaypoint);
            float speedPart = 0;
            if (m_steeringGear.AngleDiff < 90 && distanceOfShip > WaypointDistanceThreshold)
            {
                var traveled = distanceOfShip / distanceOfWaypointFromStartPoint; //percentage of travel done
                speedPart = traveled switch //todo-P1 This really doesnt work on long distance
                {
                    (> 0.75f) => 1,
                    (> 0.5f) => 0.75f,
                    (> 0.25f) => 0.5f,
                    (> 0) => 0.25f,
                    _ => 0
                };
                if (m_steeringGear.AngleDiff >= 45 && speedPart > 0.5f)
                    speedPart = 0.5f;
            }
            m_engine.SetTargetSpeedTo(speedPart);
        }

        private void RotateTowardsWaypoint()
        {
            var direction = m_ship.transform.position - m_nextWaypoint.Destination;
            var angle = -(Vector2.Angle(Vector2.right, -direction) - 90);
            float result;
            if (angle > 0)
            {
                if (direction.y < 0)
                {
                    result = 360 - angle;
                }
                else
                {
                    result = 180 + angle;
                }
            }
            else
            {
                if (direction.y > 0)
                {
                    result = 180 + angle;
                }
                else
                {
                    result = -angle;
                }
            }
            m_steeringGear.SetTargetAngle(result);
        }

        private void AddNewWaypoint()
        {
            var pointerPos = Helper.PointerPosition;
            pointerPos = new Vector3(pointerPos.x, pointerPos.y, -1);
            
            if (!Physics2D.OverlapCircle(pointerPos, 0.01f, m_oceanLayer)) return;
            
            if (m_navigationWaypoints.Any())
            {
                var toRemoved = m_navigationWaypoints.FirstOrDefault(w => Vector3.Distance(w.Destination, pointerPos) < RemoveThreshold);

                if (toRemoved != null)
                {
                    //todo-P3 remove marker animation
                    RemoveWaypoint(toRemoved);
                    return;
                }
            }
            
            //todo-P3 add new waypoint animation
            var uiInstance = Instantiate(m_waypointUIPrefab, m_waypointUI.transform);
            uiInstance.GetComponentInChildren<TextMeshProUGUI>().text = $"{m_navigationWaypoints.Count + 1}";
            var newWaypoint = new Waypoint()
            {
                Destination = pointerPos,
                LineIndex = m_navigationWaypoints.Count + 1,
                UIInstance = uiInstance
            };
            uiInstance.transform.position = newWaypoint.Destination;
            m_navigationWaypoints.Add(newWaypoint);
            m_lineRenderer.positionCount++;
            m_lineRenderer.SetPosition(newWaypoint.LineIndex, newWaypoint.Destination);
        }

        private void RemoveWaypoint(Waypoint toRemoved)
        {
            Destroy(toRemoved.UIInstance);
            m_navigationWaypoints.Remove(toRemoved);
            m_lineRenderer.positionCount = m_navigationWaypoints.Count + 1;
            var shipPos = new List<Vector3> { m_ship.transform.position };
            shipPos.AddRange(m_navigationWaypoints.Select(w => w.Destination).ToList());
            m_lineRenderer.SetPositions(shipPos.ToArray());
            m_nextWaypoint = null;
        }
        
        private void OnDrawGizmos()
        {
            m_navigationWaypoints.ForEach(w => 
                Gizmos.DrawWireSphere(w.Destination, 0.1f));
        }
    }
}