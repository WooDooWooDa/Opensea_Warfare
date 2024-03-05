using UnityEngine;

namespace Assets.Scripts.Ships.Enemy.FSM
{
    public abstract class BaseState : ScriptableObject
    {
        public virtual void Execute(BaseStateMachine machine) { }
    }
}
