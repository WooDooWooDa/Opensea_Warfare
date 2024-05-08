using System;
using System.Collections;
using System.Collections.Generic;
using Assets.Scripts.Managers;
using Assets.Scripts.Missions.Objectives;
using UI;
using UnityEngine;

namespace Assets.Scripts.Missions
{
    public class MissionManager : Manager
    {
        public string MissionSigil => m_informations.Sigil;

        [SerializeField] private MissionInformations m_informations;

        public Action OnMissionIsEnding;
        public PlayerFleet PlayerFleet => m_playerFleet;
        public EnemyFleet EnemyFleet => m_enemyFleet;
        
        private Objective m_winConditionObjective;
        private List<Objective> m_secondaryObjectives = new List<Objective>();
        private PlayerFleet m_playerFleet;
        private EnemyFleet m_enemyFleet;

        private void Start()
        {
            Initialize(); //todo move this out of start
        }

        public override void Initialize()
        {
            base.Initialize();
            m_enemyFleet = GetComponentInChildren<EnemyFleet>();
            m_enemyFleet.Initialize();
            m_playerFleet = Main.Instance.GetManager<PlayerFleet>();
            m_playerFleet.gameObject.SetActive(false);
            InitializeObjectives();
            StartCoroutine(MissionLoop());
        }

        private void InitializeObjectives()
        {
            m_winConditionObjective = Instantiate(m_informations.MainObjective.ObjectiveObj, transform);
            m_winConditionObjective.Initialize(this, m_informations.MainObjective);
            
            foreach (var objective in m_informations.SecondaryObjectives)
            {
                var obj = Instantiate(objective.ObjectiveObj, transform);
                obj.Initialize(this, objective);
                if (objective.EndOfMissionCompletionCheck) OnMissionIsEnding += obj.EndOfMissionIsCompleted;
                m_secondaryObjectives.Add(obj);
            }
        }

        private IEnumerator MissionLoop()
        {
            yield return StartCoroutine(MissionIsStarting());
            while (m_winConditionObjective.State is ObjectiveState.Active) //wait for main win condition
            {
                yield return null;
            }
            yield return StartCoroutine(MissionIsEnding());
            ReturnToPort();
        }

        private IEnumerator MissionIsStarting()
        {
            //Wait X amount of time before starting
            //or show a dialog
            yield return new WaitForSeconds(3);
            debugger.Log("Mission has started!");
            StartMission();
        }
        
        private void StartMission()
        {
            m_playerFleet.FocusOn(1);
            Main.Instance.BattleMapInputs.Enable();
            Main.Instance.GetManager<ScreenManager>().OpenScreen(ScreenName.Battle);

            m_winConditionObjective.ActivateObjective();
            m_secondaryObjectives.ForEach(o => o.ActivateObjective());
        }
        
        private IEnumerator MissionIsEnding()
        {
            m_playerFleet.FocusOn(null);
            Main.Instance.BattleMapInputs.Disable();
            Main.Instance.GetManager<ScreenManager>().CloseScreen(ScreenName.Battle);

            OnMissionIsEnding?.Invoke();
            yield return new WaitForSeconds(1);
            //wait then show end mission screen win or lose depending on main objective condition
            EndMission();
        }

        private void EndMission()
        {
            debugger.Log("Mission has ended...");
        }

        private void ReturnToPort()
        {
            debugger.Log("Returning to port...");
        }
    }
}