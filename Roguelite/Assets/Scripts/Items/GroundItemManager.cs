using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography;
using ItemData;
using Items.Inventory;
using UnityEngine;

namespace Items
{
    internal sealed class GroundItemManager : MonoBehaviour
    {
        [SerializeField] private List<Item> m_GroundItems;

        public static GroundItemManager Instance;

        private void Awake()
        {
            if (Instance != null && Instance != this)
            {
                Debug.LogError("Multiple ground item managers in scene!");
                Destroy(this);
                return;
            }
            Instance = this;
            m_GroundItems = new List<Item>();
        }

        public void AddItem(Item item)
        {
            var existingItem = m_GroundItems.FirstOrDefault(i => i.Id == item.Id);

            if (existingItem != null)
                throw new ArgumentException("Item database contains item with same Id");

            m_GroundItems.Add(item);
        }

        public void RemoveItem(string itemId)
        {
            var item = m_GroundItems.FirstOrDefault(i => i.Id == itemId);

            if (item == null)
            {
                Debug.Log("Item was not in the database");
                return;
            }
            m_GroundItems.Remove(item);
        }

        public Item FindItem(string itemId)
        {
            return m_GroundItems.FirstOrDefault(i => i.Id == itemId);
        }
    }
}
