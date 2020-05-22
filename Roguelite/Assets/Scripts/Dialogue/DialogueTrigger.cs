using UI;
using UnityEngine;

namespace Dialogue
{
    internal sealed class DialogueTrigger : MonoBehaviour
    {
        [SerializeField] private int m_NpcId;
        [SerializeField] private InteractNotice m_InteractNotice;

        private bool m_IsInTrigger;

        private void Update()
        {
            if (m_IsInTrigger && Input.GetKeyDown(KeyCode.F))
            {
                m_InteractNotice.Hide();
                NpcEngine.Instance.StartDialogue(m_NpcId);
            }
        }

        private void OnTriggerEnter(Collider other)
        {
            if (!other.CompareTag("Player"))
                return;

            m_IsInTrigger = true;
            m_InteractNotice.Show("Press F to talk");
        }

        private void OnTriggerExit(Collider other)
        {
            if (!other.CompareTag("Player"))
                return;

            m_IsInTrigger = false;
            m_InteractNotice.Hide();
        }
    }
}
