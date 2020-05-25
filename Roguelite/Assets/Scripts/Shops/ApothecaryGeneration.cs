using System.Collections.Generic;
using Currency;
using ItemGeneration;
using Items.Inventory;
using Player;
using UI.ShopUI;
using UnityEngine;

namespace Shops
{
    internal sealed class ApothecaryGeneration : Shop
    {
        [SerializeField] private ShopUI m_ApothecaryUI = null;

        private List<Item> m_ShopItems;
        private ItemFactory m_ItemFactory;

        private PlayerInventory m_Inventory;
        private CurrencyObject m_Currency;

        private void Start()
        {
            m_ItemFactory = new ItemFactory();
            m_Inventory = PlayerManager.Instance.Inventory;
            m_Currency = PlayerManager.Instance.Currency;

            GenerateShopItems();
        }

        public void GenerateShopItems()
        {
            m_ShopItems = new List<Item>
            {
                m_ItemFactory.GeneratePotion(),
                m_ItemFactory.GeneratePotion(),
                m_ItemFactory.GeneratePotion(),
                m_ItemFactory.GeneratePotion()
            };

            m_ApothecaryUI.UpdateShopItems(m_ShopItems);
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
            m_ShopItems.Add(CreateShopItem());

            m_ApothecaryUI.UpdateShopItems(m_ShopItems);
        }

        private Consumable CreateShopItem()
        {
            return m_ItemFactory.GeneratePotion();
        }
    }
}
