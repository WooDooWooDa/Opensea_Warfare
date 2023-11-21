using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnityEngine;
using UnityEngine.EventSystems;

namespace Assets.Scripts.Inputs
{
    public class OnMouseInputSystem : MonoBehaviour, IPointerDownHandler
    {
        private ISelectable m_relatedSelectable;

        private void Start()
        {
            m_relatedSelectable = GetComponent<ISelectable>();
        }

        public void OnPointerDown(PointerEventData eventData)
        {
            m_relatedSelectable.OnSelect();
        }
    }
}
