using System;
using Extensions;
using ItemData;
using Items.Inventory;
using UI.InventoryPanelUI;
using UnityEngine;

namespace UI.ItemContextMenu
{
    internal sealed class ItemContextMenu : MonoBehaviour
    {
        private CanvasGroup m_CanvasGroup;
        private PlayerInventory m_Inventory;
        private InventoryUI m_InventoryUI;
        private int m_SlotIndex;

        public bool ItemContextMenuOpen => m_CanvasGroup.interactable;

        private void Awake()
        {
            m_CanvasGroup = GetComponent<CanvasGroup>();
        }

        public void Initialize()
        {
            var pm = GameManager.Instance.PlayerManager;
            m_Inventory = pm.Inventory;
            m_InventoryUI = pm.InventoryUI;
        }

        public void OpenContextMenu(int slotIndex)
        {
            m_CanvasGroup.ToggleCanvasGroup(true);
            m_SlotIndex = slotIndex;
        }

        public void CloseContextMenu()
        {
            m_CanvasGroup.ToggleCanvasGroup(false);
            m_SlotIndex = -1;
        }

        public void Use()
        {
            m_Inventory.UseItem(m_SlotIndex);
            m_InventoryUI.UpdateSlots();
            CloseContextMenu();
        }

        public void Drop()
        {
            var location = GameManager.Instance.PlayerManager.transform.position;

            var item = m_Inventory.GetItemInSlot(m_SlotIndex);

            var groundItem = Instantiate(item.Prefab, location, item.Prefab.transform.rotation);
            groundItem.GetComponentInChildren<GroundItem>().RegisterGroundItem(item.Id);
            GameManager.Instance.ItemDatabase.GroundItems.AddItem(item);

            m_Inventory.RemoveItem(m_SlotIndex);
            m_InventoryUI.UpdateSlots();
            CloseContextMenu();
        }
    }
}
