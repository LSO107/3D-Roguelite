using System.Collections.Generic;
using Items.Definitions;

namespace Items.Equipables
{
    internal sealed class SteelDagger : Equipment
    {
        private const string IconPath = "ItemIcons/EquipmentItems/Daggers/Common";
        private const string ItemName = "Common Dagger";

        private const EquipmentSlotId SlotId = EquipmentSlotId.Weapon;
        private static readonly IReadOnlyDictionary<Stat, int> Bonuses = new Dictionary<Stat, int>
        {
            { Stat.Attack, 5 },
            { Stat.Strength, 5 },
            { Stat.Defence, 0 }
        };

        public SteelDagger()
            : base(ItemName, IconPath, SlotId, Bonuses)
        {
        }
    }
}
