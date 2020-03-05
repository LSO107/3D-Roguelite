using System.Collections.Generic;
using Items.Definitions;
using Items.EquipmentSystem;
using Items.Inventory;
using UI.EquipmentUI;
using UI.InventoryUI;
using UnityEngine;

namespace Player
{
    internal sealed class PlayerManager : MonoBehaviour
    {
        public PlayerInventory PlayerInventory;
        public PlayerEquipment PlayerEquipment;
        public InventoryUI InventoryUI;
        public EquipmentUI EquipmentUI;

        private void Start()
        {
            var inventory = new List<Item>();
            PlayerInventory = new PlayerInventory(inventory);

            PlayerEquipment = new PlayerEquipment();

            InventoryUI.Instantiate();
            EquipmentUI.Instantiate();
        }

        public void EquipItem(int slotIndex)
        {
            var equipment = PlayerInventory.GetItemInSlot(slotIndex) as Equipment;

            // May have been called by mistake, item in the given slot was not equipment
            if (equipment == null)
                return;

            // Check player's equipment to see if something is in there already
            var item = PlayerEquipment.GetEquipmentInSlot(equipment.EquipmentSlotId);

            // Set equipment slot to be equal to the thing we're trying to equip
            PlayerEquipment.Equip(equipment);

            PlayerInventory.RemoveItem(slotIndex);

            if (item != null)
            {
                PlayerInventory.AddItem(item);
            }

            GameManager.Instance.PlayerManager.EquipmentUI.UpdateSlot(equipment.EquipmentSlotId);
        }

        public void UnequipItem(EquipmentSlotId slotId)
        {
            // Get item currently in slot
            var equipment = PlayerEquipment.GetEquipmentInSlot(slotId);

            // If inventory is full, do nothing
            if (!PlayerInventory.HasEmptySlots(1))
                return;

            // Otherwise, set the equipment slot to null
            PlayerEquipment.Unequip(slotId);

            // Add the item to the inventory
            PlayerInventory.AddItem(equipment);

            // Finally, update the inventory and equipment's user interface
            GameManager.Instance.PlayerManager.EquipmentUI.UpdateSlot(equipment.EquipmentSlotId);
            GameManager.Instance.PlayerManager.InventoryUI.UpdateSlots();
        }
    }
}
