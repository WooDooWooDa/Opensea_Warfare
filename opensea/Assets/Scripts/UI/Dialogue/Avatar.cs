using UnityEngine;

namespace UI
{
    public enum AvatarExpression
    {
        Default
    }
    
    [CreateAssetMenu(fileName = "NewAvatar", menuName = "SO/Dialogue/Avatar", order = 0)]
    public class Avatar : ScriptableObject
    {
        public Sprite[] Imgs; //One image for each expression
        public string Name;
    }
}