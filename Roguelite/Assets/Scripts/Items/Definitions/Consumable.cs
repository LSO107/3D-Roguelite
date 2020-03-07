namespace Items.Definitions
{
    public abstract class Consumable : Item
    {
        protected Consumable(string name, string iconPath) 
            : base(name, iconPath)
        {
        }

        public abstract void Use();
    }
}
