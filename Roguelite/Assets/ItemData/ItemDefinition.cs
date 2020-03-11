using System;
using System.Collections.Generic;
using Items.Definitions;
using UnityEngine;

namespace ItemData
{
    internal class ItemDefinition : ScriptableObject
    {
        public string Id;
        public string Name;
        public Sprite Icon;

        protected void GenerateUniqueId()
        {
            Id = Guid.NewGuid().ToString();
        }
    }

    [CreateAssetMenu(fileName = "New Equipment", menuName = "Item Manager/Equipment")]
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
            GenerateUniqueId();

            StatBonuses = new Dictionary<Stat, int>
            {
                { Stat.Attack, m_Attack },
                { Stat.Strength, m_Strength },
                { Stat.Defence, m_Defence },
                { Stat.Agility, m_Agility }
            };
        }

        public EquipmentItem CreateInstance()
        {
            var item = Instantiate(this);
            return item;
        }
    }

    [CreateAssetMenu(fileName = "New Consumable", menuName = "Item Manager/Consumable")]
    internal abstract class ConsumableItem : ItemDefinition
    {
        public string description;
        public float value;

        public abstract void Use();
    }
}
