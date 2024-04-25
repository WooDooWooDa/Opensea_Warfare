using Assets.Scripts.Managers;
using UnityEngine;
using UnityEngine.Serialization;

namespace UI
{
    [CreateAssetMenu(fileName = "NewScreenInformations", menuName = "SO/Screen/screenInfo", order = 0)]
    public class ScreenInformations : ScriptableObject
    {
        public ScreenName ScreenName;
        public ScreenLayer ScreenLayer;
        public BaseScreen Screen;
        public bool StaticScreen = false;
    }
}