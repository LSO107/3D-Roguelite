using ItemDatabase;
using Items.Definitions;
using UnityEngine;
using UnityEngine.EventSystems;

namespace UI.Tooltip
{
    internal sealed class TooltipPointerHandler : MonoBehaviour, IPointerEnterHandler, IPointerExitHandler, IPointerDownHandler
    {
        private Tooltip m_Tooltip;

        private ItemDefinition m_Item;

        private bool ItemContextMenuOpen => GameManager.Instance.ItemContextMenu.ItemContextMenuOpen;

        private void Start()
        {
            m_Tooltip = GameManager.Instance.Tooltip;
        }

        public void UpdateItem(ItemDefinition item)
        {
            m_Item = item;
        }

        public void OnPointerEnter(PointerEventData eventData)
        {
            if (m_Item == null || ItemContextMenuOpen)
                return;
            
            m_Tooltip.OpenTooltip(m_Item);
        }

        public void OnPointerExit(PointerEventData eventData)
        {
            if (m_Item == null || ItemContextMenuOpen)
                return;

            m_Tooltip.CloseTooltip();
        }

        public void OnPointerDown(PointerEventData eventData)
        {
            m_Tooltip.CloseTooltip();
        }
    }
}
