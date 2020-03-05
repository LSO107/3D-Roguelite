using System.Collections.Generic;
using Items.Definitions;

namespace Player
{
    internal sealed class Stats
    {
        public int CombatLevel { get; private set; }
        public int BaseAttack { get; private set; }
        public int BaseStrength { get; private set; }
        public int BaseDefence { get; private set; }
        public int BaseAgility { get; private set; }

        public Stats(int combatLevel,
                     int baseAttack,
                     int baseStrength,
                     int baseDefence,
                     int baseAgility)
        {
            CombatLevel = combatLevel;
            BaseAttack = baseAttack;
            BaseStrength = baseStrength;
            BaseDefence = baseDefence;
            BaseAgility = baseAgility;
        }

        public Dictionary<Stat, int> GetBaseStats()
        {
            var dictionary = new Dictionary<Stat, int>
            {
                { Stat.Attack, BaseAttack },
                { Stat.Strength, BaseStrength },
                { Stat.Defence, BaseDefence },
                { Stat.Agility, BaseAgility }
            };

            return dictionary;
        }

        public void IncreaseCombatLevel()
        {
            CombatLevel++;
            IncreaseBaseStats();
        }

        private void IncreaseBaseStats()
        {
            BaseAttack += 5;
            BaseStrength += 5;
            BaseDefence += 5;
            BaseAgility += 5;
        }
    }
}
