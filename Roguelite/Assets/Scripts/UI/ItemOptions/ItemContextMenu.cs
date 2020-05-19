using Character.Movement;
using Extensions;
using ItemData;
using Items;
using Items.Inventory;
using Player;
using TMPro;
using UI.InventoryPanelUI;
using UI.Tooltip;
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
        private GroundItemManager m_GroundItemManager;

        private int m_SlotIndex;
        public bool IsOpen => m_ItemContextMenu.interactable;

        private void Awake()
        {
            m_ItemContextMenu = GetComponent<CanvasGroup>();
            m_GroundItemManager = GroundItemManager.Instance;
        }

        public void Initialize()
        {
            var pm = PlayerManager.Instance;
            m_Inventory = pm.Inventory;
            m_InventoryUI = pm.InventoryUI;
        }

        public void ShowItemContextMenu(int slotIndex)
        {
            PlayerManager.Instance.GetComponent<PlayerController>().ToggleInputBlocked();

            m_SlotIndex = slotIndex;
            SetItemOptionText(slotIndex);
            m_ItemContextMenu.ToggleCanvasGroup(true);
            m_ScreenOverlay.ToggleCanvasGroup(true);
            transform.position = Input.mousePosition;
        }

        public void HideItemContextMenu()
        {
            m_ItemContextMenu.ToggleCanvasGroup(false);
            PlayerManager.Instance.GetComponent<PlayerController>().ToggleInputBlocked();
        }

        public void UseItemButton()
        {
            m_Inventory.UseItem(m_SlotIndex);
            m_InventoryUI.UpdateSlots();
            HideItemContextMenu();
        }

        public void DropItemButton()
        {
            var location = PlayerManager.Instance.transform.position;

            var item = m_Inventory.GetItemInSlot(m_SlotIndex);

            var groundItem = Instantiate(item.Prefab, location + new Vector3(0, 0.1f, 0), item.Prefab.transform.rotation);
            groundItem.GetComponentInChildren<GroundItem>().RegisterGroundItem(item.Id);
            m_GroundItemManager.AddItem(item);

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
                case Equipment _:
                    m_ItemUseText.text = "Equip";
                    break;
                case Consumable _:
                    m_ItemUseText.text = "Drink";
                    break;
                default:
                    m_ItemUseText.text = "Use";
                    break;
            }
        }
    }
}
