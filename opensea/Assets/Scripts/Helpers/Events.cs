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

        public static class Ship
        {
            public static event Action<Ships.Ship, bool> IsAiming;
            public static event Action<Ships.Ship, float> ChangedSpeed;

            public static void FireIsAiming(Ships.Ship ship, bool value) => IsAiming?.Invoke(ship, value);
            public static void FireChangedSpeed(Ships.Ship ship, float value) => ChangedSpeed?.Invoke(ship, value);
        }
    }
}
