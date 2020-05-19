using System.Collections.Generic;
using Character.Movement;
using Extensions;
using Items.Definitions;
using Items.Inventory;
using Player;
using Shops;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

namespace UI.ShopUI
{
    internal sealed class ShopUI : MonoBehaviour
    {
        [SerializeField] private List<ShopSlotUI> m_Slots = new List<ShopSlotUI>();
        [SerializeField] private List<Button> m_BuyButton = new List<Button>();

        [SerializeField] private Text m_PlayerCurrencyText;
        [SerializeField] private Shop m_Shop;

        private CanvasGroup m_CanvasGroup;

        private static float CurrencyQuantity => PlayerManager.Instance.Currency.Quantity;
        public void UpdateCurrencyQuantityText() => m_PlayerCurrencyText.text = CurrencyQuantity.ToString();

        private void Awake()
        {
            m_CanvasGroup = GetComponent<CanvasGroup>();
        }

        private void Start()
        {
            for (var i = 0; i < m_Slots.Count; i++)
            {
                var eventTrigger = m_BuyButton[i].GetComponent<EventTrigger>();

                AddCallbackToButton(eventTrigger, i);
            }

            UpdateCurrencyQuantityText();
        }

        private void AddCallbackToButton(EventTrigger eventTrigger, int index)
        {
            var eventEntry = new EventTrigger.Entry
            {
                eventID = EventTriggerType.PointerClick
            };

            eventEntry.callback.AddListener(eventData => PurchaseItem(eventData, index));

            eventTrigger.triggers.Add(eventEntry);
        }

        public void PurchaseItem(BaseEventData eventData, int slotIndex)
        {
            m_Shop.PurchaseItem(slotIndex);
            UpdateCurrencyQuantityText();
        }

        public void UpdateShopItems(IList<Item> items)
        {
            for (var i = 0; i < m_Slots.Count; i++)
            {
                m_Slots[i].UpdateSlotGroup(items[i]);
            }
        }

        public void OpenShop()
        {
            PlayerManager.Instance.DisableInput();
            m_CanvasGroup.ToggleCanvasGroup(true);
        }

        public void CloseShop()
        {
            PlayerManager.Instance.EnableInput();
            m_CanvasGroup.ToggleCanvasGroup(false);
        }
    }
}
