using UnityEngine;

namespace UI
{
    [CreateAssetMenu(fileName = "NewSentence", menuName = "SO/Dialogue/Sentence", order = 0)]
    public class Sentence : ScriptableObject
    {
        public Avatar Avatar;
        [TextArea] public string Text;

        public DialogueAction Action;
        public Sentence NextSentence;
    }
}