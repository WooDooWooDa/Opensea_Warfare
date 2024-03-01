namespace Assets.Scripts.Missions.Objectives.Secondary
{
    public class NoCasualitySecondaryObjective : Objective
    {
        public override void EndOfMissionIsCompleted()
        {
            if (m_missionManager.PlayerFleet.DestroyedShips.Count == 0)
            {
                CompleteObjective();
            }
            else
            {
                FailedObjective();
            }
        }

        protected override void SetupObjectiveEventHandler()
        {
            m_missionManager.PlayerFleet.ShipIsDestroyed += ship => FailedObjective();
        }

        protected override void RemoveObjectiveEventHandler()
        {
            m_missionManager.PlayerFleet.ShipIsDestroyed -= ship => FailedObjective();
        }
    }
}