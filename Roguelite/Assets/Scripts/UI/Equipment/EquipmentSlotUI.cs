using Items.Definitions;
using Items.Inventory;
using UI.Tooltip;
using UnityEngine;
using UnityEngine.UI;

namespace UI.EquipmentUI
{
    internal sealed class EquipmentSlotUI : MonoBehaviour
    {
#pragma warning disable 0649
        public Image spriteImage;
        public Sprite DefaultIcon;

        public EquipmentSlotId SlotId;
#pragma warning restore 0649

        private TooltipPointerHandler m_TooltipPointerHandler;

        private void Awake()
        {
            m_TooltipPointerHandler = GetComponent<TooltipPointerHandler>();
            spriteImage = GetComponent<Image>();
            UpdateItemSprite(null);
        }

        public void UpdateItemSprite(Equipment equipment)
        {
            m_TooltipPointerHandler.UpdateItem(equipment);
            spriteImage.sprite = equipment != null ? equipment.Icon : DefaultIcon;
            var colour = spriteImage.color;
            colour.a = equipment != null ? 1 : 0.2f;
            spriteImage.color = colour;
        }
    }
}