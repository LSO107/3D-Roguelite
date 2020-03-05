using System;
using System.Collections.Generic;
using Items.Definitions;
using UI.EquipmentUI;

namespace Items.EquipmentSystem
{
    internal sealed class PlayerEquipment
    {
        private readonly Dictionary<EquipmentSlotId, Equipment> m_EquipmentSlots;
        private EquipmentUI m_EquipmentUI;

        public PlayerEquipment()
        {
            m_EquipmentUI = GameManager.Instance.PlayerManager.EquipmentUI;

            m_EquipmentSlots = new Dictionary<EquipmentSlotId, Equipment>
            {
                { EquipmentSlotId.Head, null },
                { EquipmentSlotId.Torso, null },
                { EquipmentSlotId.Legs, null },
                { EquipmentSlotId.Weapon, null },
            };
        }

        public void Equip(Equipment item)
        {
            m_EquipmentSlots[item.EquipmentSlotId] = item;
            m_EquipmentUI.UpdateLabels();
        }

        public void Unequip(EquipmentSlotId slotId)
        {
            m_EquipmentSlots[slotId] = null;
            m_EquipmentUI.UpdateLabels();
        }

        public Equipment GetEquipmentInSlot(EquipmentSlotId slotId)
        {
            return m_EquipmentSlots[slotId];
        }

        /// <summary>
        /// Adds all the item bonuses of the equipped items
        /// into a new dictionary
        /// </summary>
        public Dictionary<Stat, int> GetEquipmentStatBonuses()
        {
            var dictionary = new Dictionary<Stat, int>();
            foreach (var value in Enum.GetValues(typeof(Stat)))
            {
                dictionary.Add((Stat)value, 0);
            }
            foreach (var equipmentSlot in m_EquipmentSlots)
            {
                var itemDictionary = equipmentSlot.Value?.StatBonuses;

                if (itemDictionary == null)
                    continue;

                foreach (var entry in itemDictionary)
                {
                    if (dictionary.ContainsKey(entry.Key))
                    {
                        dictionary[entry.Key] += entry.Value;
                    }
                    else
                    {
                        dictionary.Add(entry.Key, entry.Value);
                    }
                }
            }

            return dictionary;
        }
    }
}
