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
        public GameObject Prefab;
        public EquipmentSlotId EquipmentSlotId;
        public Dictionary<Stat, int> StatBonuses;

        [SerializeField] private int m_Attack;
        [SerializeField] private int m_Strength;
        [SerializeField] private int m_Defence;
        [SerializeField] private int m_Agility;

        public void OnEnable()
        {
            StatBonuses = new Dictionary<Stat, int>
            {
                { Stat.Attack, m_Attack },
                { Stat.Strength, m_Strength },
                { Stat.Defence, m_Defence },
                { Stat.Agility, m_Agility }
            };
        }
    }

    [CreateAssetMenu(fileName = "Consumable", menuName = "Item/Consumable")]
    internal abstract class ConsumableItem : ItemDefinition
    {
        public string description;
        public float value;

        public abstract void Use();
    }
}
