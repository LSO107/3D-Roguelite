using Character.Movement;
using Player;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

namespace UI
{
    internal sealed class EquipmentButton : MonoBehaviour, IPointerEnterHandler, IPointerExitHandler
    {
        private Button m_Button;

        private EquipmentUI.EquipmentUI m_Eq;

        private void Awake()
        {
            m_Button = GetComponent<Button>();
            m_Eq = PlayerManager.Instance.EquipmentUI;
        }

        public void OpenEquipmentUI()
        {
            PlayerManager.Instance.EquipmentUI.OpenEquipmentInterface();
        }

        public void CloseEquipmentUI()
        {
            PlayerManager.Instance.EquipmentUI.CloseEquipmentInterface();
        }

        public void OnPointerEnter(PointerEventData eventData)
        {
            PlayerManager.Instance.GetComponent<PlayerController>().ToggleIsInputBlocked(true);
        }

        public void OnPointerExit(PointerEventData eventData)
        {
            if (m_Eq.IsEquipmentOpen)
                return;

            PlayerManager.Instance.GetComponent<PlayerController>().ToggleIsInputBlocked(false);
        }
    }
}
