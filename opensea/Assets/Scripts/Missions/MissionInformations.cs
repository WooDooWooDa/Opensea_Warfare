using System.Collections.Generic;
using Assets.Scripts.Missions.Objectives;
using Assets.Scripts.Ships.SOs;
using Env;
using UnityEngine;

namespace Assets.Scripts.Missions
{
    [CreateAssetMenu(fileName = "NewMissionInformation", menuName = "SO/Mission", order = 0)]
    public class MissionInformations : ScriptableObject
    {
        public int Number;
        public string Name;
        public string Description;
        public MapInformation MapInfo;
        public ObjectiveInformation MainObjective;
        public List<ObjectiveInformation> SecondaryObjectives;
        public List<ShipInformation> EnemyShipTypesPresentInMission;
        private List<MapHazardInformation> MapHazardsPresentInMission;
    }
}