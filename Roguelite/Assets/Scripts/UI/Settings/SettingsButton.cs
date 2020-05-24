using Player;
using UnityEngine;
using UnityEngine.EventSystems;

namespace UI.Settings
{
    internal sealed class SettingsButton : MonoBehaviour, IPointerEnterHandler, IPointerExitHandler
    {
        [SerializeField] private SettingsUI m_Settings = null;

        private PlayerController m_Player;

        private void Start()
        {
            m_Player = PlayerManager.Instance.GetComponent<PlayerController>();
        }

        public void OnPointerEnter(PointerEventData eventData)
        {
            m_Player.ToggleIsInputBlocked(true);
        }

        public void OnPointerExit(PointerEventData eventData)
        {
            if (m_Settings.IsOpen)
                return;

            m_Player.ToggleIsInputBlocked(false);
        }
    }
}
