using System;
using System.Collections.Generic;
using Character.Combat;
using Items.Definitions;
using UnityEngine;

namespace Player
{
    internal sealed class PlayerStats
    {
        public int CombatLevel { get; private set; }
        public Stat Damage { get; private set; }
        public Stat Defence { get; private set; }

        private Dictionary<StatBonus, int> m_BaseStats;

        private Dictionary<StatBonus, int> m_EquipmentStats;

        public void Initialize(int combatLevel)
        {
            CombatLevel = combatLevel;
            m_EquipmentStats = GameManager.Instance.PlayerManager.Equipment.GetEquipmentStatBonuses();

            m_BaseStats = new Dictionary<StatBonus, int>
            {
                { StatBonus.Attack, combatLevel * 1 },
                { StatBonus.Strength, combatLevel * 1 },
                { StatBonus.Defence, combatLevel * 1 },
                { StatBonus.Agility, combatLevel * 1 }
            };

            Damage = new Stat();
            Defence = new Stat();
            UpdateStats();
        }

        public Dictionary<StatBonus, int> GetBaseStats()
        {
            return m_BaseStats;
        }

        public void IncreaseCombatLevel()
        {
            CombatLevel++;
            IncreaseBaseStats();
            UpdateStats();
            var equipment = GameManager.Instance.PlayerManager.EquipmentUI;
            equipment.UpdateCombatLevelLabel();
            equipment.UpdateLabels();
        }

        public void UpdateStats()
        {
            var attack = m_BaseStats[StatBonus.Attack] + m_EquipmentStats[StatBonus.Attack];
            var strength = m_BaseStats[StatBonus.Strength] + m_EquipmentStats[StatBonus.Strength];

            var defence = m_BaseStats[StatBonus.Defence] + m_EquipmentStats[StatBonus.Defence];
            var agility = m_BaseStats[StatBonus.Agility] + m_EquipmentStats[StatBonus.Agility];

            Damage.SetBaseValue(attack + strength);
            Defence.SetBaseValue(defence + agility);

            Debug.Log($"Damage: {Damage.GetBaseValue()} Defence: {Defence.GetBaseValue()}");
        }

        private void IncreaseBaseStats()
        {
            foreach (var stat in Enum.GetValues(typeof(StatBonus)))
            {
                m_BaseStats[(StatBonus) stat] = CombatLevel * 5;
            }
        }
    }
}
