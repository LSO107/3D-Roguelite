using System.Collections.Generic;
using System.Linq;
using UnityEngine;

namespace ItemData
{
    internal sealed class ItemDatabase : MonoBehaviour
    {
        public List<ItemDefinition> ItemDefinitions;

        public void Awake()
        {
            ItemDefinitions = new List<ItemDefinition>();
        }

        public void AddItem(ItemDefinition item)
        {
            if (ItemDefinitions.Contains(item))
            {
                Debug.Log($"{item.Name} already existed in database");
                return;
            }

            ItemDefinitions.Add(item);
        }

        public ItemDefinition FindItem(string itemId)
        {
            return ItemDefinitions.FirstOrDefault(i => i.Id == itemId);
        }
    }
}
