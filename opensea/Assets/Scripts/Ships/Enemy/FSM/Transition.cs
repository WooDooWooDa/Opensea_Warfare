using UnityEngine;

namespace Assets.Scripts.Ships.Enemy.FSM
{
    [CreateAssetMenu(menuName = "FSM/Transition")]
    public sealed class Transition : ScriptableObject
    {
        public Decision Decision;
        public BaseState TrueState;
        public BaseState FalseState;

        public void Execute(BaseStateMachine stateMachine)
        {
            if (Decision.Decide(stateMachine) && !(TrueState is RemainInState))
                stateMachine.EnterState(TrueState);
            else if (!(FalseState is RemainInState))
                stateMachine.EnterState(FalseState);
        }
    }
}
