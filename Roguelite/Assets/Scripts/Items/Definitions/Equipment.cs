using System.Collections.Generic;

namespace Items.Definitions
{
    public abstract class Equipment
    {
        public EquipmentSlotId EquipmentSlotId;
        public IReadOnlyDictionary<Stat, int> StatBonuses;

        protected Equipment(EquipmentSlotId slotId, IReadOnlyDictionary<Stat, int> bonuses)
        {
            EquipmentSlotId = slotId;
            StatBonuses = bonuses;
        }
    }
}
