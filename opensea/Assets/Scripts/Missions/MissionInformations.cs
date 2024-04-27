using System;
using System.Collections.Generic;
using Assets.Scripts.Missions.Objectives;
using Assets.Scripts.Ships.SOs;
using Env;
using UnityEngine;

namespace Assets.Scripts.Missions
{
    [Serializable]
    public struct MapInformation
    {
        public int XMapSize;
        public int YMapSize;
        //etc
    }
    
    [CreateAssetMenu(fileName = "NewMissionInformation", menuName = "SO/Mission", order = 0)]
    public class MissionInformations : ScriptableObject
    {
        public int Number;
        public string Name;
        public string Description;
        public bool SingleGo; //means that the mission can only be played once after being completed ex: tutorial mission, hidden mission
        public MapInformation MapInfo;
        public ObjectiveInformation MainObjective;
        public List<ObjectiveInformation> SecondaryObjectives;
        public List<ShipInformation> EnemyShipTypesPresentInMission;
        private List<MapHazardInformation> MapHazardsPresentInMission;
    }
}