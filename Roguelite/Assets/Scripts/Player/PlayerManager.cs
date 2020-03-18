using System.Collections.Generic;
using Character;
using Character.Health;
using Currency;
using ItemData;
using Items.Definitions;
using Items.Inventory;
using Player.Experience;
using UI.EquipmentUI;
using UI.InventoryPanelUI;
using UnityEngine;

namespace Player
{
    [RequireComponent(typeof(HealthObject))]
    [RequireComponent(typeof(ExperienceObject))]
    internal sealed class PlayerManager : MonoBehaviour
    {
        public HealthObject Health { get; private set; }
        public ExperienceObject Experience { get; private set; }
        public CurrencyObject Currency { get; private set; }
        public PlayerInventory Inventory { get; private set; }
        public PlayerEquipment Equipment { get; private set; }
        public PlayerStats PlayerStats { get; private set; }

        public InventoryUI InventoryUI;
        public EquipmentUI EquipmentUI;

        private void Awake()
        {
            PlayerStats = new PlayerStats(1);
            var inventory = new List<ItemDefinition>();
            Inventory = new PlayerInventory(inventory);
            Equipment = new PlayerEquipment();

            Health = GetComponent<HealthObject>();
            Experience = GetComponent<ExperienceObject>();
            Currency = GetComponent<CurrencyObject>();
        }

        private void Start()
        {
            InventoryUI.Instantiate();
            EquipmentUI.Instantiate();
        }

        public void EquipItem(int slotIndex)
        {
            if (!(Inventory.GetItemInSlot(slotIndex) is EquipmentItem equipment))
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
