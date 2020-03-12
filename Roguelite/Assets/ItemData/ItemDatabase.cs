using System.Collections.Generic;
using System.Linq;
using UnityEngine;

namespace ItemData
{
    internal sealed class ItemDatabase : MonoBehaviour
    {
        private List<ItemDefinition> m_GroundItems = new List<ItemDefinition>();
        public List<ItemDefinition> ScriptableObjects = new List<ItemDefinition>();

        public ItemDefinition FindItemTemplate(string itemName)
        {
            return ScriptableObjects.Find(i => i.Name == itemName);
        }

        public void AddItem(ItemDefinition item)
        {
            if (m_GroundItems.Contains(item))
            {
                Debug.Log($"{item.Name} already existed in database");
                return;
            }

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

        public ItemDefinition FindItem(string itemId)
        {
            return m_GroundItems.FirstOrDefault(i => i.Id == itemId);
        }
    }
}
