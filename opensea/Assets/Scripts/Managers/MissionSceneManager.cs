using Assets.Scripts.Missions;
using UI;
using UnityEngine;

namespace Assets.Scripts.Managers
{
    public class MissionSceneManager : Manager
    {
        private const string PORT_SCENE_NAME = "port_scene";
        
        private ScreenManager m_screenManager;

        public override void Initialize()
        {
            base.Initialize();

            m_screenManager = Main.Instance.GetManager<ScreenManager>();
        }

        public void ReturnToPort()
        {
            OpenLoading(PORT_SCENE_NAME);
        }

        public void OpenMissionAsync(MissionInformations mission)
        {
            OpenLoading(mission.MissionSceneName); // Add next mission info as open param to display
        }

        private void OpenLoading(string sceneName, OpenInfo info = null)
        {
            m_screenManager.OpenScreen(ScreenName.Loading, info); 
            var loading = UnityEngine.SceneManagement.SceneManager.LoadSceneAsync(sceneName);
            loading.completed += CloseLoading;
        }

        private void CloseLoading(AsyncOperation obj)
        {
            m_screenManager.CloseScreen(ScreenName.Loading);
        }

    }
}