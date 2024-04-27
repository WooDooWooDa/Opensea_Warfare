using System;
using System.Collections.Generic;
using UnityEngine;

namespace Assets.Scripts.Missions.Objectives
{
    [Serializable]
    public struct ObjectiveStep
    {
        public string Name;
        public string Description;
    }
    
    [CreateAssetMenu(fileName = "NewStepsObjectiveInformation", menuName = "SO/StepObjective", order = 1)]
    public class StepsObjectiveInformations : ObjectiveInformation
    {
        [Header("Steps")]
        public int nbSteps;
        public List<ObjectiveStep> steps;
    }
}