using Assets.Scripts.UI.Widgets;
using System;
using System.Collections;
using System.Linq;
using TMPro;
using UI.Screens;
using UnityEngine;
using UnityEngine.UI;

namespace UI
{
    public class DialogueWidgetData : WidgetData
    {
        public DialogueInformations DialogueInformations;
        public Action[] EndOfDialogueCallbacks;
    }

    public class DialogueWidget : Widget
    {
        [SerializeField] private GameObject m_avatarGO;
        [SerializeField] private Image m_avatarImg;
        [SerializeField] private TextMeshProUGUI m_avatarName;
        [SerializeField] private TextMeshProUGUI m_dialogueTextBox;
        [SerializeField] private GameObject m_nextText;

        private DialogueInformations m_currentDialogue;
        private Sentence m_currentSentence;
        private event Action m_endOfDialogueCallback;

        private bool m_isWriting;

        public override void SetData(WidgetData data)
        {
            GetComponentsInChildren<Canvas>().ToList().ForEach(c => c.sortingLayerID = SortingLayer.NameToID("UI"));
            
            var dialogueData = ((DialogueWidgetData)data);
            m_currentDialogue = dialogueData.DialogueInformations;
            m_currentSentence = m_currentDialogue.FirstSentence;
            foreach (var callback in dialogueData.EndOfDialogueCallbacks)
            {
                m_endOfDialogueCallback += callback;
            }
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
            StopAllCoroutines();
            m_dialogueTextBox.text = m_currentSentence.Text;
            m_isWriting = false;
            if (m_nextText != null)
            {
                m_nextText.SetActive(true);
                //LeanTween.scale(m_nextText, Vector3.one * 1.5f, 0.2f).setLoopPingPong();
            }
        }

        private IEnumerator Write()
        {
            m_isWriting = true;
            if (m_nextText != null)
            {
                m_nextText.SetActive(false);
                //LeanTween.cancel(m_nextText);
            }
            var nbCharacters = m_currentSentence.Text.Length;
            var dialogueBuilder = "";
            for (var i = 0; i < nbCharacters; i++) {
                dialogueBuilder += m_currentSentence.Text[i];
                m_dialogueTextBox.text = dialogueBuilder;
                yield return new WaitForSecondsRealtime(0.1f); //todo change this value to make it faster the more characters
            }
            m_isWriting = false;
            if (m_nextText != null)
            {
                m_nextText.SetActive(true);
                //LeanTween.scale(m_nextText, Vector3.one * 1.1f, 0.5f).setLoopPingPong();
            }
            if (m_currentDialogue.Position == DialoguePosition.Corner) {
                yield return new WaitForSecondsRealtime(nbCharacters * 0.02f);
                GoNext();
            }
        }
    }
}