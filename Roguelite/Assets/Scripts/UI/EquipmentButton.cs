using Player;
using UnityEngine;
using UnityEngine.EventSystems;

namespace UI
{
    internal sealed class EquipmentButton : MonoBehaviour, IPointerEnterHandler, IPointerExitHandler, IPointerClickHandler
    {
        [SerializeField] private EquipmentUI.EquipmentUI m_Eq;
        [SerializeField] private ButtonManager m_ButtonManager;

        private PlayerController m_Player;

        private void Awake()
        {
            m_Player = PlayerManager.Instance.GetComponent<PlayerController>();
        }

        public void OnPointerEnter(PointerEventData eventData)
        {
            m_Player.ToggleIsInputBlocked(true);
        }

        public void OnPointerExit(PointerEventData eventData)
        {
            if (m_Eq.IsOpen)
                return;

            m_Player.ToggleIsInputBlocked(false);
        }

        public void OnPointerClick(PointerEventData eventData)
        {
            m_ButtonManager.DisableButtons();
        }
    }
}
