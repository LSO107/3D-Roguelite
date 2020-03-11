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

        public void NewItem(ItemDefinition item)
        {
            ItemDefinitions.Add(item);
        }

        public ItemDefinition GetItem(string itemId)
        {
            return ItemDefinitions.FirstOrDefault(i => i.Id == itemId);
        }
    }
}
