using Assets.Scripts.Helpers;
using UnityEngine;

namespace Assets.Scripts.Inputs
{
    public class OnSpaceInputSystem : MonoBehaviour
    {
        private GameInputs m_inputActions;

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
            m_inputActions.BattleMap.SpaceBar.performed += ctx => Events.Inputs.FireSpaceBarPressed();
        }
    }
}