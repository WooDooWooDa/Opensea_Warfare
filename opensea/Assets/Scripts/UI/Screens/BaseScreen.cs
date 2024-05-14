using System;
using System.Linq;
using Assets.Scripts.Managers;
using UnityEngine;
using UnityEngine.UI;

namespace UI
{
    public class OpenInfo
    {
        public ScreenInformations Informations;
        public bool HasBack = false;
        public bool Focus = false;

        //Events
        public Action OnCloseScreen;
        public Action OnBackAction;
    }
    
    public abstract class BaseScreen : MonoBehaviour
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

            gameObject.SetActive(show); //should disable the whole screen
        }

        protected virtual void SoftEnable(bool show) //should partially deactivate components
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
        
        private void OnBack()
        {
            if (!m_openInfo.HasBack) return;
            
            m_backPressed = true;
            Main.Instance.GetManager<ScreenManager>().Back();
        }

        private void OnClose()
        {
            Main.Instance.GetManager<ScreenManager>().CloseScreen(this);
        }
    }
}