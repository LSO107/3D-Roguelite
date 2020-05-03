using System;
using System.Collections.Generic;
using Items.Inventory;
using UnityEngine;

using Random = System.Random;

namespace ItemData
{
    internal sealed class ItemDatabaseManager : MonoBehaviour
    {
        [Header("Weapons (Scriptable Objects)")]
        [SerializeField] private List<EquipmentDefinition> m_Daggers = new List<EquipmentDefinition>();
        [SerializeField] private List<EquipmentDefinition> m_Scimitars = new List<EquipmentDefinition>();
        [SerializeField] private List<EquipmentDefinition> m_Swords = new List<EquipmentDefinition>();
        [SerializeField] private List<EquipmentDefinition> m_Bats = new List<EquipmentDefinition>();

        [Header("Consumables (Scriptable Objects)")]
        [SerializeField] private List<ConsumableDefinition> m_HealthPotions = new List<ConsumableDefinition>();

        private Random m_Random;

        private void Start()
        {
            m_Random = new Random();
        }

        public ConsumableDefinition GetRandomHealthPotion()
        {
            var number = m_Random.Next(m_HealthPotions.Count);
            return m_HealthPotions[number];
        }

        public EquipmentDefinition GetRandomWeapon()
        {
            var databases = new[]
            {
                m_Daggers,
                m_Swords,
                m_Bats,
                m_Scimitars
            };

            var database = databases[m_Random.Next(databases.Length)];
            return database[m_Random.Next(database.Count)];
        }
    }
}
