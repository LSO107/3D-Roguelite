using System;
using System.Collections.Generic;
using System.Linq;
using Items.Definitions;
using Player;
using UnityEngine;

namespace Items.Inventory
{
    internal sealed class PlayerInventory
    {
        private static PlayerManager PlayerManager => GameManager.Instance.PlayerManager;

        private readonly Slot[] m_Slots;
        private const int InventorySize = 6;

        public PlayerInventory(IEnumerable<Item> items)
        {
            m_Slots = new Slot[InventorySize];
            for (var i = 0; i < m_Slots.Length; i++)
            {
                m_Slots[i] = new Slot(null);
            }
            var itemArray = items.ToArray();
            for (var i = 0; i < itemArray.Length; i++)
            {
                m_Slots[i].SetItem(itemArray[i]);
            }
        }

        /// <summary>
        /// Adds the item to the first available inventory slot
        /// </summary>
        public void AddItem(Item itemDefinition)
        {
            if (!HasEmptySlots(1))
            {
                throw new Exception("Could not add item to inventory as inventory is full");
            }

            var firstEmptySlot = GetFirstEmptySlot();

            m_Slots[firstEmptySlot].SetItem(itemDefinition);
            PlayerManager.InventoryUI.UpdateSlots();
        }

        /// <summary>
        /// Removes the item from the slotIndex
        /// </summary>
        public void RemoveItem(int slotIndex)
        {
            var slot = m_Slots[slotIndex];

            if (slot.ItemDefinition == null)
            {
                throw new Exception($"Could not empty slot at index {slotIndex} as it was already empty.");
            }

            slot.Empty();
        }

        /// <summary>
        /// Removes the item type from the inventory
        /// </summary>
        public void RemoveItem(Type itemType)
        {
            var slotsWithItems = m_Slots.Where(slot => slot.ItemDefinition != null);
            var matchingSlot = slotsWithItems.FirstOrDefault
                    (slot => slot.ItemDefinition.GetType() == itemType);

            if (matchingSlot == null)
            {
                throw new Exception($"Could not remove Item {itemType} as it did not exist inventory.");
            }

            matchingSlot.Empty();
        }

        public bool HasEmptySlots(int amountOfSlots)
        {
            return m_Slots.Count(slot => slot.ItemDefinition == null) >= amountOfSlots;
        }

        public int GetFirstEmptySlot()
        {
            for (var i = 0; i < m_Slots.Length; i++)
            {
                if (m_Slots[i].ItemDefinition == null)
                {
                    return i;
                }
            }

            Debug.Log("No free slots available.");
            return 0;
        }

        public Item GetItemInSlot(int slotIndex)
        {
            return m_Slots[slotIndex].ItemDefinition;
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
