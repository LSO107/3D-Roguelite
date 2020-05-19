using System.Collections.Generic;
using Items.Inventory;
using Player;
using UI.ItemOptions;
using UnityEngine;
using UnityEngine.EventSystems;

namespace UI.InventoryPanelUI
{
    internal sealed class InventoryUI : MonoBehaviour, IPointerEnterHandler, IPointerExitHandler
    {
        private PlayerInventory m_Inventory;

        public List<InventorySlotUI> ItemUI = new List<InventorySlotUI>();

        [SerializeField] private ItemContextMenu m_ItemContextMenu;

        public void Instantiate()
        {
            m_Inventory = PlayerManager.Instance.Inventory;

            for (var i = 0; i < ItemUI.Count; i++)
            {
                var eventTrigger = ItemUI[i].GetComponentInChildren<EventTrigger>();

                AddCallbackToButton(eventTrigger, i);

                UpdateSlot(i);
            }

            m_ItemContextMenu.Initialize();
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

                m_ItemContextMenu.ShowItemContextMenu(slotIndex);
            }
            else
            {
                if (m_ItemContextMenu.IsOpen)
                    m_ItemContextMenu.HideItemContextMenu();

                m_Inventory.UseItem(slotIndex);
                UpdateSlot(slotIndex);
            }
        }

        public void OnPointerEnter(PointerEventData eventData)
        {
            PlayerManager.Instance.GetComponent<PlayerController>().ToggleIsInputBlocked(true);
        }

        public void OnPointerExit(PointerEventData eventData)
        {
            PlayerManager.Instance.GetComponent<PlayerController>().ToggleIsInputBlocked(false);
        }
    }
}
