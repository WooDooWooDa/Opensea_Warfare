using Assets.Scripts.UI.Widgets;
using System;
using System.Collections;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

namespace UI
{
    public class DialogueWidgetData : WidgetData
    {
        public DialogueInformations DialogueInformations;
        public Action EndOfDialogueCallback;
    }

    public class DialogueWidget : Widget
    {
        [SerializeField] private GameObject m_avatarGO;
        [SerializeField] private Image m_avatarImg;
        [SerializeField] private TextMeshProUGUI m_avatarName;
        [SerializeField] private TextMeshProUGUI m_dialogueTextBox;

        private DialogueInformations m_currentDialogue;
        private Sentence m_currentSentence;
        private Action m_endOfDialogueCallback;

        private bool m_isWriting;

        public override void SetData(WidgetData data)
        {
            var dialogueData = ((DialogueWidgetData)data);
            m_currentDialogue = dialogueData.DialogueInformations;
            m_currentSentence = m_currentDialogue.FirstSentence;
            m_endOfDialogueCallback = dialogueData.EndOfDialogueCallback;
            StartWriting();
        }

        public void GoNext()
        {
            if (m_isWriting) {
                CompleteWrite();
                return;
            }

            if (m_currentSentence.NextSentence != null) {
                m_currentSentence = m_currentSentence.NextSentence;
                StartWriting();
            } else {
                m_endOfDialogueCallback?.Invoke();
            }
        }

        private void StartWriting()
        {
            m_dialogueTextBox.text = "";
            StartCoroutine(Write());
        }

        private void CompleteWrite()
        {
            StopCoroutine(Write());
            m_dialogueTextBox.text = m_currentSentence.Text;
        }

        private IEnumerator Write()
        {
            m_isWriting = true;
            var nbCaracters = m_currentSentence.Text.Length;
            var dialogueBuilder = "";
            for (int i = 0; i < nbCaracters; i++) {
                dialogueBuilder += m_currentSentence.Text[i];
                m_dialogueTextBox.text = dialogueBuilder;
                yield return new WaitForSecondsRealtime(0.05f);
            }
            m_isWriting = false;
            if (m_currentDialogue.AutoPassToNext) {
                yield return new WaitForSecondsRealtime(m_currentDialogue.TimeBeforeNext);
                GoNext();
            }
        }
    }
}