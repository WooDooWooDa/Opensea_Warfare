using Assets.Scripts.Helpers;
using System;
using UnityEngine;
using UnityEngine.EventSystems;

namespace Assets.Scripts.Inputs
{
    [RequireComponent(typeof(Collider2D))]
    public class Selectable : MonoBehaviour, IPointerDownHandler
    {
        private ISelectable m_relatedSelectable;

        private void Start()
        {
            m_relatedSelectable = GetComponent<ISelectable>();
        }

        public void OnPointerDown(PointerEventData eventData)
        {
            if (eventData.button != PointerEventData.InputButton.Left) return;
            
            m_relatedSelectable.OnSelect();
        }
    }
}
