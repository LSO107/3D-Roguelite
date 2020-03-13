using System.Collections.Generic;
using UnityEngine;

namespace ItemData
{
    internal sealed class ItemDatabaseManager : MonoBehaviour
    {
        [Header("Weapons (Scriptable Objects)")]
        [SerializeField] private List<EquipmentItem> m_Daggers = new List<EquipmentItem>();
        [SerializeField] private List<EquipmentItem> m_Spears = new List<EquipmentItem>();
        [SerializeField] private List<EquipmentItem> m_Swords = new List<EquipmentItem>();

        [Header("Consumables (Scriptable Objects)")]
        [SerializeField] private List<ConsumableItem> m_HealthPotions = new List<ConsumableItem>();

        public ItemDatabase GroundItems { get; private set; }
        public ItemDatabase Daggers { get; private set; }
        public ItemDatabase Spears { get; private set; }
        public ItemDatabase Swords { get; private set; }
        public ItemDatabase HealthPotions { get; private set; }

        private void Start()
        {
            GroundItems = new ItemDatabase(new List<ItemDefinition>());
            Daggers = new ItemDatabase(m_Daggers);
            Spears = new ItemDatabase(m_Spears);
            Swords = new ItemDatabase(m_Swords);
            HealthPotions = new ItemDatabase(m_HealthPotions);
        }
    }
}
