using System;
using Items.Definitions;
using Items.Inventory;
using UnityEngine;

using Random = System.Random;

namespace ItemData
{
    internal sealed class ItemGenerator 
    {
        private EquipmentDefinition item;
        private readonly Random m_Random;

        public ItemGenerator()
        {
            m_Random = new Random();
        }

        public Consumable GeneratePotion()
        {
            var itemDatabase = GameManager.Instance.ItemDatabase;
            var consumableContainer = itemDatabase.GetRandomHealthPotion();
            var potion = consumableContainer.CreateInstance();

            var con = new Consumable(potion);
            return con;
        }

        public Equipment GenerateEquipmentFromTemplate()
        {
            var itemDatabase = GameManager.Instance.ItemDatabase;
            item = itemDatabase.GetRandomWeapon();
            GameObject.Instantiate(item.Prefab);

            return GenerateRandomBonuses(item);
        }

        private Equipment GenerateRandomBonuses(EquipmentDefinition definition)
        {
            switch (definition.EquipmentSlotId)
            {
                case EquipmentSlotId.Torso:
                    return GenerateRandomChestplate(definition);
                case EquipmentSlotId.Head:
                    return GenerateRandomHelmet(definition);
                case EquipmentSlotId.Legs:
                    return GenerateRandomLegs(definition);
                case EquipmentSlotId.Weapon:
                    return GenerateRandomWeapon(definition);
                default:
                    throw new ArgumentException("I don't know what kind of item this is :D");
            }
        }

        private Equipment GenerateRandomChestplate(EquipmentDefinition definition)
        {
            // Max bonus: player level * 3 + rarity modifier
            var level = GameManager.Instance.PlayerManager.PlayerStats.CombatLevel;
            var maximumValue = level * 3;

            foreach (var stat in Enum.GetValues(typeof(StatBonus)))
            {
                definition.StatBonuses[(StatBonus)stat] += m_Random.Next(maximumValue);
            }

            return new Equipment(definition);
        }

        private Equipment GenerateRandomHelmet(EquipmentDefinition definition)
        {
            var level = GameManager.Instance.PlayerManager.PlayerStats.CombatLevel;
            var maximumValue = level * 3;

            foreach (var stat in Enum.GetValues(typeof(StatBonus)))
            {
                definition.StatBonuses[(StatBonus)stat] += m_Random.Next(maximumValue);
            }

            return new Equipment(definition);
        }

        private Equipment GenerateRandomLegs(EquipmentDefinition definition)
        {
            var level = GameManager.Instance.PlayerManager.PlayerStats.CombatLevel;
            var maximumValue = level * 3;

            foreach (var stat in Enum.GetValues(typeof(StatBonus)))
            {
                definition.StatBonuses[(StatBonus)stat] += m_Random.Next(maximumValue);
            }

            return new Equipment(definition);
        }

        private Equipment GenerateRandomWeapon(EquipmentDefinition definition)
        {
            if (!(definition is WeaponDefinition def))
            {
                throw new ArgumentException("Tried to generate random weapon, but received not a weapon :D");
            }

            var level = GameManager.Instance.PlayerManager.PlayerStats.CombatLevel;
            var maximumValue = level * 3;

            foreach (var stat in Enum.GetValues(typeof(StatBonus)))
            {
                def.StatBonuses[(StatBonus)stat] += m_Random.Next(maximumValue);
            }

            return new Equipment(def);
        }
    }
}
