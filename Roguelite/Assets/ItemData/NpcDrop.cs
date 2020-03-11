using System;
using Items.Definitions;
using Items.Inventory;
using UnityEngine;
using Random = System.Random;

namespace ItemData
{
    internal sealed class NpcDrop : MonoBehaviour
    {
        private PlayerInventory m_Inventory;

        private Random m_Random;

        public string ItemId = "";

        private void Start()
        {
            m_Inventory = GameManager.Instance.PlayerManager.Inventory;
        }

        public void SetItemId(string id)
        {
            ItemId = id;
        }

        private void OnMouseDown()
        {
            var itemDb = GameManager.Instance.ItemDatabase;
            var item = itemDb.GetItem(ItemId);
            Debug.Log(item.Id);

            if (item == null)
            {
                Debug.Log("ITEM WAS NULL, NOT IN DB.");
            }
            else
            {
                Debug.Log("ITEM WAS IN DB.");
                m_Inventory.AddItem(item);
            }

            Destroy(gameObject);
        }
    }
}
