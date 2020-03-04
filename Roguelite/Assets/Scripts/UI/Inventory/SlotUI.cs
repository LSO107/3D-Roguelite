using Items.Definitions;
using UnityEngine;
using UnityEngine.UI;

namespace UI.Inventory
{
    internal sealed class SlotUI : MonoBehaviour
    {
        private Item m_Item;
        private Image m_Image;

        private void Awake()
        {
            m_Image = GetComponent<Image>();
            UpdateItemSprite(null);
        }

        public void UpdateItemSprite(Item itemDefinition)
        {
            m_Item = itemDefinition;

            if (m_Item != null)
            {
                m_Image.color = Color.white;
                m_Image.sprite = m_Item.Sprite;
            }
            else
            {
                m_Image.color = Color.clear;
            }
        }
    }
}
