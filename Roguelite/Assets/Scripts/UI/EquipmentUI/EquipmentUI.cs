using System.Collections.Generic;
using System.Linq;
using Items.Definitions;
using Player;
using UnityEngine;
using UnityEngine.EventSystems;

namespace UI.EquipmentUI
{
    internal sealed class EquipmentUI : MonoBehaviour
    {
        private PlayerManager m_PlayerManager;

        public Dictionary<EquipmentSlotId, Equipment> CurrentEquipmentSlots;

        public List<EquipmentSlotUI> EquipmentSlots = new List<EquipmentSlotUI>();

        public void Instantiate()
        {
            m_PlayerManager = GameManager.Instance.PlayerManager;

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
    }
}
