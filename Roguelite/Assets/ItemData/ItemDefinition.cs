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
        public GameObject Prefab;

        protected void GenerateUniqueId()
        {
            Id = Guid.NewGuid().ToString();
        }
    }

    [CreateAssetMenu(fileName = "New Equipment", menuName = "Item Manager/Equipment")]
    internal sealed class EquipmentItem : ItemDefinition
    {
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

    internal abstract class ConsumableItem : ItemDefinition
    {
        public string description;

        public abstract void Use();
    }

    [CreateAssetMenu(fileName = "Health Potion", menuName = "Item Manager/Consumable/Potions/Health")]
    internal sealed class Health : ConsumableItem
    {
        public int value;

        public void OnEnable()
        {
            GenerateUniqueId();
        }

        public ConsumableItem CreateInstance()
        {
            var item = Instantiate(this);
            return item;
        }

        public override void Use()
        {
            var playerHealth = GameManager.Instance.PlayerManager.Health;
            playerHealth.Heal(value);
        }
    }
}
