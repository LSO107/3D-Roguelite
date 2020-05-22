using ItemData;

namespace Items.Inventory
{
    internal abstract class Consumable : Item
    {
        public string Description { get; }

        public abstract void Use();

        protected Consumable(ConsumableDefinition con)
            : base(con.Name, con.Icon, con.Prefab, con.BaseGoldCost)
        {
            Description = con.description;
        }
    }
}