using UnityEngine;

namespace UI
{
    [CreateAssetMenu(fileName = "NewDialogueAction", menuName = "SO/Dialogue/Action", order = 0)]
    public abstract class DialogueAction : ScriptableObject
    {
        public bool DoBeforeText = true;

        public abstract void Execute();
    }
}