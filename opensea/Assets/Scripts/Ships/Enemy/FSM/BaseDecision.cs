using UnityEngine;

namespace Assets.Scripts.Ships.Enemy.FSM
{
    public abstract class Decision : ScriptableObject
    {
        public abstract bool Decide(BaseStateMachine state);
    }
}
