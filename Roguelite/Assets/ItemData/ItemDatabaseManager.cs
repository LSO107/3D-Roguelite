using System.Collections.Generic;
using UnityEngine;

namespace ItemData
{
    internal sealed class ItemDatabaseManager : MonoBehaviour
    {
        [Header("Weapons")]
        [SerializeField] private List<EquipmentItem> m_DaggersSO = new List<EquipmentItem>();
        [SerializeField] private List<EquipmentItem> m_SpearsSO = new List<EquipmentItem>();
        [SerializeField] private List<EquipmentItem> m_SwordsSO = new List<EquipmentItem>();

        [Header("Consumables")]
        [SerializeField] private List<ConsumableItem> m_HealthPotionsSO = new List<ConsumableItem>();

        public ItemDatabase GroundItems { get; private set; }
        public ItemDatabase Daggers { get; private set; }
        public ItemDatabase Spears { get; private set; }
        public ItemDatabase Swords { get; private set; }
        public ItemDatabase HealthPotions { get; private set; }

        private void Start()
        {
            GroundItems = new ItemDatabase(new List<ItemDefinition>());
            Daggers = new ItemDatabase(m_DaggersSO);
            Spears = new ItemDatabase(m_SpearsSO);
            Swords = new ItemDatabase(m_SwordsSO);
            HealthPotions = new ItemDatabase(m_HealthPotionsSO);
        }
    }
}
