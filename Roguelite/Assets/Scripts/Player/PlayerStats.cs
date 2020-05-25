using System;
using System.Collections.Generic;
using Character.Combat;
using Character.Health;
using Items.Definitions;
using UnityEngine;
using UnityEngine.SceneManagement;

namespace Player
{
    internal sealed class PlayerStats : MonoBehaviour
    {
        public int CombatLevel { get; private set; }
        public Stat Damage { get; private set; }
        public Stat Defence { get; private set; }

        private Dictionary<StatBonus, int> m_BaseStats;

        private Dictionary<StatBonus, int> m_EquipmentStats;

        private HealthObject m_HealthObject;

        private System.Random m_Random;

        [SerializeField] private SplatMarker m_SplatMarker = null;

        public void Initialize(int combatLevel)
        {
            m_Random = new System.Random();
            m_HealthObject = GetComponent<HealthObject>();

            CombatLevel = combatLevel;

            m_BaseStats = new Dictionary<StatBonus, int>
            {
                { StatBonus.Attack, combatLevel * 1 },
                { StatBonus.Strength, combatLevel * 1 },
                { StatBonus.Defence, combatLevel * 1 },
                { StatBonus.Agility, combatLevel * 1 }
            };

            Damage = new Stat();
            Defence = new Stat();

            Damage.SetBaseValue(10);
            Defence.SetBaseValue(10);

            UpdateStats();
        }

        public void TakeDamage(int damage)
        {
            var randomDamage = m_Random.Next(damage);
            var blockedDamage = m_Random.Next(Defence.GetBaseValue());
            var finalDamage = randomDamage - blockedDamage;

            if (finalDamage < 0)
                finalDamage = 0;

            m_HealthObject.Damage(finalDamage);
            m_SplatMarker.Show(finalDamage);
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
            var equipment = PlayerManager.Instance.EquipmentUI;
            equipment.UpdateCombatLevelLabel();
            equipment.UpdateLabels();
        }

        public void UpdateStats()
        {
            m_EquipmentStats = PlayerManager.Instance.Equipment.GetEquipmentStatBonuses();

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
