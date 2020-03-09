namespace Items.Definitions
{
    public abstract class Consumable : Item
    {
        public string Description;
        public int Value;

        protected Consumable(string name, string iconPath, string description, int value) 
            : base(name, iconPath)
        {
            Description = description;
            Value = value;
        }

        public abstract void Use();
    }
}
