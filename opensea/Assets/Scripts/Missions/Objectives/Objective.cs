using System;
using UnityEngine;

namespace Assets.Scripts.Missions.Objectives
{
    public enum ObjectiveState
    {
        Inactive,
        Active,
        Completed,
        Failed
    }
    
    public abstract class Objective : MonoBehaviour
    {
        public ObjectiveState State;
        public ObjectiveInformation Information => m_information;
        public bool IsActive => State is ObjectiveState.Active;
        
        protected MissionManager m_missionManager;
        protected ObjectiveInformation m_information;
        
        public virtual void Initialize(MissionManager manager, ObjectiveInformation info)
        {
            m_missionManager = manager;
            m_information = info;
            
            SetupObjectiveEventHandler();
            if (m_information.Type is ObjectiveType.Main)
            {
                m_missionManager.PlayerFleet.FleetIsDestroyed += FailedObjective;
            }
        }

        public virtual void ActivateObjective()
        {
            if (State is not ObjectiveState.Inactive) return;
            
            State = ObjectiveState.Active;
        }

        public virtual void EndOfMissionIsCompleted()
        {
            if (!m_information.EndOfMissionCompletionCheck) return;
            
            CompleteObjective();
        }

        protected void CompleteObjective()
        {
            if (!IsActive) return;

            State = ObjectiveState.Completed;
            Debug.Log("Completed Objective : " + m_information.Name);
            //todo completed objective logic
            
            RemoveObjectiveEventHandler();
        }

        protected void FailedObjective()
        {
            if (!IsActive) return;

            State = ObjectiveState.Failed;
            Debug.Log("Failed Objective : " + m_information.Name);
            
            RemoveObjectiveEventHandler();
        }
        
        protected abstract void SetupObjectiveEventHandler();
        protected abstract void RemoveObjectiveEventHandler();
        protected virtual void UpdateObjective(float delta) {}

        private void Update()
        {
            if (!IsActive) return;
            
            UpdateObjective(Time.deltaTime);
        }
    }
}