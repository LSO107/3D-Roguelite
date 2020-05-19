namespace Items.Inventory
{
    internal sealed class InventorySlot
    {
        public Item Item { get; private set; }

        public InventorySlot(Item item)
        {
            Item = item;
        }

        public void SetItem(Item item)
        {
            Item = item;
        }

        public void Empty()
        {
            Item = null;
        }
    }
}
