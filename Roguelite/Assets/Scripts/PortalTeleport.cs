using UI;
using UnityEngine;

internal sealed class PortalTeleport : MonoBehaviour
{
    [SerializeField] private Vector3 m_Location;
    [SerializeField] private Vector3 m_Rotation;
    [SerializeField] private bool m_PortalActive = true;
    [SerializeField] private InteractNotice m_InteractNotice;
    [SerializeField] private string m_NoticeMessage;

    private Transform m_PlayerTransform;

    private bool m_IsInTrigger;

    private void Start()
    {
        DayNightCycle.Instance.RegisterStartOfDayEvent(() => m_PortalActive = false);
        DayNightCycle.Instance.RegisterEndOfDayEvent(() => m_PortalActive = true);
    }

    private void Update()
    {
        if (m_IsInTrigger && Input.GetKeyDown(KeyCode.F))
        {
            m_InteractNotice.Hide();
            m_PlayerTransform.position = m_Location;

            m_PlayerTransform.rotation = Quaternion.Euler(m_Rotation);
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        if (!other.CompareTag("Player") || m_PortalActive == false)
            return;

        m_InteractNotice.Show(m_NoticeMessage);
        m_IsInTrigger = true;
        m_PlayerTransform = other.transform;
    }

    private void OnTriggerExit(Collider other)
    {
        if (!other.CompareTag("Player") || m_PortalActive == false)
            return;

        m_IsInTrigger = false;
        m_InteractNotice.Hide();
        m_PlayerTransform = null;
    }
}
