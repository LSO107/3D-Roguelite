using UnityEngine;

internal sealed class PortalTeleport : MonoBehaviour
{
    [SerializeField] private Vector3 m_Location;
    [SerializeField] private bool m_ExitingShop;
    [SerializeField] private bool m_PortalActive = true;

    private void Start()
    {
        DayNightCycle.Instance.RegisterStartOfDayEvent(() => m_PortalActive = false);
        DayNightCycle.Instance.RegisterEndOfDayEvent(() => m_PortalActive = true);
    }

    private void OnTriggerEnter(Collider other)
    {
        if (!other.CompareTag("Player") || m_PortalActive == false)
            return;

        other.transform.position = m_Location;

        if (m_ExitingShop)
        {
            other.transform.rotation = Quaternion.Euler(0,0,0);
        }
    }
}
