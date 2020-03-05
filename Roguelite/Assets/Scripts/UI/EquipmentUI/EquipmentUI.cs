using System.Collections.Generic;
using System.Linq;
using System.Text;
using Extensions;
using Items.Definitions;
using Player;
using TMPro;
using UnityEngine;
using UnityEngine.EventSystems;

namespace UI.EquipmentUI
{
    internal sealed class EquipmentUI : MonoBehaviour
    {
        private PlayerManager m_PlayerManager;
        private CanvasGroup m_CanvasGroup;

        [SerializeField] private TextMeshProUGUI m_StatTypesText;
        [SerializeField] private TextMeshProUGUI m_StatBonusesText;
        [SerializeField] private TextMeshProUGUI m_CombatLevelText;

        public Dictionary<EquipmentSlotId, Equipment> CurrentEquipmentSlots;
        public List<EquipmentSlotUI> EquipmentSlots = new List<EquipmentSlotUI>();

        public void Instantiate()
        {
            m_PlayerManager = GameManager.Instance.PlayerManager;
            m_CanvasGroup = GetComponent<CanvasGroup>();

            CurrentEquipmentSlots = new Dictionary<EquipmentSlotId, Equipment>
            {
                {EquipmentSlotId.Head, null},
                {EquipmentSlotId.Torso, null},
                {EquipmentSlotId.Legs, null},
                {EquipmentSlotId.Weapon, null},
            };

            foreach (var button in EquipmentSlots)
            {
                var eventTrigger = button.GetComponent<EventTrigger>();
                AddCallbackToButton(eventTrigger, button.SlotId);
            }

            UpdateLabels();
        }

        public void UpdateSlot(EquipmentSlotId slot)
        {
            var definition = m_PlayerManager.PlayerEquipment.GetEquipmentInSlot(slot);

            var matchingSlot = EquipmentSlots.Single(e => e.SlotId == slot);
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
            var equipmentItem = m_PlayerManager.PlayerEquipment.GetEquipmentInSlot(slotId);

            if (equipmentItem == null)
                return;

            m_PlayerManager.UnequipItem(slotId);
        }

        public void CloseEquipmentInterface()
        {
            m_CanvasGroup.ToggleCanvasGroup(false);
        }

        public void UpdateCombatLevelLabel()
        {
            var combatLevel = m_PlayerManager.Stats.CombatLevel;
            m_CombatLevelText.text = $"Level {combatLevel}";
        }

        public void UpdateLabels()
        {
            var playerStats = m_PlayerManager.Stats;
            var playerEquipment = m_PlayerManager.PlayerEquipment;

            var baseStats = playerStats.GetBaseStats();
            var equipmentStats = playerEquipment.GetEquipmentStatBonuses();

            var currentStats = baseStats.Keys
                                        .Union(equipmentStats.Keys)
                                        .Select(key =>
                    {
                        baseStats.TryGetValue(key, out var baseValue);
                        equipmentStats.TryGetValue(key, out var equipmentValue);

                        return new KeyValuePair<Stat, int>(key, baseValue + equipmentValue);
                    }
                )
                .ToDictionary(kvp => kvp.Key, kvp => kvp.Value);

            SetStatsLabelText(currentStats);
        }

        private void SetStatsLabelText(IReadOnlyDictionary<Stat, int> stats)
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
