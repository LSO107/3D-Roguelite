using ItemData;

namespace Items.Inventory
{
    internal class Consumable : Item
    {
        public string Description { get; }

        public virtual void Use()
        {

        }

        public Consumable(ConsumableDefinition con)
            : base(con.Name, con.Icon, con.Prefab, con.BaseGoldCost)
        {
            Description = con.description;
        }
    }
}