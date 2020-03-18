using System;
using Items.Definitions;
using Random = System.Random;

namespace ItemData
{
    internal sealed class ItemGenerator 
    {
        private EquipmentItem m_ItemContainer;
        private readonly Random m_Random;

        public ItemGenerator()
        {
            m_Random = new Random();
        }

        public ConsumableItem GeneratePotion()
        {
            var itemDatabase = GameManager.Instance.ItemDatabase;
            var consumableContainer = itemDatabase.HealthPotions.GetRandomItem<ConsumableItem>();
            var potion = consumableContainer.CreateInstance();
            itemDatabase.HealthPotions.AddItem(potion);
            return potion;
        }

        public EquipmentItem GenerateEquipmentItem()
        {
            var itemDatabase = GameManager.Instance.ItemDatabase;
            m_ItemContainer =  itemDatabase.Daggers.GetRandomItem<EquipmentItem>();
            var item = m_ItemContainer.CreateInstance();
            itemDatabase.Daggers.AddItem(item);
            return GenerateRandomBonuses(item);
        }

        private EquipmentItem GenerateRandomBonuses(EquipmentItem item)
        {
            var playerLevel = GameManager.Instance.PlayerManager.Stats.CombatLevel;

            var maximumValue = playerLevel * 10;

            foreach (var stat in Enum.GetValues(typeof(Stat)))
            {
                item.StatBonuses[(Stat)stat] += m_Random.Next(maximumValue);
            }
            return item;
        }
    }
}
