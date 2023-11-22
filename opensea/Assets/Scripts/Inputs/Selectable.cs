using System;
using UnityEngine;
using UnityEngine.EventSystems;

namespace Assets.Scripts.Inputs
{
    public class Selectable : MonoBehaviour, IPointerDownHandler
    {
        private ISelectable m_relatedSelectable;

        private void Start()
        {
            m_relatedSelectable = GetComponent<ISelectable>();
        }

        public void OnPointerDown(PointerEventData eventData)
        {
            if (eventData.button == PointerEventData.InputButton.Left)
                m_relatedSelectable.OnSelect();
        }
    }
}
