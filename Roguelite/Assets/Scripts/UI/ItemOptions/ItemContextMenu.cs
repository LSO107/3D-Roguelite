using Extensions;
using ItemData;
using Items.Inventory;
using TMPro;
using UI.InventoryPanelUI;
using UnityEngine;

namespace UI.ItemOptions
{
    internal sealed class ItemContextMenu : MonoBehaviour
    {
        [SerializeField] private CanvasGroup m_ScreenOverlay;
        [SerializeField] private TextMeshProUGUI m_ItemUseText;

        private CanvasGroup m_ItemContextMenu;
        private PlayerInventory m_Inventory;
        private InventoryUI m_InventoryUI;

        private int m_SlotIndex;
        public bool IsOpen => m_ItemContextMenu.interactable;
        public void HideItemContextMenu() => m_ItemContextMenu.ToggleCanvasGroup(false);

        private void Awake()
        {
            m_ItemContextMenu = GetComponent<CanvasGroup>();
        }

        public void Initialize()
        {
            var pm = GameManager.Instance.PlayerManager;
            m_Inventory = pm.Inventory;
            m_InventoryUI = pm.InventoryUI;
        }

        public void ShowItemContextMenu(int slotIndex)
        {
            m_SlotIndex = slotIndex;
            SetItemOptionText(slotIndex);
            m_ItemContextMenu.ToggleCanvasGroup(true);
            m_ScreenOverlay.ToggleCanvasGroup(true);
            transform.position = Input.mousePosition;
        }

        public void UseItemButton()
        {
            m_Inventory.UseItem(m_SlotIndex);
            m_InventoryUI.UpdateSlots();
            HideItemContextMenu();
        }

        public void DropItemButton()
        {
            var location = GameManager.Instance.PlayerManager.transform.position;

            var item = m_Inventory.GetItemInSlot(m_SlotIndex);

            var groundItem = Instantiate(item.Prefab, location, item.Prefab.transform.rotation);
            groundItem.GetComponentInChildren<GroundItem>().RegisterGroundItem(item.Id);
            GameManager.Instance.ItemDatabase.GroundItems.AddItem(item);

            m_Inventory.RemoveItem(m_SlotIndex);
            m_InventoryUI.UpdateSlots();
            HideItemContextMenu();
        }

        private void SetItemOptionText(int slotIndex)
        {
            var item = m_Inventory.GetItemInSlot(slotIndex);

            if (item == null)
                return;

            switch (item)
            {
                case EquipmentItem _:
                    m_ItemUseText.text = "Equip";
                    break;
                case ConsumableItem _:
                    m_ItemUseText.text = "Drink";
                    break;
                default:
                    m_ItemUseText.text = "Use";
                    break;
            }
        }
    }
}
