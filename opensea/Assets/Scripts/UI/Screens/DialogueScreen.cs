using UnityEngine;
using UnityEngine.InputSystem;

namespace UI.Screens
{
    public enum DialoguePosition
    {
        Corner,
        Bottom
    }
    
    public class DialogueScreenOpenInfo: OpenInfo
    {
        public DialogueInformations Dialogue;
    }
    
    public class DialogueScreen : BaseScreen
    {
        [Header("DialogueScreen")] 
        [SerializeField] private DialogueWidget m_bottomWidget;
        [SerializeField] private DialogueWidget m_cornerWidget;

        private DialogueWidget m_currentDialogueWidget;
        private float m_goNextSpamBuffer;

        public override BaseScreen Open(OpenInfo openInfo)
        {
            base.Open(openInfo);

            var info = (DialogueScreenOpenInfo)m_openInfo;

            if (info.Dialogue.Position == DialoguePosition.Bottom) {
                m_currentDialogueWidget = m_bottomWidget;
                Time.timeScale = 0;

                //todo deactivate ships inputs
            } else {
                m_currentDialogueWidget = m_cornerWidget;
            }
            m_currentDialogueWidget.gameObject.SetActive(true);
            m_currentDialogueWidget.SetData(new DialogueWidgetData() { 
                DialogueInformations = info.Dialogue,
                EndOfDialogueCallback = OnClose
            });

            return this;
        }

        public override void Close()
        {
            var info = (DialogueScreenOpenInfo)m_openInfo;
            if (info.Dialogue.Position == DialoguePosition.Bottom) {
                Time.timeScale = 1;

                //todo reactivate inputs
            }
            m_currentDialogueWidget.gameObject.SetActive(false);

            base.Close();
        }

        private void Update()
        {
            m_goNextSpamBuffer += Time.unscaledDeltaTime;
            if (m_goNextSpamBuffer >= 1f && (Keyboard.current.anyKey.isPressed || Mouse.current.leftButton.isPressed || Mouse.current.rightButton.isPressed)) {
                m_currentDialogueWidget.GoNext();
                m_goNextSpamBuffer = 0;
            }
        }
    }
}