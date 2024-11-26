using UnityEngine;

namespace UI
{
    [CreateAssetMenu(fileName = "NewPopup", menuName = "SO/Popup", order = 0)]
    public class PopupInformations : ScriptableObject
    {
        public Vector3 PositionOnScreen;
        [TextArea] public string Text;
    }
}