using System;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

namespace Assets.Scripts.Helpers
{
    public static class Helper
    {
        public static Vector3 PointerPosition => Camera.main.ScreenToWorldPoint(Input.mousePosition);

        public static Quaternion SpriteLookAt(Transform transform, Vector3 other)
        {
            var dir = other - transform.position;
            var angle = Mathf.Atan2(dir.y, dir.x) * Mathf.Rad2Deg - 180;
            return Quaternion.AngleAxis(angle, Vector3.forward);
        }
    }
}