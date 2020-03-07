using System.Collections.Generic;
using Items.Definitions;
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
        public Stats Stats;

        private void Start()
        {
            Stats = new Stats(1, 5, 5, 5, 5);

            var inventory = new List<Item>();
            PlayerInventory = new PlayerInventory(inventory);
            PlayerEquipment = new PlayerEquipment();

            InventoryUI.Instantiate();
            EquipmentUI.Instantiate();
        }

        public void EquipItem(int slotIndex)
        {
            if (!(PlayerInventory.GetItemInSlot(slotIndex) is Equipment equipment))
                return;

            var item = PlayerEquipment.GetEquipmentInSlot(equipment.EquipmentSlotId);

            PlayerEquipment.Equip(equipment);
            PlayerInventory.RemoveItem(slotIndex);

            if (item != null)
            {
                PlayerInventory.AddItem(item);
            }

            EquipmentUI.UpdateSlot(equipment.EquipmentSlotId);
        }

        public void UnequipItem(EquipmentSlotId slotId)
        {
            var equipment = PlayerEquipment.GetEquipmentInSlot(slotId);

            if (!PlayerInventory.HasEmptySlots(1))
                return;

            PlayerEquipment.Unequip(slotId);
            PlayerInventory.AddItem(equipment);

            EquipmentUI.UpdateSlot(equipment.EquipmentSlotId);
            InventoryUI.UpdateSlots();
        }
    }
}
