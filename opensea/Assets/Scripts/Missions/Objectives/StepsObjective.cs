using UnityEngine;

namespace Assets.Scripts.Missions.Objectives
{
    public abstract class StepsObjective : Objective
    {
        private StepsObjectiveInformations m_stepInformations;

        protected Step m_currentStep;
        
        protected int m_currentStepIndex;
        private ObjectiveStep m_currentObjectiveStep;
        
        public override void Initialize(MissionManager manager, ObjectiveInformation info)
        {
            m_currentStepIndex = 0;
            InternalNextStep();
            
            base.Initialize(manager, info);

            m_stepInformations = (StepsObjectiveInformations)info;
            m_currentObjectiveStep = m_stepInformations.steps[0];
        }

        public override void ActivateObjective()
        {
            base.ActivateObjective();
            m_currentStep.StartStep();
        }

        public void CompleteStep()
        {
            m_currentStep.RemoveEventHandler();
            m_currentStepIndex++;
            if (m_currentStepIndex == m_stepInformations.nbSteps)
            {
                CompleteObjective();
                return;
            }

            m_currentObjectiveStep = m_stepInformations.steps[m_currentStepIndex];
            NextStep();
            m_currentStep.SetupEventHandler();
        }

        protected override void SetupObjectiveEventHandler() { }

        protected override void RemoveObjectiveEventHandler() { }

        protected override void UpdateObjective(float delta)
        {
            if (m_currentStep is null) return;
            
            if (m_currentStep.VerifyCondition())
                CompleteStep();
        }

        private void NextStep()
        {
            Debug.Log("Going to next step !");
            m_currentStep.EndStep();
            InternalNextStep();
            m_currentStep.StartStep();
            Debug.Log("Starting step : " + m_currentObjectiveStep.Name);
        }

        protected abstract void InternalNextStep();
    }

    public abstract class Step
    {
        public float StepProgressPercentage;
        public StepsObjective Objective;

        protected Step(StepsObjective obj)
        {
            Objective = obj;
        }

        public virtual void StartStep() { }
        public virtual void EndStep() { }

        public virtual void SetupEventHandler() { }
        public virtual void RemoveEventHandler() { }
        public virtual bool VerifyCondition() { return false; }
    }
}