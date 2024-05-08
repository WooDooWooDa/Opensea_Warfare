using System.Collections;
using UnityEngine;

namespace Assets.Scripts.UI.Widgets
{
    public class WidgetData
    {

    }

    public class Widget : MonoBehaviour
    {
        protected WidgetData WidgetData;

        public virtual void SetData(WidgetData data)
        {
            WidgetData = data;
        }

    }
}