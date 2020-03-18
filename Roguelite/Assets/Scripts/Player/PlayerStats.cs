using System;
using System.Collections.Generic;
using Character.Combat;
using Items.Definitions;

namespace Player
{
    internal sealed class PlayerStats
    {
        public int CombatLevel { get; private set; }
        public Stat Damage { get; private set; }
        public Stat Defence { get; private set; }

        private Dictionary<StatBonus, int> m_BaseStats;

        public PlayerStats(int combatLevel)
        {
            m_BaseStats = new Dictionary<StatBonus, int>
            {
                { StatBonus.Attack, combatLevel * 1 },
                { StatBonus.Strength, combatLevel * 1 },
                { StatBonus.Defence, combatLevel * 1 },
                { StatBonus.Agility, combatLevel * 1 }
            };
        }

        public Dictionary<StatBonus, int> GetBaseStats()
        {
            return m_BaseStats;
        }

        public void IncreaseCombatLevel()
        {
            CombatLevel++;
            IncreaseBaseStats();
            var equipment = GameManager.Instance.PlayerManager.EquipmentUI;
            equipment.UpdateCombatLevelLabel();
            equipment.UpdateLabels();
        }

        public void UpdateStats()
        {

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
