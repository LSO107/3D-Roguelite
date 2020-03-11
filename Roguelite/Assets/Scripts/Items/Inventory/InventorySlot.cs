﻿using ItemData;
using Items.Definitions;

namespace Items.Inventory
{
    internal sealed class InventorySlot
    {
        public ItemDefinition ItemDefinition { get; private set; }

        public InventorySlot(ItemDefinition itemDefinition)
        {
            ItemDefinition = itemDefinition;
        }

        public void SetItem(ItemDefinition itemDefinition)
        {
            ItemDefinition = itemDefinition;
        }

        public void Empty()
        {
            ItemDefinition = null;
        }
    }
}
