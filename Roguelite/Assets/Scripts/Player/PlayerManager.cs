using System.Collections.Generic;
using Health;
using Items.Definitions;
using Items.Inventory;
using UI.EquipmentUI;
using UI.InventoryUI;
using UnityEngine;
using UnityEngine.UI;

namespace Player
{
    [RequireComponent(typeof(HealthObject))]
    internal sealed class PlayerManager : MonoBehaviour
    {
        public HealthObject Health { get; private set; }
        public PlayerInventory Inventory { get; private set; }
        public PlayerEquipment Equipment { get; private set; }
        public Stats Stats { get; private set; }

        public Slider HealthBarUI;
        public InventoryUI InventoryUI;
        public EquipmentUI EquipmentUI;

        private void Start()
        {
            Stats = new Stats(1, 5, 5, 5, 5);
            var inventory = new List<Item>();
            Inventory = new PlayerInventory(inventory);
            Equipment = new PlayerEquipment();
            Health = GetComponent<HealthObject>();

            InventoryUI.Instantiate();
            EquipmentUI.Instantiate();
        }

        public void EquipItem(int slotIndex)
        {
            if (!(Inventory.GetItemInSlot(slotIndex) is Equipment equipment))
                return;

            var item = Equipment.GetEquipmentInSlot(equipment.EquipmentSlotId);

            Equipment.Equip(equipment);
            Inventory.RemoveItem(slotIndex);

            if (item != null)
            {
                Inventory.AddItem(item);
            }

            EquipmentUI.UpdateSlot(equipment.EquipmentSlotId);
        }

        public void UnequipItem(EquipmentSlotId slotId)
        {
            var equipment = Equipment.GetEquipmentInSlot(slotId);

            if (!Inventory.HasEmptySlots(1))
                return;

            Equipment.Unequip(slotId);
            Inventory.AddItem(equipment);

            EquipmentUI.UpdateSlot(equipment.EquipmentSlotId);
            InventoryUI.UpdateSlots();
        }
    }
}
