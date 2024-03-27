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
        
        static string[] Directions = { "N", "NbE", "NNE", "NEbN", "NE", "NEbE", "ENE", "EbN", "E", "EbS", "ESE", "SEbE", "SE", "SEbS", "SSE", "SbE", 
            "S", "SbW", "SSW", "SWbS", "SW", "SWbW", "WSW", "WbS", "W", "WbN", "WNW", "NWbW", "NW", "NWbN", "NNW", "NbW" };
        
        public static string GetStringDirection(double angle)
        {
            var numDirections = Directions.Length;
            angle = 360 - angle;
            return Directions[(int)Math.Round((angle % 360) / (360.0 / numDirections)) % numDirections];
        }
    }
}