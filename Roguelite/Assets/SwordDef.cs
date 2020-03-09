using System.Collections.Generic;
using Items.Definitions;

public class SwordDef : Equipment
{
    private const string IconPath = "ItemIcons/EquipmentItems/Daggers/Rare";
    private const string ItemName = "Rare Dagger";

    private const EquipmentSlotId SlotId = EquipmentSlotId.Weapon;
    private static readonly IReadOnlyDictionary<Stat, int> Bonuses = new Dictionary<Stat, int>
    {
        { Stat.Attack, 20 },
        { Stat.Strength, 25 },
        { Stat.Defence, 0 },
        { Stat.Agility, 1 }
    };

    public SwordDef()
        : base(ItemName, IconPath, SlotId, Bonuses)
    {
    }
}
