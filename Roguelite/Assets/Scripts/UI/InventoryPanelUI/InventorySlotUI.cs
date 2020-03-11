using ItemDatabase;
using Items.Definitions;
using UI.Tooltip;
using UnityEngine;
using UnityEngine.UI;

namespace UI.InventoryPanelUI
{
    internal sealed class InventorySlotUI : MonoBehaviour
    {
        private ItemDefinition m_Item;
        private Image m_Image;

        private TooltipPointerHandler m_TooltipPointerHandler;

        private void Awake()
        {
            m_TooltipPointerHandler = GetComponent<TooltipPointerHandler>();
            m_Image = GetComponent<Image>();
            UpdateItemSprite(null);
        }

        public void UpdateItemSprite(ItemDefinition itemDefinition)
        {
            m_TooltipPointerHandler.UpdateItem(itemDefinition);
            m_Item = itemDefinition;

            if (m_Item != null)
            {
                m_Image.color = Color.white;
                m_Image.sprite = m_Item.itemIcon;
            }
            else
            {
                m_Image.color = Color.clear;
            }
        }
    }
}
