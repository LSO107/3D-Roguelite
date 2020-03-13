using Items.Inventory;
using UnityEngine;

namespace ItemData
{
    internal sealed class GroundItem : MonoBehaviour
    {
        private PlayerInventory m_Inventory;
        private ItemDatabase m_ItemDatabase;
        private ItemGenerator m_ItemGenerator;

        private string m_ItemId = string.Empty;
        private float m_Timer;
        private float m_Seconds = 30;

        private void Start()
        {
            m_Inventory = GameManager.Instance.PlayerManager.Inventory;
            m_ItemDatabase = GameManager.Instance.ItemDatabase.GroundItems;
            m_ItemGenerator = new ItemGenerator();
        }

        private void Update()
        {
            m_Timer += Time.deltaTime;

            if (m_Timer >= m_Seconds)
            {
                DestroyGroundItem();
            }
        }

        public void RegisterGroundItem(string itemId)
        {
            m_ItemId = itemId;
        }

        private void OnMouseDown()
        {
            var existingItem = m_ItemDatabase.FindItem(m_ItemId);

            if (existingItem == null)
            {
                var item = m_ItemGenerator.GenerateEquipmentItem();
                m_Inventory.AddItem(item);
                Debug.Log("Item was not in database, new item generated");
            }
            else
            {
                m_Inventory.AddItem(existingItem);
                Debug.Log("Item was in database, recreated existing item");
            }

            GameManager.Instance.ItemDatabase.GroundItems.RemoveItem(m_ItemId);
            Destroy(gameObject);
        }

        private void DestroyGroundItem()
        {
            m_ItemDatabase.RemoveItem(m_ItemId);
            Destroy(gameObject);
        }
    }
}
