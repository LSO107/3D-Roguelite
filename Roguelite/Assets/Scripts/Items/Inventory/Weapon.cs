using ItemData;

namespace Items.Inventory
{
    internal sealed class Weapon : Equipment
    {
        public WeaponType WeaponType { get; }

        public Weapon(WeaponDefinition definition, RarityModifier rarity)
            : base(definition, rarity)
        {
            WeaponType = definition.WeaponType;
        }
    }
}
