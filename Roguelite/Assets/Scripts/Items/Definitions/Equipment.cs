using System.Collections.Generic;

namespace Items.Definitions
{
    public abstract class Equipment : Item
    {
        public EquipmentSlotId EquipmentSlotId;
        public IReadOnlyDictionary<Stat, int> StatBonuses;

        protected Equipment(string name, string iconPath, EquipmentSlotId slotId, IReadOnlyDictionary<Stat, int> bonuses) 
            : base(name, iconPath)
        {
            EquipmentSlotId = slotId;
            StatBonuses = bonuses;
        }
    }
}
