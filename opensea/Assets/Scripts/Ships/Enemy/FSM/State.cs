using System.Collections.Generic;
using UnityEngine;

namespace Assets.Scripts.Ships.Enemy.FSM
{
    [CreateAssetMenu(menuName = "FSM/State")]
    public sealed class State : BaseState
    {
        public List<BaseAction> Action = new List<BaseAction>();
        public List<Transition> Transitions = new List<Transition>();

        public override void Execute(BaseStateMachine machine)
        {
            foreach (var action in Action)
                action.Execute(machine);

            foreach (var transition in Transitions)
                transition.Execute(machine);
        }
    }
}
