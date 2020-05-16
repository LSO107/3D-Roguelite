using System.Collections.Generic;
using Currency;
using ItemGeneration;
using Items.Definitions;
using Items.Inventory;
using UI.ShopUI;
using UnityEngine;

namespace Shops
{
    internal sealed class BlacksmithGeneration : MonoBehaviour
    {
        [SerializeField] private ShopUI m_BlacksmithShopUI;
        [SerializeField] private int m_Slots;

        private List<Equipment> m_ShopItems;
        private ItemFactory m_ItemFactory;

        private PlayerInventory m_Inventory;
        private CurrencyObject m_Currency;

        private void Start()
        {
            m_ItemFactory = new ItemFactory();
            m_Inventory = GameManager.Instance.PlayerManager.Inventory;
            m_Currency = GameManager.Instance.PlayerManager.Currency;

            //m_BlacksmithShopUI.Instantiate();
            //GenerateShopItems();
        }

        public void GenerateShopItems()
        {
            m_ShopItems = new List<Equipment>
            {
                m_ItemFactory.GenerateEquipmentFromTemplate(EquipmentSlotId.Head),
                m_ItemFactory.GenerateEquipmentFromTemplate(EquipmentSlotId.Torso),
                m_ItemFactory.GenerateEquipmentFromTemplate(EquipmentSlotId.Legs),
                m_ItemFactory.GenerateEquipmentFromTemplate(EquipmentSlotId.Weapon)
            };

            // BlacksmithUI.DisplayItems(m_ShopItems);
            m_BlacksmithShopUI.UpdateShopItems(m_ShopItems);
        }

        public void PurchaseItem(int slotIndex)
        {
            var item = m_ShopItems[slotIndex];

            if (m_Currency.CurrencyQuantity < item.GoldCost || !m_Inventory.HasEmptySlots(1))
                return;

            m_Currency.RemoveGold(item.GoldCost);
            m_ShopItems.Remove(item);
            m_Inventory.AddItem(item);

            // Update UI
            m_BlacksmithShopUI.UpdateShopItems(m_ShopItems);
        }
    }
}
