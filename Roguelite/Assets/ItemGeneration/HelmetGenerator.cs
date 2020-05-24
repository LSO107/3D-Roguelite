using System;
using System.Collections.Generic;
using System.Linq;
using ItemData;
using Items.Definitions;
using Items.Inventory;
using Logging;
using Player;
using UnityEngine;
using Random = System.Random;

namespace ItemGeneration
{
    internal sealed class HelmetGenerator : IItemGenerator
    {
        private readonly Random m_Random;
        private readonly ILog m_Log;

        public HelmetGenerator(Random random, ILog log)
        {
            m_Random = random;
            m_Log = log;
        }

        public Equipment GenerateBonuses(EquipmentDefinition definition, RarityModifier rarity)
        {
            m_Log.Log($"I am generating a {definition.Name} item with rarity {rarity}", LogLevel.Info);
            m_Log.Log($"Base stats are {string.Join(", ", definition.StatBonuses.Select(m => $"{m.Key}: {m.Value}"))}", LogLevel.Info);

            if (definition.EquipmentSlotId != EquipmentSlotId.Head)
            {
                throw new ArgumentException("Equipment must be of type helmet, was " + definition.EquipmentSlotId);
            }

            var minBonus = MinimumFlatBonus[rarity];
            var maxBonus = Mathf.RoundToInt(GetMaxBonus(rarity));

            m_Log.Log($"Min bonus is {minBonus}", LogLevel.Info);
            m_Log.Log($"Max bonus is {maxBonus}", LogLevel.Info);

            return GenerateEquipmentWithBonuses(definition, rarity, minBonus, maxBonus);
        }

        private static float GetMaxBonus(RarityModifier rarity)
        {
            var levelBonus = PlayerManager.Instance.PlayerStats.CombatLevel;
            var rarityBonus = (levelBonus + MaximumFlatBonus[rarity]) * PercentageBonus[rarity];

            return levelBonus + rarityBonus;
        }

        private Equipment GenerateEquipmentWithBonuses(EquipmentDefinition def, RarityModifier rarity, int minBonus, int maxBonus)
        {
            var statsToBoost = def.GetStatBonuses().ToList();

            var equipment = new Equipment(def, rarity);

            foreach (var stat in statsToBoost)
            {
                var rand = m_Random.Next(minBonus, maxBonus + 1);

                m_Log.Log($"Applying a bonus of {rand} to stat {stat}", LogLevel.Info);

                equipment.StatBonuses[stat] += rand;
            }

            m_Log.Log($"Final stats: {string.Join(", ", equipment.StatBonuses.Select(m => $"{m.Key}: {m.Value}"))}\n", LogLevel.Info);

            return equipment;
        }

        private static readonly IReadOnlyDictionary<RarityModifier, int> MinimumFlatBonus
            = new Dictionary<RarityModifier, int>
        {
            { RarityModifier.Common, 0 },
            { RarityModifier.Rare, 2 },
            { RarityModifier.Epic, 4 }
        };

        private static readonly IReadOnlyDictionary<RarityModifier, int> MaximumFlatBonus 
            = new Dictionary<RarityModifier, int>
            {
                { RarityModifier.Common, 1 },
                { RarityModifier.Rare, 3 },
                { RarityModifier.Epic, 5 },
            };

        private static readonly IReadOnlyDictionary<RarityModifier, float> PercentageBonus
            = new Dictionary<RarityModifier, float>
            {
                { RarityModifier.Common, 1.0f },
                { RarityModifier.Rare, 1.2f },
                { RarityModifier.Epic, 1.4f },
            };
    }
}
