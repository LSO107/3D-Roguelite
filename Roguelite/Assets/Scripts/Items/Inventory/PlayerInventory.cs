using System;
using System.Collections.Generic;
using System.Linq;
using Items.Definitions;
using UnityEngine;

namespace Items.Inventory
{
    internal sealed class PlayerInventory
    {
        private readonly Slot[] Slots;
        private const int InventorySize = 6;

        public PlayerInventory(IEnumerable<Item> items)
        {
            Slots = new Slot[InventorySize];
            for (var i = 0; i < Slots.Length; i++)
            {
                Slots[i] = new Slot(null);
            }
            var itemArray = items.ToArray();
            for (var i = 0; i < itemArray.Length; i++)
            {
                Slots[i].SetItem(itemArray[i]);
            }
            Debug.Log($"Slots length: {Slots.Length}. itemArray : {itemArray.Length}");
        }

        public void AddItem(Item itemDefinition)
        {
            Debug.Log($"Attempting to add {itemDefinition.Name} to the inventory");
            if (!HasEmptySlots(1))
            {
                throw new Exception("Could not add item to inventory as inventory is full");
            }

            var firstEmptySlot = GetFirstEmptySlot();

            Debug.Log($"first empty slot = {firstEmptySlot}");

            Slots[firstEmptySlot].SetItem(itemDefinition);
            GameManager.Instance.InventoryUI.UpdateSlots();
            Debug.Log($"{itemDefinition.Name} was added to the inventory!");
        }

        public void RemoveItem(int slotIndex)
        {
            var slot = Slots[slotIndex];

            if (slot.ItemDefinition == null)
            {
                throw new Exception($"Could not empty slot at index {slotIndex} as it was already empty.");
            }

            slot.Empty();
        }

        public void RemoveItem(Type itemType)
        {
            var slotsWithItems = Slots.Where(slot => slot.ItemDefinition != null);
            var matchingSlot = slotsWithItems.FirstOrDefault
                    (slot => slot.ItemDefinition.GetType() == itemType);

            if (matchingSlot == null)
            {
                throw new Exception($"Could not remove Item {itemType} as it did not exist inventory.");
            }

            matchingSlot.Empty();
        }

        /// <summary>
        /// Iterates over the inventory to check if there are X amount of slots empty
        /// </summary>
        public bool HasEmptySlots(int amountOfSlots)
        {
            return Slots.Count(slot => slot.ItemDefinition == null) >= amountOfSlots;
        }

        /// <summary>
        /// If there are any empty slots, finds the first one in the inventory
        /// </summary>
        public int GetFirstEmptySlot()
        {
            for (var i = 0; i < Slots.Length; i++)
            {
                if (Slots[i].ItemDefinition == null)
                {
                    return i;
                }
            }

            Debug.Log("No free slots available.");
            return 0;
        }

        /// <summary>
        /// Returns the <see cref="Item"/> in the slot at the given index.
        /// Can return null.
        /// </summary>
        public Item GetItemInSlot(int slotIndex)
        {
            return Slots[slotIndex].ItemDefinition;
        }

        /// <summary>
        /// Use the item in the slot with the given index
        /// </summary>
        public void UseItem(int slotIndex)
        {
            var item = GetItemInSlot(slotIndex);

            var consumable = item as Consumable;
            if (consumable != null)
            {
                consumable.Use();
                RemoveItem(slotIndex);
            }
            else if (item is Equipment)
            {
                //GameManager.Instance.Player.EquipItem(slotIndex);
            }
        }
    }
}
