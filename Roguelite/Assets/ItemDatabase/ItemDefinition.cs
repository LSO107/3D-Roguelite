using System;
using System.Collections.Generic;
using Items.Definitions;
using Player;
using UnityEngine;
using Random = System.Random;

namespace ItemDatabase
{
    internal class ItemDefinition : ScriptableObject
    {
        public string itemName;
        public Sprite itemIcon;
    }

    [CreateAssetMenu(fileName = "Equipment", menuName = "Item/Equipment")]
    internal sealed class EquipmentItem : ItemDefinition
    {
        public EquipmentSlotId equipmentSlotId;
        public Dictionary<Stat, int> bonuses;

        [SerializeField] private int m_Attack;
        [SerializeField] private int m_Strength;
        [SerializeField] private int m_Defence;
        [SerializeField] private int m_Agility;

        public void OnEnable()
        {
            bonuses = new Dictionary<Stat, int>
            {
                { Stat.Attack, m_Attack },
                { Stat.Strength, m_Strength },
                { Stat.Defence, m_Defence },
                { Stat.Agility, m_Agility }
            };
        }
    }

    [CreateAssetMenu(fileName = "Consumable", menuName = "Item/Consumable")]
    internal sealed class ConsumableItem : ItemDefinition
    {
        public string description;
        public float value;
    }
}
