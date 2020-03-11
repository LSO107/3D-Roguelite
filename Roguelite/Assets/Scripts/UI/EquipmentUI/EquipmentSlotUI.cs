using ItemData;
using Items.Definitions;
using UI.Tooltip;
using UnityEngine;
using UnityEngine.UI;

namespace UI.EquipmentUI
{
    internal sealed class EquipmentSlotUI : MonoBehaviour
    {
        public Image spriteImage;
        public Sprite DefaultIcon;

        public EquipmentSlotId SlotId;

        private TooltipPointerHandler m_TooltipPointerHandler;

        private void Awake()
        {
            m_TooltipPointerHandler = GetComponent<TooltipPointerHandler>();
            spriteImage = GetComponent<Image>();
            UpdateItemSprite(null);
        }

        public void UpdateItemSprite(EquipmentItem item)
        {
            m_TooltipPointerHandler.UpdateItem(item);
            spriteImage.sprite = item != null ? item.Icon : DefaultIcon;
            var colour = spriteImage.color;
            colour.a = item != null ? 1 : 0.2f;
            spriteImage.color = colour;
        }
    }
}