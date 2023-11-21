using Assets.Scripts.Helpers;
using UnityEngine;

namespace Assets.Scripts.Inputs
{
    public class OnNumpadInputSystem : MonoBehaviour
    {
        [SerializeField] private GameInputs m_inputActions;

        private void Awake()
        {
            m_inputActions = new GameInputs();
        }

        private void OnEnable()
        {
            m_inputActions.Enable();
        }

        private void OnDisable()
        {
            m_inputActions.Disable();
        }

        private void Start()
        {
            m_inputActions.BattleMap.Select1.performed += ctx => Events.Inputs.FireOnNumPressed(1);
            m_inputActions.BattleMap.Select2.performed += ctx => Events.Inputs.FireOnNumPressed(2);
            m_inputActions.BattleMap.Select3.performed += ctx => Events.Inputs.FireOnNumPressed(3);
            m_inputActions.BattleMap.Select4.performed += ctx => Events.Inputs.FireOnNumPressed(4);
            m_inputActions.BattleMap.Select5.performed += ctx => Events.Inputs.FireOnNumPressed(5);
            m_inputActions.BattleMap.Select6.performed += ctx => Events.Inputs.FireOnNumPressed(6);
            m_inputActions.BattleMap.Select7.performed += ctx => Events.Inputs.FireOnNumPressed(7);
            m_inputActions.BattleMap.Select8.performed += ctx => Events.Inputs.FireOnNumPressed(8);
            m_inputActions.BattleMap.Select9.performed += ctx => Events.Inputs.FireOnNumPressed(9);
            m_inputActions.BattleMap.Select10.performed += ctx => Events.Inputs.FireOnNumPressed(10);
        }

        private void OnDestroy()
        {
            m_inputActions.BattleMap.Select1.performed -= ctx => Events.Inputs.FireOnNumPressed(1);
            m_inputActions.BattleMap.Select2.performed -= ctx => Events.Inputs.FireOnNumPressed(2);
            m_inputActions.BattleMap.Select3.performed -= ctx => Events.Inputs.FireOnNumPressed(3);
            m_inputActions.BattleMap.Select4.performed -= ctx => Events.Inputs.FireOnNumPressed(4);
            m_inputActions.BattleMap.Select5.performed -= ctx => Events.Inputs.FireOnNumPressed(5);
            m_inputActions.BattleMap.Select6.performed -= ctx => Events.Inputs.FireOnNumPressed(6);
            m_inputActions.BattleMap.Select7.performed -= ctx => Events.Inputs.FireOnNumPressed(7);
            m_inputActions.BattleMap.Select8.performed -= ctx => Events.Inputs.FireOnNumPressed(8);
            m_inputActions.BattleMap.Select9.performed -= ctx => Events.Inputs.FireOnNumPressed(9);
            m_inputActions.BattleMap.Select10.performed -= ctx => Events.Inputs.FireOnNumPressed(10);
        }
    }
}
