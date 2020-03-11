using System;
using Items.Definitions;
using Items.Inventory;
using UnityEngine;
using Random = System.Random;

namespace ItemDatabase
{
    internal sealed class Drop : MonoBehaviour
    {
        public EquipmentItem itemDefinition;

        private PlayerInventory m_Inventory;

        private Random m_Random;

        private void Start()
        {
            m_Inventory = GameManager.Instance.PlayerManager.Inventory;
            m_Random = new Random();
        }

        private void OnMouseDown()
        {
            GenerateRandomBonuses();

            //m_Inventory.AddItem(itemDefinition);

            foreach (var stat in itemDefinition.bonuses.Values)
            {
                Debug.Log(stat);
            }
        }

        private void GenerateRandomBonuses()
        {
            var level = GameManager.Instance.PlayerManager.Stats.CombatLevel;

            var maximumValue = level * 10;

            foreach (var stat in Enum.GetValues(typeof(Stat)))
            {
                itemDefinition.bonuses[(Stat) stat] += m_Random.Next(maximumValue);
            }
        }
    }
}
