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

        private Transform m_PlayerLocation;

        private void Start()
        {
            m_Inventory = GameManager.Instance.PlayerManager.Inventory;
            m_ItemDatabase = GameManager.Instance.ItemDatabase.GroundItems;
            m_ItemGenerator = new ItemGenerator();
            m_PlayerLocation = GameManager.Instance.PlayerManager.transform;
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
            var distance = Vector3.Distance(transform.position, m_PlayerLocation.position);

            if (distance > 2)
                return;

            var itemDefinition = m_ItemDatabase.FindItem(m_ItemId);

            if (itemDefinition == null)
            {
                var item = m_ItemGenerator.GenerateEquipmentItem();
                m_Inventory.AddItem(item);
                Debug.Log("Item was not in database, new item bonuses generated");
            }
            else
            {
                m_Inventory.AddItem(itemDefinition);
                Debug.Log("Item found in database, recreated existing item");
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
