using System;
using UnityEngine;

namespace Assets.Scripts.Common
{
    public interface IDetectable
    {
        public float DetectableRange { get; set; }
        public Action<float, Vector3> OnDetected { get; set; }
    }
}
