using System;
using ItemData;
using Items.Definitions;
using Items.Inventory;
using Logging;
using Random = System.Random;

namespace ItemGeneration
{
    internal sealed class ItemFactory
    {
        private readonly IItemGenerator m_HelmetGenerator;
        private readonly IItemGenerator m_ChestplateGenerator;
        private readonly IItemGenerator m_PlatelegsGenerator;
        private readonly IItemGenerator m_WeaponGenerator;

        private readonly Random m_Random;

        public ItemFactory()
        {
            m_Random = new Random();
            m_HelmetGenerator = new HelmetGenerator(m_Random, new FileLog(@"C:\Users\leeok\Desktop\logs\helmetgenerator.txt"));
            m_ChestplateGenerator = new ChestplateGenerator(m_Random, new FileLog(@"C:\Users\leeok\Desktop\logs\chestgenerator.txt"));
            m_PlatelegsGenerator = new PlatelegsGenerator(m_Random, new FileLog(@"C:\Users\leeok\Desktop\logs\legsgenerator.txt"));
            m_WeaponGenerator = new WeaponGenerator(m_Random, new FileLog(@"C:\Users\leeok\Desktop\logs\weapongenerator.txt"));
        }

        public HealthPotion GeneratePotion()
        {
            var itemDatabase = GameManager.Instance.ItemDatabase;
            var definition = itemDatabase.GetRandomHealthPotion();

            var con = new HealthPotion(definition);
            return con;
        }

        public Equipment GenerateEquipmentFromTemplate(EquipmentSlotId equipmentType)
        {
            var itemDatabase = GameManager.Instance.ItemDatabase;

            EquipmentDefinition itemDef = null;
            switch (equipmentType)
            {
                case EquipmentSlotId.Weapon:
                    itemDef = itemDatabase.GetRandomWeapon();
                    break;
                case EquipmentSlotId.Head:
                    itemDef = itemDatabase.GetRandomHelmet();
                    break;
                case EquipmentSlotId.Torso:
                    itemDef = itemDatabase.GetRandomChestplate();
                    break;
                case EquipmentSlotId.Legs:
                    itemDef = itemDatabase.GetRandomPlatelegs();
                    break;
            }

            var rarity = GetRandomRarity();
            return GenerateRandomBonuses(itemDef, rarity);
        }

        private RarityModifier GetRandomRarity()
        {
            var randomNumber = m_Random.Next(0, 100);

            if (randomNumber < 60)
            {
                return RarityModifier.Common;
            }

            if (randomNumber < 90)
            {
                return RarityModifier.Rare;
            }

            return RarityModifier.Epic;
        }

        private Equipment GenerateRandomBonuses(EquipmentDefinition definition, RarityModifier rarity)
        {
            if (definition == null)
                return null;

            switch (definition.EquipmentSlotId)
            {
                case EquipmentSlotId.Torso:
                    return m_ChestplateGenerator.GenerateBonuses(definition, rarity);
                case EquipmentSlotId.Head:
                    return m_HelmetGenerator.GenerateBonuses(definition, rarity);
                case EquipmentSlotId.Legs:
                    return m_PlatelegsGenerator.GenerateBonuses(definition, rarity);
                case EquipmentSlotId.Weapon:
                    return m_WeaponGenerator.GenerateBonuses(definition, rarity);
                default:
                    throw new ArgumentException("I don't know what kind of item this is :D");
            }
        }
    }
}
