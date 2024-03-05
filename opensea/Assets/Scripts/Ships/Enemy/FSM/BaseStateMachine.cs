using UnityEngine;

namespace Assets.Scripts.Ships.Enemy.FSM
{
    public class BaseStateMachine : MonoBehaviour
    {
        [SerializeField] private BaseState _initialState;

        public bool IsActive { get; set; }
        public Enemy AttachedEnemy => m_enemy;

        private BaseState CurrentState { get; set; }
        private Enemy m_enemy;

        private void Awake()
        {
            CurrentState = _initialState;
            m_enemy = GetComponent<Enemy>();
        }

        public void EnterState(BaseState newState)
        {
            ExitState(CurrentState);

            CurrentState = newState;
        }

        private void ExitState(BaseState currentLastState)
        {
            
        }

        private void Update()
        {
            CurrentState.Execute(this);
        }
    }
}
