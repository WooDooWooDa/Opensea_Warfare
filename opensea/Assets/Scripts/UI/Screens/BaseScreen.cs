using System;
using Assets.Scripts.Managers;
using UnityEngine;
using UnityEngine.UI;

namespace UI
{
    public class OpenInfo
    {
        public ScreenInformations Informations;
        public bool HasBack = false;

        //Events
        public Action OnCloseScreen;
        public Action OnBackAction;
    }
    
    public class BaseScreen : MonoBehaviour
    {
        [Header("Base")]
        [SerializeField] private Button m_backButton;
        [SerializeField] private Button m_closeButton;

        protected ScreenInformations m_informations;
        protected OpenInfo m_openInfo = null;
        private bool m_backPressed = false;

        protected virtual void Awake()
        {
            m_backButton?.onClick.AddListener(OnBack);
            m_closeButton?.onClick.AddListener(OnClose);
        }
        
        public void Enable(bool show)
        {
            if (m_informations.StaticScreen) {
                SoftEnable(show);
                return;
            }

            gameObject.SetActive(show); //disable the whole screen
        }

        protected virtual void SoftEnable(bool show) //partially deactivate components
        {

        }
        
        public virtual BaseScreen Open(OpenInfo openInfo)
        {
            m_informations = openInfo.Informations;
            m_openInfo = openInfo;

            return this;
        }

        public virtual void Close()
        {
            if (m_backPressed) {
                m_openInfo?.OnBackAction?.Invoke();
            } else {
                m_openInfo?.OnCloseScreen?.Invoke();
            }
        }
        
        protected void OnBack()
        {
            if (!m_openInfo.HasBack) return;
            
            m_backPressed = true;
            Main.Instance.GetManager<ScreenManager>().Back();
        }

        protected void OnClose()
        {
            Main.Instance.GetManager<ScreenManager>().CloseScreen(m_informations.ScreenName);
        }
    }
}