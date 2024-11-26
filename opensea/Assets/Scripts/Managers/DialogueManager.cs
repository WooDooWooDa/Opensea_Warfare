using System;
using System.Collections.Generic;
using System.Linq;
using UI;
using UI.Screens;

namespace Assets.Scripts.Managers
{
    public class DialogueManager : Manager
    {
        private class DialogueQueueItem
        {
            public DialogueInformations info;
            public Action callback;
        }
        
        private Queue<DialogueQueueItem> m_dialoguesQueue = new Queue<DialogueQueueItem>();

        private ScreenManager m_screenManager;
        private BaseScreen m_openedScreen;
        
        public override void Initialize()
        {
            base.Initialize();

            m_screenManager = Main.Instance.GetManager<ScreenManager>();
        }

        public void QueueDialogue(DialogueInformations dialogue, Action endOfDialogueCallBack = null)
        {
            m_dialoguesQueue.Enqueue(new DialogueQueueItem() { info = dialogue, callback = endOfDialogueCallBack});
            if (m_dialoguesQueue.Count == 1 && !m_openedScreen)
                ShowNextDialogue();
        }
        
        public void StartDialogueNow(DialogueInformations dialogue, Action endOfDialogueCallBack = null)
        {
            m_dialoguesQueue.Clear();
            m_dialoguesQueue.Enqueue(new DialogueQueueItem() { info = dialogue, callback = endOfDialogueCallBack});
            if (m_dialoguesQueue.Count == 1 && !m_openedScreen)
                ShowNextDialogue();
        }

        public void ClearDialogue()
        {
            m_dialoguesQueue.Clear();
            if (m_openedScreen) 
                m_screenManager.CloseScreen(m_openedScreen);
        }

        private void ShowNextDialogue()
        {
            if (m_dialoguesQueue.Count > 0)
            {
                if (m_openedScreen) 
                    m_screenManager.CloseScreen(m_openedScreen);
                Show(m_dialoguesQueue.Dequeue());
            }
            else
            {
                m_screenManager.CloseScreen(m_openedScreen);
            }
        }

        private void Show(DialogueQueueItem dialogue)
        {
            var openInfo = new DialogueScreenOpenInfo()
            {
                Dialogue = dialogue.info,
                EndOfDialogueCallbacks = new[] { dialogue.callback, ShowNextDialogue },
                Focus = dialogue.info.Position == DialoguePosition.Bottom
            };
            
            m_openedScreen = m_screenManager.OpenScreen(ScreenName.DialogueBox, openInfo);
        }
    }
}