using ItemData;

namespace Items.Inventory
{
    internal sealed class Weapon : Equipment
    {
        public WeaponType WeaponType { get; }

        public Weapon(WeaponDefinition definition)
            : base(definition)
        {
            WeaponType = definition.WeaponType;
        }
    }
}
