using Assets.Scripts.Inputs;
using System;

namespace Assets.Scripts.Helpers
{
    public static class Events
    {
        public static class Inputs
        {
            public static event Action<int> OnNumPressed;
            public static event Action<float> OnUpDownChanged;
            public static event Action<float> OnSideChanged;
            public static event Action OnSpaceBarPressed;

            public static void FireOnNumPressed(int value) => OnNumPressed?.Invoke(value);
            public static void FireOnUpDownChanged(float value) => OnUpDownChanged?.Invoke(value);
            public static void FireOnSideChanged(float value) => OnSideChanged?.Invoke(value);
            public static void FireSpaceBarPressed() => OnSpaceBarPressed?.Invoke();
        }

        public static class Actions
        {
            public static event Action<Selectable> OnSelected;
            public static void FireOnSelected(Selectable selectable) => OnSelected?.Invoke(selectable);
        }
    }
}
