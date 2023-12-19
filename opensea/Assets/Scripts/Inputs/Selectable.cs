using Assets.Scripts.Helpers;
using UnityEngine;
using UnityEngine.EventSystems;

namespace Assets.Scripts.Inputs
{
    [RequireComponent(typeof(Collider2D), typeof(ISelectable))]
    public class Selectable : MonoBehaviour, IPointerDownHandler
    {
        private ISelectable m_relatedSelectable;

        private bool m_listeningToSelection = true;
        
        private void Start()
        {
            m_relatedSelectable = GetComponent<ISelectable>();
            
            Events.Ship.IsAiming += (ship, value) =>
            {
                m_listeningToSelection = !value;
            };
        }

        public void OnPointerDown(PointerEventData eventData)
        {
            if (!m_listeningToSelection) return;
            
            if (eventData.button != PointerEventData.InputButton.Left) return;
            
            m_relatedSelectable.OnSelect();
        }
    }
}
