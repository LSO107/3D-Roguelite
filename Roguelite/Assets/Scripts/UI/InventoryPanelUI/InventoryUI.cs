using System;
using System.Collections.Generic;
using Extensions;
using Items.Inventory;
using UI.ItemOptions;
using UnityEngine;
using UnityEngine.EventSystems;

namespace UI.InventoryPanelUI
{
    internal sealed class InventoryUI : MonoBehaviour
    {
        private PlayerInventory m_Inventory;

        public List<SlotUI> ItemUI = new List<SlotUI>();

        [SerializeField] private ItemContextMenu m_ItemContextMenu;
        [SerializeField] private CanvasGroup m_ScreenClick;

        public void Instantiate()
        {
            m_Inventory = GameManager.Instance.PlayerManager.Inventory;

            for (var i = 0; i < ItemUI.Count; i++)
            {
                var eventTrigger = ItemUI[i].GetComponentInChildren<EventTrigger>();

                AddCallbackToButton(eventTrigger, i);

                UpdateSlot(i);
            }

            m_ItemContextMenu.Instantiate();
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
            var pointerEventData = (PointerEventData) eventData;
            var rightClickIndex = -2;

            if (pointerEventData.pointerId == rightClickIndex)
            {
                if (m_Inventory.GetItemInSlot(slotIndex) == null)
                    return;

                m_ScreenClick.ToggleCanvasGroup(true);
                m_ItemContextMenu.transform.position = Input.mousePosition;
                m_ItemContextMenu.Open(slotIndex);
            }
            else
            {
                m_Inventory.UseItem(slotIndex);
                UpdateSlot(slotIndex);
            }
        }
    }
}
