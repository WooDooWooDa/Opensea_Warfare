namespace Assets.Scripts.Missions.Objectives.Secondary
{
    public class FinishInTimeSecondaryObjective : Objective
    {
        private float m_timeRemaining = 300f;
        
        protected override void UpdateObjective(float delta)
        {
            m_timeRemaining -= delta;
            if (m_timeRemaining <= 0)
            {
                FailedObjective();
            }
        }

        public override void EndOfMissionIsCompleted()
        {
            if (m_timeRemaining > 0)
            {
                CompleteObjective();
            }
        }

        protected override void SetupObjectiveEventHandler()
        {
            
        }

        protected override void RemoveObjectiveEventHandler()
        {
            
        }
    }
}