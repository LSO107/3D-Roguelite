using System.Collections.Generic;
using System.Linq;
using System.Text;
using Items.Definitions;
using Player;
using TMPro;
using UnityEngine;
using UnityEngine.EventSystems;
using Utils;

namespace UI.EquipmentUI
{
    internal sealed class EquipmentUI : MonoBehaviour
    {
        [SerializeField] private ButtonManager m_ButtonManager;
        [SerializeField] private TextMeshProUGUI m_StatTypesText;
        [SerializeField] private TextMeshProUGUI m_StatBonusesText;
        [SerializeField] private TextMeshProUGUI m_CombatLevelText;

        private PlayerManager m_PlayerManager;
        private CanvasGroup m_CanvasGroup;

        public List<EquipmentSlotUI> equipmentSlots = new List<EquipmentSlotUI>();

        public bool IsOpen => m_CanvasGroup.interactable;

        public void Instantiate()
        {
            m_PlayerManager = PlayerManager.Instance;
            m_CanvasGroup = GetComponent<CanvasGroup>();

            foreach (var button in equipmentSlots)
            {
                var eventTrigger = button.GetComponent<EventTrigger>();
                AddCallbackToButton(eventTrigger, button.SlotId);
            }

            UpdateLabels();
        }

        public void UpdateSlot(EquipmentSlotId slot)
        {
            var definition = m_PlayerManager.Equipment.GetEquipmentInSlot(slot);

            var matchingSlot = equipmentSlots.Single(e => e.SlotId == slot);
            matchingSlot.UpdateItemSprite(definition);
        }

        private void AddCallbackToButton(EventTrigger eventTrigger, EquipmentSlotId slotId)
        {
            var eventEntry = new EventTrigger.Entry
            {
                eventID = EventTriggerType.PointerClick,
            };

            eventEntry.callback.AddListener(eventData => ClickItem(eventData, slotId));

            eventTrigger.triggers.Add(eventEntry);
        }

        public void ClickItem(BaseEventData eventData, EquipmentSlotId slotId)
        {
            var equipmentItem = m_PlayerManager.Equipment.GetEquipmentInSlot(slotId);

            if (equipmentItem == null)
                return;

            m_PlayerManager.UnequipItem(slotId);
        }

        public void OpenEquipmentInterface()
        {
            m_ButtonManager.DisableButtons();
            UserInterfaceUtils.OpenUserInterface(m_CanvasGroup);
        }

        public void CloseEquipmentInterface()
        {
            UserInterfaceUtils.CloseUserInterface(m_CanvasGroup);
            m_ButtonManager.EnableButtons();
        }

        public void UpdateCombatLevelLabel()
        {
            var combatLevel = m_PlayerManager.PlayerStats.CombatLevel;
            m_CombatLevelText.text = $"Level {combatLevel}";
        }

        /// <summary>
        /// Calculate bonuses using base stats and equipment stats
        /// </summary>
        public void UpdateLabels()
        {
            var playerStats = m_PlayerManager.PlayerStats;
            var playerEquipment = m_PlayerManager.Equipment;

            var baseStats = playerStats.GetBaseStats();
            var equipmentStats = playerEquipment.GetEquipmentStatBonuses();

            var currentStats = baseStats.Keys
                                        .Union(equipmentStats.Keys)
                                        .Select(key =>
                    {
                        baseStats.TryGetValue(key, out var baseValue);
                        equipmentStats.TryGetValue(key, out var equipmentValue);

                        return new KeyValuePair<StatBonus, int>(key, baseValue + equipmentValue);
                    }
                )
                .ToDictionary(kvp => kvp.Key, kvp => kvp.Value);

            SetStatsLabelText(currentStats);
        }

        /// <summary>
        /// Format stats labels and update text labels
        /// </summary>
        private void SetStatsLabelText(IReadOnlyDictionary<StatBonus, int> stats)
        {
            var stringBuilder = new StringBuilder();
            foreach (var stat in stats)
            {
                stringBuilder.Append($"{stat.Key}\n");
            }
            m_StatTypesText.text = stringBuilder.ToString();

            stringBuilder.Clear();
            foreach (var stat in stats)
            {
                stringBuilder.Append($"{stat.Value}\n");
            }

            m_StatBonusesText.text = stringBuilder.ToString();
        }
    }
}
