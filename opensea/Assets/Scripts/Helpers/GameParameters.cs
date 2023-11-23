using System;
using UnityEngine;

namespace Assets.Scripts.Helpers
{
    public class GameParameters : MonoBehaviour
    {
        public static GameParameters Instance { get => m_instance; }

        private static GameParameters m_instance;

        private GameParametersSO m_gameParametersSO;
        public GameParametersSO Value => m_gameParametersSO;

        private void Awake()
        {
            if (m_instance == null) {
                m_instance = this;
            }

            m_gameParametersSO = Resources.Load<GameParametersSO>("Parameters/gameParameters");
        }

    }

    
}
