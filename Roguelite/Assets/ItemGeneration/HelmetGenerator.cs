using System;
using System.Linq;
using ItemData;
using Items.Definitions;
using Items.Inventory;
using Logging;
using UnityEngine;
using Random = System.Random;

namespace ItemGeneration
{
    internal sealed class HelmetGenerator : IItemGenerator
    {
        private Random m_Random;
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

            var maxBonus = GameManager.Instance.PlayerManager.PlayerStats.CombatLevel + GetFlatRarityBonus(rarity) * GetPercentageRarityBonus(rarity);

            m_Log.Log($"Max bonus is {maxBonus}", LogLevel.Info);

            return GenerateEquipmentWithBonuses(definition, Mathf.RoundToInt(maxBonus));
        }

        private static float GetPercentageRarityBonus(RarityModifier rarity)
        {
            switch (rarity)
            {
                case RarityModifier.Common:
                    return 1.0f;
                case RarityModifier.Rare:
                    return 1.2f;
                case RarityModifier.Epic:
                    return 1.4f;
                default:
                    throw new ArgumentException("Rarity was wrong");
            }
        }

        private static int GetFlatRarityBonus(RarityModifier rarity)
        {
            switch (rarity)
            {
                case RarityModifier.Common:
                    return 1;
                case RarityModifier.Rare:
                    return 3;
                case RarityModifier.Epic:
                    return 5;
                default:
                    throw new ArgumentException("Rarity was wrong");
            }
        }

        private Equipment GenerateEquipmentWithBonuses(EquipmentDefinition def, int maxBonus)
        {
            var statsToBoost = def.GetStatBonuses().ToList();

            var equipment = new Equipment(def);

            foreach (var stat in statsToBoost)
            {
                var rand = m_Random.Next(maxBonus + 1);

                m_Log.Log($"Applying a bonus of {rand} to stat {stat}", LogLevel.Info);

                equipment.StatBonuses[stat] += rand;
            }

            m_Log.Log($"Final stats: {string.Join(", ", equipment.StatBonuses.Select(m => $"{m.Key}: {m.Value}"))}\n", LogLevel.Info);

            return equipment;
        }
    }
}
