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

        private void Start()
        {
            m_CanvasGroup = GetComponent<CanvasGroup>();
        }

        public void Instantiate()
        {
            var pm = GameManager.Instance.PlayerManager;
            m_Inventory = pm.Inventory;
            m_InventoryUI = pm.InventoryUI;
        }

        public void Open(int slotIndex)
        {
            m_CanvasGroup.ToggleCanvasGroup(true);
            m_SlotIndex = slotIndex;
        }

        public void Close()
        {
            m_CanvasGroup.ToggleCanvasGroup(false);
            m_SlotIndex = -1;
        }

        public void Use()
        {
            m_Inventory.UseItem(m_SlotIndex);
            m_InventoryUI.UpdateSlots();
            Close();
        }

        public void Drop()
        {
            var location = GameManager.Instance.PlayerManager.transform.position;

            var inventoryItem = m_Inventory.GetItemInSlot(m_SlotIndex);

            if (inventoryItem is EquipmentItem)
            {
                var item = GameManager.Instance.ItemDatabase.GetItem(inventoryItem.Id) as EquipmentItem;

                if (item == null)
                {
                    Debug.Log("ITEM WAS NOT IN DATABASE");
                }
                else
                {
                    var newItem = Instantiate(item.Prefab, location, Quaternion.Euler(0, 0, 90));
                    newItem.GetComponent<NpcDrop>().SetItemId(item.Id);
                }
            }

            m_Inventory.RemoveItem(m_SlotIndex);
            m_InventoryUI.UpdateSlots();
            Close();
        }
    }
}
