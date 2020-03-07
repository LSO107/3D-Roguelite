using System.Collections.Generic;
using Items.Inventory;
using UnityEngine;
using UnityEngine.EventSystems;

namespace UI.InventoryUI
{
    internal sealed class InventoryUI : MonoBehaviour
    {
        private PlayerInventory m_Inventory;

        public List<SlotUI> ItemUI = new List<SlotUI>();

        /// <summary>
        /// Instantiate manually to ensure the inventory is set up before the slots
        /// </summary>
        public void Instantiate()
        {
            m_Inventory = GameManager.Instance.PlayerManager.Inventory;

            for (var i = 0; i < ItemUI.Count; i++)
            {
                var eventTrigger = ItemUI[i].GetComponentInChildren<EventTrigger>();

                AddCallbackToButton(eventTrigger, i);

                UpdateSlot(i);
            }
        }

        public void UpdateSlots()
        {
            for (var i = 0; i < ItemUI.Count; i++)
            {
                var definition = m_Inventory.GetItemInSlot(i);

                ItemUI[i].UpdateItemSprite(definition);
            }
        }

        private void UpdateSlot(int slot)
        {
            var definition = m_Inventory.GetItemInSlot(slot);

            ItemUI[slot].UpdateItemSprite(definition);
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
            m_Inventory.UseItem(slotIndex);
            UpdateSlot(slotIndex);
        }
    }
}
