using Assets.Scripts.Inputs;
using Assets.Scripts.Managers;
using UnityEngine;

namespace Env
{
    public class Ocean : MonoBehaviour, ISelectable
    {
        
        
        public void OnSelect()
        {
            //Main.Instance.GetManager<FleetManager>().FocusOn(null);
        }

        public void OnDeselect()
        {
            
        }
    }
}