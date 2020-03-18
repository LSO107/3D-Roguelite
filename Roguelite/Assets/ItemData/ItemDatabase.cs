using System;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using Random = System.Random;

namespace ItemData
{
    internal sealed class ItemDatabase
    {
        private readonly List<ItemDefinition> m_ItemDatabase;

        private readonly Random m_Random;

        public ItemDatabase(IEnumerable<ItemDefinition> itemData)
        {
            m_ItemDatabase = new List<ItemDefinition>(itemData);
            m_Random = new Random();
        }

        public void AddItem(ItemDefinition itemDefinition)
        {
            var item = m_ItemDatabase.FirstOrDefault(i => i.Id == itemDefinition.Id);

            if (item != null)
                throw new ArgumentException("Item database contains item with same Id");

            m_ItemDatabase.Add(itemDefinition);
        }

        public void RemoveItem(string itemId)
        {
            var item = m_ItemDatabase.FirstOrDefault(i => i.Id == itemId);

            if (item == null)
            {
                Debug.Log("Item was not in the database");
                return;
            }
            m_ItemDatabase.Remove(item);
        }

        public ItemDefinition FindItem(string itemId)
        {
            return m_ItemDatabase.FirstOrDefault(i => i.Id == itemId);
        }

        public ItemDefinition FindItem(ItemDefinition item)
        {
            return m_ItemDatabase.FirstOrDefault(i => i == item);
        }

        public T GetRandomItem<T>() where T : ItemDefinition
        {
            var number = m_Random.Next(m_ItemDatabase.Count);
            return (T) m_ItemDatabase[number];
        }
    }
}
