using ItemGeneration;
using Items;
using Items.Inventory;
using Player;
using UnityEngine;

namespace ItemData
{
    internal sealed class GroundItem : MonoBehaviour
    {
        private PlayerInventory m_Inventory;
        private ItemFactory m_ItemFactory;
        private GroundItemManager m_GroundItems;

        private string m_ItemId = string.Empty;
        private float m_Timer;
        private float m_Seconds = 30;

        private Transform m_PlayerLocation;

        private void Start()
        {
            m_Inventory = PlayerManager.Instance.Inventory;
            m_GroundItems = GroundItemManager.Instance;
            m_ItemFactory = new ItemFactory();
            m_PlayerLocation = PlayerManager.Instance.transform;
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

            var item = m_GroundItems.FindItem(m_ItemId);

            m_Inventory.AddItem(item);
            m_GroundItems.RemoveItem(m_ItemId);
            Destroy(gameObject);
        }

        private void DestroyGroundItem()
        {
            m_GroundItems.RemoveItem(m_ItemId);
            Destroy(gameObject);
        }
    }
}
