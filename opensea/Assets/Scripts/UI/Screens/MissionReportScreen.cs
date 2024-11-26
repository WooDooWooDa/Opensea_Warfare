using System;
using System.Collections.Generic;
using Assets.Scripts.Managers;
using Assets.Scripts.Missions;
using Assets.Scripts.Missions.Objectives;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

namespace UI.Screens
{
    public class MissionReportOpenInfo : OpenInfo
    {
        public MissionInformations MissionInformation;
        public MissionReport Report;
    }
    
    public class MissionReportScreen : BaseScreen
    {
        [SerializeField] private TextMeshProUGUI m_title;
        [SerializeField] private GameObject m_loseBackground;
        [Header("Report")] 
        [SerializeField] private Image m_mainStar;
        [SerializeField] private List<Image> m_sideStars;
        [SerializeField] private TextMeshProUGUI m_time;
        [SerializeField] private Transform m_missionBannerTransform;
        [Header("Buttons")] 
        [SerializeField] private Button m_returnToPortBtn;
        [SerializeField] private Button m_continueBtn;

        [Header("Prefab")] 
        [SerializeField] private Sprite m_blueStar;
        
        private MissionReportOpenInfo m_info;
        
        // ReSharper disable Unity.PerformanceAnalysis
        public override BaseScreen Open(OpenInfo openInfo)
        {
            base.Open(openInfo);
            m_info = (MissionReportOpenInfo)openInfo;

            m_returnToPortBtn.onClick.AddListener(() => Main.Instance.GetManager<MissionSceneManager>().ReturnToPort());

            SetProgressBtn();
            
            if (m_info.Report.MainObjectiveStatus == ObjectiveState.Completed)
                SetWinScreen();
            else
                SetLostScreen();

            return this;
        }

        public void Test()
        {
            Debug.Log("TEST click!");
        }

        private void SetProgressBtn()
        {
            if (m_info.MissionInformation.NextMisssion is null)
            {
                m_continueBtn.gameObject.SetActive(false);
                return;
            }
                
            if (m_info.Report.MainObjectiveStatus == ObjectiveState.Completed)    
            {
                m_continueBtn.gameObject.GetComponentInChildren<TextMeshProUGUI>().text = "Continue";
                m_continueBtn.onClick.AddListener(() => Main.Instance.GetManager<MissionSceneManager>()
                    .OpenMissionAsync(m_info.MissionInformation.NextMisssion));
            }
            else
            {
                m_continueBtn.gameObject.GetComponentInChildren<TextMeshProUGUI>().text = "Retry Mission";
                m_continueBtn.onClick.AddListener(() => Main.Instance.GetManager<MissionSceneManager>()
                    .OpenMissionAsync(m_info.MissionInformation));
            }
        }

        private void SetWinScreen()
        {
            m_title.text = "Mission Completed";
            m_mainStar.gameObject.SetActive(true);
            m_mainStar.sprite = m_blueStar; //todo Add stomp animation when set
            for (var i = 0; i < m_info.Report.nbSidesObj; i++)
            {
                if (m_info.Report.SideObjectivesStatus[i] is ObjectiveState.Completed)
                {
                    m_sideStars[i].gameObject.SetActive(true);
                    m_sideStars[i].sprite = m_blueStar; //todo Add stomp animation when set
                }
            }
            //todo Add time if time based
        }
        
        private void SetLostScreen()
        {
            m_title.text = "Fleet Sunk";
            m_loseBackground.SetActive(true);
            m_mainStar.gameObject.SetActive(false);
            m_sideStars.ForEach(s => s.gameObject.SetActive(false));
        }
    }
}