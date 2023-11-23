using Assets.Scripts.Inputs;
using System;

namespace Assets.Scripts.Helpers
{
    public static class Events
    {
        public static class Inputs
        {
            public static Action<int> OnNumPressed;

            public static void FireOnNumPressed(int value) => OnNumPressed?.Invoke(value);
        }

        public static class Actions
        {
            public static Action<Selectable> OnSelected;
            public static void FireOnSelected(Selectable selectable) => OnSelected?.Invoke(selectable);
        }
    }
}
