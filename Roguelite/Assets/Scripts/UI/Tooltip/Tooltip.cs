using System;
using System.Text;
using Extensions;
using ItemData;
using Items.Definitions;
using Items.Inventory;
using Player;
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

        public void ShowTooltipItemNameOnly(Item item)
        {
            transform.position = Input.mousePosition;

            string colour;

            if (item is Equipment eq)
                colour = GetNameColour(eq.Rarity);
            else
                colour = "#c0c0c0ff";

            m_Text.text = $"<size=20><color={colour}>{item.Name}</color></size>";
            m_CanvasGroup.ToggleCanvasGroup(true);
        }

        public void OpenTooltip(Item item)
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
        private void UpdateText(Item item)
        {
            if (item is Consumable consumable)
            {
                m_Text.text = GetConsumableText(consumable);
            }
            else if (item is Equipment equipment)
            {
                m_Text.text = GetEquipmentText(equipment);
            }
        }

        /// <summary>
        /// Compares the item stats with the corresponding equipmentSlotId
        /// </summary>
        private static string GetEquipmentText(Equipment equipment)
        {
            var sb = new StringBuilder();

            var eq = PlayerManager.Instance.Equipment;

            var colour = GetNameColour(equipment.Rarity);
            sb.Append($"<size=20><color={colour}>{equipment.Name}</color></size>\n");

            foreach (var value in Enum.GetValues(typeof(StatBonus)))
            {
                var inventoryStat = equipment.StatBonuses[(StatBonus) value];

                var equipmentStat = eq?.GetEquipmentStatBonuses()[(StatBonus) value];

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

        private static string GetNameColour(RarityModifier rarity)
        {
            switch (rarity)
            {
                case RarityModifier.Common:
                    return "#c0c0c0ff";
                case RarityModifier.Rare:
                    return "#0471FD";
                case RarityModifier.Epic:
                    return "orange";
                default:
                    throw new ArgumentException("Did not recognise rarity modifier");
            }
        }

        /// <summary>
        /// Extracts the consumable item information
        /// </summary>
        private static string GetConsumableText(Consumable consumable)
        {
            var sb = new StringBuilder();
            sb.Append($"<size=20><color=orange>{consumable.Name}</color></size>\n");
            sb.Append($"<color=white>{consumable.Description}</color>");
            return sb.ToString();
        }
    }
}
