using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnityEngine.InputSystem;

namespace Assets.Scripts.Helpers
{
    public static class Events
    {
        public static class Inputs
        {
            public static Action<int> OnNumPressed;

            public static void FireOnNumPressed(int value) => OnNumPressed?.Invoke(value);
        }
    }
}
