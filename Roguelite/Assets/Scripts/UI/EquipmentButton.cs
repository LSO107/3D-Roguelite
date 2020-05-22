using Player;
using UnityEngine;
using UnityEngine.EventSystems;

namespace UI
{
    internal sealed class EquipmentButton : MonoBehaviour, IPointerEnterHandler, IPointerExitHandler
    {
        [SerializeField] private EquipmentUI.EquipmentUI m_Eq;

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
            if (m_Eq.IsOpen)
                return;

            m_Player.ToggleIsInputBlocked(false);
        }
    }
}
