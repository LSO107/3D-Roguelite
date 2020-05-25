using UnityEngine;

namespace UI
{
    internal sealed class DisplayMessage : MonoBehaviour
    {
        [SerializeField] private InteractNotice m_InteractNotice;
        [SerializeField] private string m_Message;

        private Collider m_TriggerBox;

        private void Awake()
        {
            m_TriggerBox = GetComponent<Collider>();
        }

        private void Start()
        {
            DayNightCycle.Instance.RegisterStartOfDayEvent(() => m_TriggerBox.enabled = false);
            DayNightCycle.Instance.RegisterEndOfDayEvent(() => m_TriggerBox.enabled = true);
        }

        private void OnTriggerEnter(Collider other)
        {
            if (!other.CompareTag("Player"))
                return;

            m_InteractNotice.Show(m_Message);
        }

        private void OnTriggerExit(Collider other)
        {
            if (!other.CompareTag("Player"))
                return;

            m_InteractNotice.Hide();
        }
    }
}
