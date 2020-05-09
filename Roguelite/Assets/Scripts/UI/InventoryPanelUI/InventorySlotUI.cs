using ItemData;
using Items.Definitions;
using Items.Inventory;
using UI.Tooltip;
using UnityEngine;
using UnityEngine.UI;

namespace UI.InventoryPanelUI
{
    internal sealed class InventorySlotUI : MonoBehaviour
    {
        private Item m_Item;
        private Image m_Image;

        private TooltipPointerHandler m_TooltipPointerHandler;

        private void Awake()
        {
            m_TooltipPointerHandler = GetComponent<TooltipPointerHandler>();
            m_Image = GetComponent<Image>();
            UpdateItemSprite(null);
        }

        public void UpdateItemSprite(Item item)
        {
            m_TooltipPointerHandler.UpdateItem(item);
            m_Item = item;

            if (m_Item != null)
            {
                m_Image.color = Color.white;
                m_Image.sprite = m_Item.Icon;
            }
            else
            {
                m_Image.color = Color.clear;
            }
        }
    }
}
