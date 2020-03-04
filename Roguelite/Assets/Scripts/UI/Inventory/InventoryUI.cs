using System;
using System.Collections.Generic;
using Items.Inventory;
using UnityEngine;
using UnityEngine.EventSystems;

namespace UI.Inventory
{
    internal sealed class InventoryUI : MonoBehaviour
    {
        private PlayerInventory m_Inventory;

        public List<SlotUI> ItemUI = new List<SlotUI>();

        public void Instantiate()
        {
            m_Inventory = GameManager.Instance.PlayerManager.PlayerInventory;

            for (var i = 0; i < ItemUI.Count; i++)
            {
                var eventTrigger = ItemUI[i].GetComponentInChildren<EventTrigger>();

                AddCallbackToButton(eventTrigger, i);

                UpdateSlot(i);
            }
        }

        /// <summary>
        /// Iterate over inventory slots,
        /// call UpdateItemSprite on every slot
        /// </summary>
        public void UpdateSlots()
        {
            for (var i = 0; i < ItemUI.Count; i++)
            {
                var definition = m_Inventory.GetItemInSlot(i);

                ItemUI[i].UpdateItemSprite(definition);
            }
        }

        /// <summary>
        /// Update the slot indexes item placeholder
        /// sprite with the itemDefinition sprite
        /// </summary>
        private void UpdateSlot(int slot)
        {
            var definition = m_Inventory.GetItemInSlot(slot);

            ItemUI[slot].UpdateItemSprite(definition);
        }

        /// <summary>
        /// Waits for mouse click to trigger an event
        /// </summary>
        private void AddCallbackToButton(EventTrigger eventTrigger, int index)
        {
            var eventEntry = new EventTrigger.Entry
            {
                eventID = EventTriggerType.PointerClick,
            };

            eventEntry.callback.AddListener(eventData => ClickItem(eventData, index));

            eventTrigger.triggers.Add(eventEntry);
        }

        /// <summary>
        /// If item clicked, use the item then update the slot
        /// </summary>
        public void ClickItem(BaseEventData eventData, int slotIndex)
        {
            m_Inventory.UseItem(slotIndex);
            UpdateSlot(slotIndex);
        }
    }
}
