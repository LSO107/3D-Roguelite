using System.Collections.Generic;
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
            StatBonuses = eq.StatBonuses;
            Attack = eq.m_Attack;
            Defence = eq.m_Defence;
            Strength = eq.m_Strength;
            Agility = eq.m_Agility;
        }

        public Equipment(string name,
                         Sprite icon,
                         GameObject prefab,
                         int goldCost,
                         EquipmentSlotId slotId, 
                         Dictionary<StatBonus, int> statBonuses,
                         int attack,
                         int strength,
                         int defence,
                         int agility)
            : base(name, icon, prefab, goldCost)
        {
            EquipmentSlotId = slotId;
            StatBonuses = statBonuses;
            Attack = attack;
            Strength = strength;
            Defence = defence;
            Agility = agility;
        }
    }
}
