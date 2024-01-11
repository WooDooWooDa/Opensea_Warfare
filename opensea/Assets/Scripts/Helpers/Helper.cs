using System;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

namespace Assets.Scripts.Helpers
{
    public static class Helper
    {
        public static Vector3 PointerPosition => Camera.main.ScreenToWorldPoint(Input.mousePosition);
    }
}