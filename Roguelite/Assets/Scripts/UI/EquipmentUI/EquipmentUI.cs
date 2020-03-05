using System.Collections.Generic;
using System.Linq;
using Items.Definitions;
using Items.EquipmentSystem;
using Items.Inventory;
using Player;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

namespace UI.EquipmentUI
{
    internal sealed class EquipmentUI : MonoBehaviour
    {
        public PlayerManager Player;

        private PlayerEquipment m_PlayerEquipment;
        public Dictionary<EquipmentSlotId, Equipment> CurrentEquipmentSlots;

        public List<EquipmentSlotUI> EquipmentSlots = new List<EquipmentSlotUI>();

        public void Instantiate()
        {
            m_PlayerEquipment = GameManager.Instance.PlayerManager.PlayerEquipment;
            //EquipmentSlots = GetComponentsInChildren<EquipmentSlotUI>().ToList();

            CurrentEquipmentSlots = new Dictionary<EquipmentSlotId, Equipment>
            {
                {EquipmentSlotId.Head, null},
                {EquipmentSlotId.Torso, null},
                {EquipmentSlotId.Legs, null},
                {EquipmentSlotId.Weapon, null},
            };

            // Register on click events for each button in our equipment window
            // each button will tell our callback which slot it represents
            // so that the buttons are implementation agnostic.
            //
            foreach (var button in EquipmentSlots)
            {
                var eventTrigger = button.GetComponent<EventTrigger>();
                AddCallbackToButton(eventTrigger, button.SlotId);
            }
        }

        /// <summary>
        /// Update the slot indexes item placeholder
        /// sprite with the itemDefinition sprite
        /// </summary>
        /// 
        public void UpdateSlot(EquipmentSlotId slot)
        {
            var definition = m_PlayerEquipment.GetEquipmentInSlot(slot);

            var matchingSlot = EquipmentSlots.Single(e => e.SlotId == slot);
            matchingSlot.UpdateItemSprite(definition);
        }

        /// <summary>
        /// Waits for mouse click to trigger an event
        /// </summary>
        /// 
        private void AddCallbackToButton(EventTrigger eventTrigger, EquipmentSlotId slotId)
        {
            var eventEntry = new EventTrigger.Entry
            {
                eventID = EventTriggerType.PointerClick,
            };

            eventEntry.callback.AddListener(eventData => ClickItem(eventData, slotId));

            eventTrigger.triggers.Add(eventEntry);
        }

        /// <summary>
        /// If item clicked, unequip the item then update the slot
        /// </summary>
        /// 
        public void ClickItem(BaseEventData eventData, EquipmentSlotId slotId)
        {
            // Get the equipment item in the slot we've just clicked
            var equipmentItem = m_PlayerEquipment.GetEquipmentInSlot(slotId);
            // If there isn't an item in that slot, return
            if (equipmentItem == null)
                return;

            // Tell the player to unequip the item in that slot
            Player.UnequipItem(slotId);
        }
    }
}
