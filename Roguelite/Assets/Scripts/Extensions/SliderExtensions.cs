using UnityEngine;
using UnityEngine.UI;

namespace Extensions
{
    internal static class SliderExtensions
    {
        public static void UpdateBarValue(this Slider slider, int currentValue, int maxValue)
        {
            slider.value = (float) currentValue / maxValue;
        }
    }
}
