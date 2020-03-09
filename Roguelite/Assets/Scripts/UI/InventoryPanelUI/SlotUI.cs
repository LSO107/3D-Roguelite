using Items.Definitions;
using UI.ItemOptions;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

namespace UI.InventoryPanelUI
{
    internal sealed class SlotUI : MonoBehaviour, IPointerEnterHandler, IPointerExitHandler, IPointerDownHandler
    {
        private Item m_Item;
        private Image m_Image;

        private Tooltip m_Tooltip => GameManager.Instance.Tooltip;

        private void Awake()
        {
            m_Image = GetComponent<Image>();
            UpdateItemSprite(null);
        }

        public void UpdateItemSprite(Item itemDefinition)
        {
            m_Item = itemDefinition;

            if (m_Item != null)
            {
                m_Image.color = Color.white;
                m_Image.sprite = m_Item.Sprite;
            }
            else
            {
                m_Image.color = Color.clear;
            }
        }

        public void OnPointerEnter(PointerEventData eventData)
        {
            if (m_Item == null)
                return;

            m_Tooltip.OpenTooltip(m_Item);
        }

        public void OnPointerExit(PointerEventData eventData)
        {
            if (m_Item == null)
                return;

            m_Tooltip.CloseTooltip();
        }

        public void OnPointerDown(PointerEventData eventData)
        {
            m_Tooltip.CloseTooltip();
        }
    }
}
