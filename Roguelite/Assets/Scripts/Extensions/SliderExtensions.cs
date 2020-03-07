using UnityEngine;
using UnityEngine.UI;

namespace Extensions
{
    internal static class SliderExtensions
    {
        public static void UpdateBarValue(this Slider slider, float currentValue, float maxValue)
        {
            slider.value = currentValue / maxValue;
        }
    }
}
