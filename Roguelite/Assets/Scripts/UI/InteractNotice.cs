using Extensions;
using TMPro;
using UnityEngine;

namespace UI
{
    internal sealed class InteractNotice : MonoBehaviour
    {
        private CanvasGroup m_CanvasGroup;
        private TextMeshProUGUI m_Message;

        private void Awake()
        {
            m_CanvasGroup = GetComponent<CanvasGroup>();
            m_Message = GetComponentInChildren<TextMeshProUGUI>();
        }

        public void Hide()
        {
            m_CanvasGroup.ToggleCanvasGroup(false);
        }

        public void Show(string message)
        {
            m_CanvasGroup.ToggleCanvasGroup(true);
            m_Message.text = message;
        }
    }
}
