using System.Collections.Generic;
using System.Linq;
using ItemData;
using Items.Definitions;
using UnityEngine;

namespace Items.Inventory
{
    internal class Equipment : Item
    {
        public EquipmentSlotId EquipmentSlotId { get; }
        public Dictionary<StatBonus, int> StatBonuses { get; }

        public int Attack { get; }
        public int Strength { get; }
        public int Defence { get; }
        public int Agility { get; }

        public Equipment(EquipmentDefinition eq)
            : base(eq.Name, eq.Icon, eq.Prefab, eq.BaseGoldCost)
        {
            EquipmentSlotId = eq.EquipmentSlotId;
            StatBonuses = eq.StatBonuses.ToDictionary(m => m.Key, m => m.Value);
            Attack = eq.m_Attack;
            Defence = eq.m_Defence;
            Strength = eq.m_Strength;
            Agility = eq.m_Agility;
        }
    }
}
