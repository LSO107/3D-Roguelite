using System.Collections;
using UnityEngine;

namespace Shops
{
    internal sealed class ShopLantern : MonoBehaviour
    {
        private Light m_Light;

        private void Awake()
        {
            m_Light = GetComponent<Light>();
        }

        private void Start()
        {
            DayNightCycle.Instance.RegisterStartOfDayEvent(() => StartCoroutine(Fade(0f, 0.5f)));
            DayNightCycle.Instance.RegisterEndOfDayEvent(() => StartCoroutine(Fade(1f, 0.5f)));

            DayNightCycle.Instance.RegisterScheduledEvent(0.3f, () => StartCoroutine(Fade(0f, 0.5f)));
            DayNightCycle.Instance.RegisterScheduledEvent(0.7f, () => StartCoroutine(Fade(1f, 0.5f)));
        }

        private IEnumerator Fade(float targetIntensity, float seconds)
        {
            var t = 0f;
            var start = m_Light.intensity;

            while (t < seconds)
            {
                t += Time.deltaTime;
                m_Light.intensity = Mathf.Lerp(start, targetIntensity, t / seconds);
                yield return null;
            }

            m_Light.intensity = targetIntensity;
        }
    }
}
