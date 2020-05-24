using Items;
using Items.Inventory;
using Player;
using UI.Tooltip;
using UnityEngine;

namespace ItemData
{
    internal sealed class GroundItem : MonoBehaviour
    {
        private PlayerInventory m_Inventory;
        private GroundItemManager m_GroundItems;
        private Tooltip m_Tooltip;

        private string m_ItemId = string.Empty;
        private float m_Timer;
        private float m_Seconds = 30;

        private Transform m_PlayerLocation;

        private void Start()
        {
            m_Inventory = PlayerManager.Instance.Inventory;
            m_GroundItems = GroundItemManager.Instance;
            m_PlayerLocation = PlayerManager.Instance.transform;
            m_Tooltip = GameManager.Instance.Tooltip;
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

        private void OnMouseEnter()
        {
            PlayerManager.Instance.GetComponent<PlayerController>().ToggleIsInputBlocked(true);
        }

        private void OnMouseOver()
        {
            var item = m_GroundItems.FindItem(m_ItemId);
            m_Tooltip.ShowTooltipItemNameOnly(item);
        }

        private void OnMouseExit()
        {
            PlayerManager.Instance.GetComponent<PlayerController>().ToggleIsInputBlocked(false);
            m_Tooltip.CloseTooltip();
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
