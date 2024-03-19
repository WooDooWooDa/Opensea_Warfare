using System;
using UnityEngine;

namespace Assets.Scripts.Common
{
    public interface IDetectable
    {
        public Action<IDetectable, float, Vector3> OnDetected { get; set; }
        public Action<IDetectable> OnConceal { get; set; }
        public bool TryDetected(float dist, Vector2 dir);
    }
}
