using UI.Screens;
using UnityEngine;
using UnityEngine.Serialization;

namespace UI
{
    [CreateAssetMenu(fileName = "NewDialogue", menuName = "SO/Dialogue/Dialogue", order = 0)]
    public class DialogueInformations : ScriptableObject
    {
        public DialoguePosition Position;

        public bool AutoPassToNext;
        public float TimeBeforeNext;
        public Sentence FirstSentence;
    }
}