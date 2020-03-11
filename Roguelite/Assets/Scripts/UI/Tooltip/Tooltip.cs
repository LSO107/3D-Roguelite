﻿using System;
using System.Text;
using Extensions;
using ItemDatabase;
using Items.Definitions;
using TMPro;
using UnityEngine;

namespace UI.Tooltip
{
    internal sealed class Tooltip : MonoBehaviour
    {
        private CanvasGroup m_CanvasGroup;
        private TextMeshProUGUI m_Text;

        private void Start()
        {
            m_CanvasGroup = GetComponent<CanvasGroup>();
            m_Text = GetComponentInChildren<TextMeshProUGUI>();
        }

        public void OpenTooltip(ItemDefinition item)
        {
            transform.position = Input.mousePosition;
            UpdateText(item);
            m_CanvasGroup.ToggleCanvasGroup(true);
        }

        public void CloseTooltip()
        {
            m_CanvasGroup.ToggleCanvasGroup(false);
        }

        /// <summary>
        /// Update the tooltip text with the corresponding item text
        /// </summary>
        private void UpdateText(ItemDefinition item)
        {
            if (item is ConsumableItem consumable)
            {
                m_Text.text = GetConsumableText(consumable);
            }
            else if (item is EquipmentItem equipment)
            {
                m_Text.text = GetEquipmentText(equipment);
            }
        }

        /// <summary>
        /// Compares the item stats with the corresponding equipmentSlotId
        /// </summary>
        private static string GetEquipmentText(EquipmentItem inventoryItem)
        {
            var sb = new StringBuilder();

            var equipment = GameManager.Instance.PlayerManager.Equipment;

            sb.Append($"<size=20><color=orange>{inventoryItem.itemName}</color></size>\n");

            foreach (var value in Enum.GetValues(typeof(Stat)))
            {
                var inventoryStat = inventoryItem.StatBonuses[(Stat) value];

                var equipmentStat = equipment?.GetEquipmentStatBonuses()[(Stat) value];

                if (inventoryStat > equipmentStat)
                {
                    sb.Append($"<color=green>{inventoryStat} {value}</color>\n");
                }
                else if (inventoryStat < equipmentStat)
                {
                    sb.Append($"<color=red>{inventoryStat} {value}</color>\n");
                }
                else
                {
                    sb.Append($"<color=white>{inventoryStat} {value}</color>\n");
                }
            }

            return sb.ToString();
        }

        /// <summary>
        /// Extracts the consumable item information
        /// </summary>
        private static string GetConsumableText(ConsumableItem item)
        {
            var sb = new StringBuilder();
            sb.Append($"<size=20><color=orange>{item.itemName}</color></size>\n");
            sb.Append($"<color=white>{item.description}</color>");
            return sb.ToString();
        }
    }
}
