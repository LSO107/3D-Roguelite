using UnityEngine;

namespace UI
{
    internal sealed class DisplayMessage : MonoBehaviour
    {
        [SerializeField] private InteractNotice m_InteractNotice;
        [SerializeField] private string m_Message;

        private void OnTriggerEnter(Collider other)
        {
            m_InteractNotice.Show(m_Message);
        }

        private void OnTriggerExit(Collider other)
        {
            m_InteractNotice.Hide();
        }
    }
}
