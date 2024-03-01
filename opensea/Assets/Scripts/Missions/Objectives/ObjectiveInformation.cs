using UnityEngine;
using UnityEngine.Serialization;

namespace Assets.Scripts.Missions.Objectives
{
    public enum ObjectiveType
    {
        Main,
        Secondary
    }
    
    [CreateAssetMenu(fileName = "NewObjectiveInformation", menuName = "SO/Objective", order = 0)]
    public class ObjectiveInformation : ScriptableObject
    {
        public int Number;
        public string Name;
        public string Description;
        public ObjectiveType Type;
        public bool EndOfMissionCompletionCheck; //determines that the objective is check if completed at the end of the mission ex : no casuality
        public Objective ObjectiveObj;
    }
}