using System;
using System.Collections.Generic;
using System.Linq;
using Player;
using UnityEngine;

namespace Items.Inventory
{
    internal sealed class PlayerInventory
    {
        private static PlayerManager PlayerManager => PlayerManager.Instance;

        private readonly InventorySlot[] m_Slots;
        private const int InventorySize = 6;

        public PlayerInventory(IEnumerable<Item> items)
        {
            m_Slots = new InventorySlot[InventorySize];
            for (var i = 0; i < m_Slots.Length; i++)
            {
                m_Slots[i] = new InventorySlot(null);
            }
            var itemArray = items.ToArray();
            for (var i = 0; i < itemArray.Length; i++)
            {
                m_Slots[i].SetItem(itemArray[i]);
            }
        }

        public void AddItem(Item item)
        {
            if (item == null)
            {
                throw new ArgumentNullException(nameof(item));
            }

            if (!HasEmptySlots(1))
            {
                throw new Exception("Could not add item to inventory as inventory is full");
            }

            var firstEmptySlot = GetFirstEmptySlot();

            m_Slots[firstEmptySlot].SetItem(item);
            PlayerManager.InventoryUI.UpdateSlots();
        }

        public void RemoveItem(int slotIndex)
        {
            var slot = m_Slots[slotIndex];

            if (slot.Item == null)
            {
                throw new Exception($"Could not empty slot at index {slotIndex} as it was already empty.");
            }

            slot.Empty();
        }

        public void RemoveItem(Type itemType)
        {
            var slotsWithItems = m_Slots.Where(slot => slot.Item != null);
            var matchingSlot = slotsWithItems.FirstOrDefault
                    (slot => slot.Item.GetType() == itemType);

            if (matchingSlot == null)
            {
                throw new Exception($"Could not remove Item {itemType} as it did not exist inventory.");
            }

            matchingSlot.Empty();
        }

        public bool HasEmptySlots(int amountOfSlots)
        {
            return m_Slots.Count(slot => slot.Item == null) >= amountOfSlots;
        }

        public int GetFirstEmptySlot()
        {
            for (var i = 0; i < m_Slots.Length; i++)
            {
                if (m_Slots[i].Item == null)
                {
                    return i;
                }
            }

            Debug.Log("No free slots available.");
            return 0;
        }

        public Item GetItemInSlot(int slotIndex)
        {
            return m_Slots[slotIndex].Item;
        }

        public void UseItem(int slotIndex)
        {
            var item = GetItemInSlot(slotIndex);

            if (item is Consumable consumable)
            {
                consumable.Use();
                RemoveItem(slotIndex);
            }
            else if (item is Equipment)
            {
                PlayerManager.EquipItem(slotIndex);
            }
        }
    }
}
