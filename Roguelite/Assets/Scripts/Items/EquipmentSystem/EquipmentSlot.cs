using Items.Definitions;

namespace Items.EquipmentSystem
{
    internal sealed class EquipmentSlot
    {
        public EquipmentSlotId SlotId;

        public Equipment Equipment;

        public EquipmentSlot(EquipmentSlotId slotId, Equipment equipment)
        {
            SlotId = slotId;
            Equipment = equipment;
        }
    }
}
