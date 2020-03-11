using System;
using Items.Definitions;
using UnityEngine;
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

        public EquipmentItem GenerateItem()
        {
            m_ItemContainer = Resources.Load<EquipmentItem>("ItemSO/Dagger");
            var item = m_ItemContainer.CreateInstance();
            GameManager.Instance.ItemDatabase.AddItem(item);
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
