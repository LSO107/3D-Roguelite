using System;
using System.Collections.Generic;
using Items.Definitions;
using Player;
using UI.EquipmentUI;

namespace Items.Inventory
{
    internal sealed class PlayerEquipment
    {
        private readonly Dictionary<EquipmentSlotId, Equipment> m_EquipmentSlots;
        private readonly EquipmentUI m_EquipmentUI;
        private readonly PlayerStats m_PlayerStats;

        public PlayerEquipment()
        {
            m_EquipmentUI = PlayerManager.Instance.EquipmentUI;
            m_PlayerStats = PlayerManager.Instance.PlayerStats;

            m_EquipmentSlots = new Dictionary<EquipmentSlotId, Equipment>
            {
                { EquipmentSlotId.Head, null },
                { EquipmentSlotId.Torso, null },
                { EquipmentSlotId.Legs, null },
                { EquipmentSlotId.Weapon, null },
            };
        }

        public void Equip(Equipment definition)
        {
            m_EquipmentSlots[definition.EquipmentSlotId] = definition;
            m_EquipmentUI.UpdateLabels();
            m_PlayerStats.UpdateStats();
        }

        public void Unequip(EquipmentSlotId slotId)
        {
            m_EquipmentSlots[slotId] = null;
            m_EquipmentUI.UpdateLabels();
            m_PlayerStats.UpdateStats();
        }

        /// <summary>
        /// Returns the item in the slotId or null
        /// </summary>
        public Equipment GetEquipmentInSlot(EquipmentSlotId slotId)
        {
            return m_EquipmentSlots[slotId];
        }

        /// <summary>
        /// Returns the item bonus for each <see cref="StatBonus"/>
        /// </summary>
        public Dictionary<StatBonus, int> GetEquipmentStatBonuses()
        {
            var dictionary = new Dictionary<StatBonus, int>();
            foreach (var value in Enum.GetValues(typeof(StatBonus)))
            {
                dictionary.Add((StatBonus)value, 0);
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
