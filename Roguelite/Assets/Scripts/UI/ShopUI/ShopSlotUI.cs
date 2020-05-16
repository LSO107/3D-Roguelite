using Items.Inventory;
using UI.Tooltip;
using UnityEngine;
using UnityEngine.UI;

namespace UI.ShopUI
{
    internal sealed class ShopSlotUI : MonoBehaviour
    {
        private Item m_Item;
        private Image m_Image;

        [SerializeField] private Text m_Price;
        [SerializeField] private Button m_BuyButton;

        private TooltipPointerHandler m_TooltipPointerHandler;

        private static float PlayerGold => GameManager.Instance.PlayerManager.Currency.CurrencyQuantity;

        private void Awake()
        {
            m_TooltipPointerHandler = GetComponent<TooltipPointerHandler>();
            m_Image = GetComponent<Image>();
            UpdateSlotGroup(null);
        }

        public void UpdateSlotGroup(Item item)
        {
            m_TooltipPointerHandler.UpdateItem(item);
            UpdateItemSprite(item);
            m_Price.text = item.GoldCost.ToString();
            m_Price.color = PlayerGold >= item.GoldCost ? Color.yellow : Color.red;
            m_BuyButton.interactable = PlayerGold >= item.GoldCost;
        }

        private void UpdateItemSprite(Item item)
        {
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
