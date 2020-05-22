using UnityEngine;
using Utils;

namespace UI.Settings
{
    internal sealed class SettingsUI : MonoBehaviour
    {
        [SerializeField] private ButtonManager m_ButtonManager;
        private CanvasGroup m_CanvasGroup;

        public bool IsOpen => m_CanvasGroup.interactable;

        private void Awake()
        {
            m_CanvasGroup = GetComponent<CanvasGroup>();
        }

        public void OpenSettings()
        {
            m_ButtonManager.DisableButtons();
            UserInterfaceUtils.OpenUserInterface(m_CanvasGroup);
        }

        public void CloseSettings()
        {
            UserInterfaceUtils.CloseUserInterface(m_CanvasGroup);
            m_ButtonManager.EnableButtons();
        }
    }
}
