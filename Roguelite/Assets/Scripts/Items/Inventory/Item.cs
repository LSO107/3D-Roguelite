using System;
using UnityEngine;

namespace Items.Inventory
{
    [Serializable]
    internal class Item
    {
        public string Id { get; }
        public string Name { get; }
        public Sprite Icon { get; }
        public GameObject Prefab { get; }
        public int GoldCost { get; }

        public Item(string name, Sprite icon, GameObject prefab, int goldCost)
        {
            Id = Guid.NewGuid().ToString();
            Name = name;
            Icon = icon;
            Prefab = prefab;
            GoldCost = goldCost;
        }
    }
}
