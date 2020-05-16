using System.Collections.Generic;
using System.Net.Mime;
using System.Runtime.Remoting.Messaging;
using Items.Inventory;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

namespace UI.ShopUI
{
    internal sealed class ShopUI : MonoBehaviour
    {
        [SerializeField] private List<ShopSlotUI> m_Slots = new List<ShopSlotUI>();

        public void Start()
        {
            for (var i = 0; i < m_Slots.Count; i++)
            {
                var eventTrigger = m_Slots[i].GetComponentInChildren<EventTrigger>();

                AddCallbackToButton(eventTrigger, i);
            }
        }

        private void AddCallbackToButton(EventTrigger eventTrigger, int index)
        {
            var eventEntry = new EventTrigger.Entry
            {
                eventID = EventTriggerType.PointerClick
            };

            eventEntry.callback.AddListener(eventData => ClickItem(eventData, index));

            eventTrigger.triggers.Add(eventEntry);
        }

        public void ClickItem(BaseEventData eventData, int slotIndex)
        {
            Debug.Log($"Shop Slot {slotIndex} was clicked.");
        }

        public void UpdateShopItems(List<Equipment> items)
        {
            for (var i = 0; i < m_Slots.Count; i++)
            {
                m_Slots[i].UpdateSlotGroup(items[i]);
            }
        }
    }
}
