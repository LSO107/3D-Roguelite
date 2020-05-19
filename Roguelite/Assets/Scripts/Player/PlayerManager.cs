﻿using System.Collections.Generic;
using Character.Health;
using Character.Movement;
using Currency;
using Items.Definitions;
using Items.Inventory;
using Player.Experience;
using UI.EquipmentUI;
using UI.InventoryPanelUI;
using UnityEngine;

namespace Player
{
    [RequireComponent(typeof(HealthObject))]
    [RequireComponent(typeof(ExperienceObject))]
    internal sealed class PlayerManager : MonoBehaviour
    {
        public HealthObject Health { get; private set; }
        public ExperienceObject Experience { get; private set; }
        public CurrencyObject Currency { get; private set; }
        public PlayerInventory Inventory { get; private set; }
        public PlayerEquipment Equipment { get; private set; }
        public PlayerStats PlayerStats { get; private set; }

        public InventoryUI InventoryUI;
        public EquipmentUI EquipmentUI;

        private CameraFollow m_Camera;

        public static PlayerManager Instance;

        private void Awake()
        {
            if (Instance == null)
            {
                Instance = this;
            }
            else if (Instance != this)
            {
                Destroy(gameObject);
            }

            m_Camera = Camera.main.GetComponent<CameraFollow>();
            PlayerStats = GetComponent<PlayerStats>();

            Inventory = new PlayerInventory(new List<Item>());
            Equipment = new PlayerEquipment();

            Health = GetComponent<HealthObject>();
            Experience = GetComponent<ExperienceObject>();
            Currency = GetComponent<CurrencyObject>();
        }

        private void Start()
        {
            PlayerStats.Initialize(1);
            InventoryUI.Instantiate();
            EquipmentUI.Instantiate();
        }

        public void EquipItem(int slotIndex)
        {
            if (!(Inventory.GetItemInSlot(slotIndex) is Equipment equipment))
                return;

            var item = Equipment.GetEquipmentInSlot(equipment.EquipmentSlotId);

            Equipment.Equip(equipment);
            Inventory.RemoveItem(slotIndex);

            if (item != null)
            {
                Inventory.AddItem(item);
            }

            EquipmentUI.UpdateSlot(equipment.EquipmentSlotId);
        }

        public void UnequipItem(EquipmentSlotId slotId)
        {
            var equipment = Equipment.GetEquipmentInSlot(slotId);

            if (!Inventory.HasEmptySlots(1))
                return;

            Equipment.Unequip(slotId);
            Inventory.AddItem(equipment);

            EquipmentUI.UpdateSlot(equipment.EquipmentSlotId);
            InventoryUI.UpdateSlots();
        }

        public void DisableInput()
        {
            GetComponent<CharacterUserInput>().IsFrozen = true;
            GetComponent<PlayerController>().ToggleIsInputBlocked(true);
            m_Camera.LockCamera();
        }

        public void EnableInput()
        {
            GetComponent<CharacterUserInput>().IsFrozen = false;
            GetComponent<PlayerController>().ToggleIsInputBlocked(false);
            m_Camera.FreeCamera();
        }
    }
}
