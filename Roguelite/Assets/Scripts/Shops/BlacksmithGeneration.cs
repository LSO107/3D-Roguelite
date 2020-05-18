using Currency;
using ItemGeneration;
using Items.Definitions;
using Items.Inventory;
using System.Collections.Generic;
using UI.ShopUI;
using UnityEngine;

namespace Shops
{
    internal sealed class BlacksmithGeneration : Shop
    {
        [SerializeField] private ShopUI m_BlacksmithShopUI;
        [SerializeField] private int m_Slots;

        private List<Item> m_ShopItems;
        private ItemFactory m_ItemFactory;

        private PlayerInventory m_Inventory;
        private CurrencyObject m_Currency;

        private void Start()
        {
            m_ItemFactory = new ItemFactory();
            m_Inventory = GameManager.Instance.PlayerManager.Inventory;
            m_Currency = GameManager.Instance.PlayerManager.Currency;
                
            GenerateShopItems();
        }

        public void GenerateShopItems()
        {
            m_ShopItems = new List<Item>
            {
                m_ItemFactory.GenerateEquipmentFromTemplate(EquipmentSlotId.Head),
                m_ItemFactory.GenerateEquipmentFromTemplate(EquipmentSlotId.Torso),
                m_ItemFactory.GenerateEquipmentFromTemplate(EquipmentSlotId.Legs),
                m_ItemFactory.GenerateEquipmentFromTemplate(EquipmentSlotId.Weapon)
            };

            m_BlacksmithShopUI.UpdateShopItems(m_ShopItems);
        }

        public override void PurchaseItem(int slotIndex)
        {
            var item = m_ShopItems[slotIndex];

            if (m_Currency.Quantity < item.GoldCost || !m_Inventory.HasEmptySlots(1))
                return;

            Debug.Log($"Purchased item in slot: {slotIndex}");

            m_Currency.RemoveGold(item.GoldCost);

            m_Inventory.AddItem(item);
            m_ShopItems.Remove(item);
            m_ShopItems.Add(CreateShopItem(item as Equipment));

            m_BlacksmithShopUI.UpdateShopItems(m_ShopItems);
        }

        private Equipment CreateShopItem(Equipment item)
        {
            return m_ItemFactory.GenerateEquipmentFromTemplate(item.EquipmentSlotId);
        }
    }
}
