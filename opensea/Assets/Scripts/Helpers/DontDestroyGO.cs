using UnityEngine;

namespace Assets.Scripts.Helpers
{
    public class DontDestroyGO : MonoBehaviour
    {
        void Awake()
        {
            DontDestroyOnLoad(this.gameObject);
        }
    }
}