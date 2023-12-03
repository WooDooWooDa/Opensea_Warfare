using System;
using Assets.Scripts.Helpers;
using UnityEngine;

namespace Assets.Scripts.Inputs
{
    public class OnArrowInputSystem : MonoBehaviour
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
            m_inputActions.BattleMap.Move.performed += ctx => Pressed();
        }

        private void Pressed()
        {
            var vec = m_inputActions.BattleMap.Move.ReadValue<Vector2>();
            if (vec.y != 0)
            {
                Events.Inputs.FireOnUpDownChanged(vec.y);
            } 
            else if (vec.x != 0)
            {
                Events.Inputs.FireOnSideChanged(vec.x);
            } 
        }
    }
}