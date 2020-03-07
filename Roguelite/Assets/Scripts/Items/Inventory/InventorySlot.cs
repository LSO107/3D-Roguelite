using Items.Definitions;

namespace Items.Inventory
{
    internal sealed class InventorySlot
    {
        public Item ItemDefinition { get; private set; }

        public InventorySlot(Item itemDefinition)
        {
            ItemDefinition = itemDefinition;
        }

        public void SetItem(Item itemDefinition)
        {
            ItemDefinition = itemDefinition;
        }

        public void Empty()
        {
            ItemDefinition = null;
        }
    }
}
