using System;
using System.Collections.Generic;
using Items.Definitions;
using UnityEngine;

namespace ItemData
{
    public class ItemDefinition : ScriptableObject
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

    [CreateAssetMenu(fileName = "New Weapon", menuName = "Item Manager/Weapon")]
    internal sealed class WeaponDefinition : EquipmentItem
    {
        public WeaponType WeaponType;
    }

    internal enum WeaponType
    {
        Dagger,
        Sword,
        Spear,
        PepperSpray
    }

    [CreateAssetMenu(fileName = "New Equipment", menuName = "Item Manager/Equipment")]
    internal class EquipmentItem : ItemDefinition
    {
        public EquipmentSlotId EquipmentSlotId;
        public Dictionary<StatBonus, int> StatBonuses;

        [SerializeField] private int m_Attack;
        [SerializeField] private int m_Strength;
        [SerializeField] private int m_Defence;
        [SerializeField] private int m_Agility;

        public void OnEnable()
        {
            GenerateUniqueId();

            StatBonuses = new Dictionary<StatBonus, int>
            {
                { StatBonus.Attack, m_Attack },
                { StatBonus.Strength, m_Strength },
                { StatBonus.Defence, m_Defence },
                { StatBonus.Agility, m_Agility }
            };
        }

        public EquipmentItem CreateInstance()
        {
            var item = Instantiate(this);
            return item;
        }
    }

    internal class ConsumableItem : ItemDefinition
    {
        public string description;

        public virtual void Use()
        {
            throw new NotImplementedException("Must override Use method");
        }

        public ConsumableItem CreateInstance()
        {
            var item = Instantiate(this);
            return item;
        }
    }

    [CreateAssetMenu(fileName = "Health Potion", menuName = "Item Manager/Consumable/Potions/Health")]
    internal sealed class Potion : ConsumableItem
    {
        public int value;

        public void OnEnable()
        {
            GenerateUniqueId();
        }

        public override void Use()
        {
            var playerHealth = GameManager.Instance.PlayerManager.Health;
            playerHealth.Heal(value);
        }
    }
}
