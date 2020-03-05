using Items.Definitions;

namespace Items.Inventory
{
    internal sealed class Slot
    {
        public Item ItemDefinition { get; private set; }

        public Slot(Item itemDefinition)
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
