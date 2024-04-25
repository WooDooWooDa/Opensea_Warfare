using System.Collections.Generic;
using UnityEngine;

namespace UI
{
    [CreateAssetMenu(fileName = "FILENAME", menuName = "SO/Screen/Config", order = 0)]
    public class ScreenConfig : ScriptableObject
    {
        public List<ScreenInformations> Screens;
    }
}