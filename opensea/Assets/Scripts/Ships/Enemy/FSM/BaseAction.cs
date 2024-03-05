using UnityEngine;

namespace Assets.Scripts.Ships.Enemy.FSM
{
    public abstract class BaseAction : ScriptableObject
    {
        public abstract void Execute(BaseStateMachine stateMachine);
    }
}
