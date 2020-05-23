using UnityEngine;

internal sealed class SelfDestruct : MonoBehaviour
{
    [SerializeField] private float m_Lifetime = 2f;
    private float m_Timer;

    private void Update()
    {
        m_Timer += Time.deltaTime;

        if (m_Timer >= m_Lifetime)
            Destroy(gameObject);
    }
}
