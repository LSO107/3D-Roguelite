using ItemData;
using ItemGeneration;
using Logging;
using UnityEngine;

using Random = System.Random;

namespace Debugging
{
    public class ItemFactoryTest : MonoBehaviour
    {
        [SerializeField] private EquipmentDefinition[] m_Helmets;
        [SerializeField] private EquipmentDefinition[] m_Chestplates;
        [SerializeField] private EquipmentDefinition[] m_Platelegs;
        [SerializeField] private EquipmentDefinition[] m_Weapons;

        private Random m_Random;
        private HelmetGenerator m_HelmetGenerator;
        private ChestplateGenerator m_ChestplateGenerator;
        private PlatelegsGenerator m_PlatelegsGenerator;
        private WeaponGenerator m_WeaponGenerator;

        private void Awake()
        {
            m_Random = new Random();

            m_HelmetGenerator = new HelmetGenerator(m_Random);
            m_ChestplateGenerator = new ChestplateGenerator(m_Random);
            m_PlatelegsGenerator = new PlatelegsGenerator(m_Random);
            m_WeaponGenerator = new WeaponGenerator(m_Random);
        }

        private void Start()
        {
            for (var i = 0; i < 50; i++)
            {
                var randHelm = m_Helmets[m_Random.Next(m_Helmets.Length)];
                var randChest = m_Chestplates[m_Random.Next(m_Chestplates.Length)];
                var randLegs = m_Platelegs[m_Random.Next(m_Platelegs.Length)];
                var randWeapon = m_Weapons[m_Random.Next(m_Weapons.Length)];

                m_HelmetGenerator.GenerateBonuses(randHelm, GetRandomRarity());
                m_ChestplateGenerator.GenerateBonuses(randChest, GetRandomRarity());
                m_PlatelegsGenerator.GenerateBonuses(randLegs, GetRandomRarity());
                m_WeaponGenerator.GenerateBonuses(randWeapon, GetRandomRarity());
            }
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
    }
}
