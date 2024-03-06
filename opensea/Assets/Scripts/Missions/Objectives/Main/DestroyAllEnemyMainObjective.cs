using Assets.Scripts.Ships;

namespace Assets.Scripts.Missions.Objectives
{
    public class DestroyAllEnemyMainObjective : Objective
    {
        private int m_nbEnemyToDestroy;
        private int m_nbEnemyDestroyed;

        public override void Initialize(MissionManager manager, ObjectiveInformation info)
        {
            base.Initialize(manager, info);
            m_nbEnemyToDestroy = m_missionManager.EnemyFleet.Ships.Count;
        }

        protected override void SetupObjectiveEventHandler()
        {
            m_missionManager.EnemyFleet.ShipIsDestroyed += ShipDestroyed;
            m_missionManager.EnemyFleet.FleetIsDestroyed += CompleteObjective;
        }

        protected override void RemoveObjectiveEventHandler()
        {
	        m_missionManager.EnemyFleet.ShipIsDestroyed -= ShipDestroyed;
            m_missionManager.EnemyFleet.FleetIsDestroyed -= CompleteObjective;
        }
        
        private void ShipDestroyed(Ship obj)
        {
            m_nbEnemyDestroyed++;
        }
    }
}