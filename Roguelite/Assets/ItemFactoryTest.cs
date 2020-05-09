using ItemData;
using ItemGeneration;
using Logging;
using UnityEngine;

using Random = System.Random;

public class ItemFactoryTest : MonoBehaviour
{
    [SerializeField] private EquipmentDefinition[] m_Helmets;

    private ILog m_Log;
    private Random m_Random;
    private HelmetGenerator m_Generator;

    private void Awake()
    {
        m_Log = new FileLog(@"C:\Users\leeok\Desktop\blah.txt");
        m_Random = new Random();
        m_Generator = new HelmetGenerator(m_Random, m_Log);
    }

    private void Start()
    {
        for (var i = 0; i < 50; i++)
        {
            var randHelm = m_Helmets[m_Random.Next(m_Helmets.Length)];
            m_Generator.GenerateBonuses(randHelm, GetRandomRarity());
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
