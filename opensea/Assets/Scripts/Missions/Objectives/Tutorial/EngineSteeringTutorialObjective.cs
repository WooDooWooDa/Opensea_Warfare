using System.Collections.Generic;
using System.Linq;
using Assets.Scripts.Managers;
using Assets.Scripts.Missions.Objectives.ObjectiveComponents;
using Assets.Scripts.Ships.Modules;
using UI;
using UI.Screens;
using UnityEngine;

namespace Assets.Scripts.Missions.Objectives.Tutorial
{
    public class EngineSteeringTutorialObjective : StepsObjective
    {
        [SerializeField] public ObjectiveWaypoint m_fullSpeedWaypoint;
        [SerializeField] public ObjectiveWaypoint[] m_avoidSideWaypoint;
        [SerializeField] public ObjectiveWaypoint m_endWaypoint;
        [Header("Dialogues")]
        [SerializeField] private DialogueInformations m_fullSpeedDialogue;

        protected override void InternalNextStep()
        {
            m_currentStep = m_currentStepIndex switch
            {
                0 => new StepFullSpeed(this, m_fullSpeedWaypoint),
                1 => new StepToggleFollowShipCamera(this),
                2 => new StepFullStop(this),
                3 => new StepAvoidIsland(this, m_avoidSideWaypoint),
                4 => new StepManeuverToEnd(this, m_endWaypoint),
                _ => m_currentStep
            };
        }

        private class StepFullSpeed : Step
        {
            private readonly Engine m_shipEngine;
            private readonly ObjectiveWaypoint m_waypoint;
            public StepFullSpeed(StepsObjective obj, ObjectiveWaypoint objectiveWaypoint) : base(obj)
            {
                var fleet = Main.Instance.GetManager<PlayerFleet>();
                m_shipEngine = fleet.FlagShip.GetModuleOfType<Engine>();
                m_waypoint = objectiveWaypoint;
                m_waypoint.Activate();
            }

            public override void StartStep()
            {
                /*Main.Instance.GetManager<ScreenManager>().OpenScreen(ScreenName.DialogueBox, new DialogueScreenOpenInfo()
                {
                    Dialogue = Resources.Load<DialogueInformations>("Dialogues/T1/Step1")
                });*/
                Main.Instance.GetManager<DialogueManager>().QueueDialogue(Resources.Load<DialogueInformations>("Dialogues/T1/Step1"));
            }

            public override bool VerifyCondition()
            {
                return m_shipEngine.TargetPercentageOfSpeed >= 1f && m_waypoint.Visited;
            }
        }
        
        private class StepToggleFollowShipCamera : Step
        {
            public StepToggleFollowShipCamera(StepsObjective obj) : base(obj) { }
            
            public override void SetupEventHandler()
            {
                Main.Instance.BattleMapInputs.ScrollWheelClick.performed += (_) => Objective.CompleteStep();
            }
        }
        
        private class StepFullStop : Step
        {
            private readonly Engine m_shipEngine; 
            public StepFullStop(StepsObjective obj) : base(obj)
            {
                var fleet = Main.Instance.GetManager<PlayerFleet>();
                m_shipEngine = fleet.FlagShip.GetModuleOfType<Engine>();
            }

            public override bool VerifyCondition()
            {
                return m_shipEngine.CurrentSpeedPercentage <= -0.15f;
            }
        }
        
        private class StepAvoidIsland : Step
        {
            private readonly List<ObjectiveWaypoint> m_waypoints;
            public StepAvoidIsland(StepsObjective obj, IEnumerable<ObjectiveWaypoint> objectiveWaypoints) : base(obj)
            {
                m_waypoints = objectiveWaypoints.ToList();
                m_waypoints.ForEach(w => w.Activate());
            }
            
            public override void SetupEventHandler()
            {
                m_waypoints.ForEach(w => w.OnVisited += Objective.CompleteStep);
            }

            public override void RemoveEventHandler()
            {
                m_waypoints.ForEach(w => w.OnVisited -= Objective.CompleteStep);
            }
        }
        
        private class StepManeuverToEnd : Step
        {
            private readonly ObjectiveWaypoint m_waypoint;
            public StepManeuverToEnd(StepsObjective obj, ObjectiveWaypoint objectiveWaypoint) : base(obj)
            {
                m_waypoint = objectiveWaypoint;
                m_waypoint.Activate();
                m_waypoint.ShowBox();
            }
            
            public override void SetupEventHandler()
            {
                m_waypoint.OnVisited += Objective.CompleteStep;
            }

            public override void RemoveEventHandler()
            {
                m_waypoint.OnVisited -= Objective.CompleteStep;
            }
        }
    }
}