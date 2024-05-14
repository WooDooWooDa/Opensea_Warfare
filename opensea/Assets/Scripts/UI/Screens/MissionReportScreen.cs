using System;
using Assets.Scripts.Managers;
using Assets.Scripts.Missions;
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
        [Header("Report")] 
        [SerializeField] private Image m_mainStar;
        [SerializeField] private Image[] m_sideStars;
        [SerializeField] private TextMeshProUGUI m_time;
        [SerializeField] private Transform m_missionBannerTransform;
        [Header("Buttons")] 
        [SerializeField] private Button m_returnToPortBtn;
        [SerializeField] private Button m_continueBtn;

        private MissionReportOpenInfo m_info;
        
        public override BaseScreen Open(OpenInfo openInfo)
        {
            base.Open(openInfo);
            m_info = (MissionReportOpenInfo)openInfo;

            m_returnToPortBtn.onClick.AddListener(() => Main.Instance.GetManager<MissionSceneManager>().ReturnToPort());
            
            if (m_info.MissionInformation.NextMisssion != null)
            {
                m_continueBtn.gameObject.SetActive(true);
                m_continueBtn.onClick.AddListener(() => Main.Instance.GetManager<MissionSceneManager>()
                    .OpenMissionAsync(m_info.MissionInformation.NextMisssion));
            }

            return this;
        }
    }
}