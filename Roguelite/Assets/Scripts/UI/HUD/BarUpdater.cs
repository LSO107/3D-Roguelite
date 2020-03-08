using UnityEngine;
using UnityEngine.UI;

namespace UI.HUD
{
    internal sealed class BarUpdater : MonoBehaviour
    {
        private Slider m_Bar;

        private void Start()
        {
            m_Bar = GetComponent<Slider>();
        }

        public void UpdateBar(float currentValue, float maxValue)
        {
            m_Bar.value = currentValue / maxValue;
        }
    }
}
