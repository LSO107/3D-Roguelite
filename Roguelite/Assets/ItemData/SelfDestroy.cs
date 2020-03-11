using UnityEngine;

namespace ItemData
{
    internal sealed class SelfDestroy : MonoBehaviour
    {
        private float m_Timer;
        private float m_Seconds = 30;

        private void Update()
        {
            m_Timer += Time.deltaTime;

            if (m_Timer >= m_Seconds)
            {
                Destroy(gameObject);
            }
        }
    }
}
