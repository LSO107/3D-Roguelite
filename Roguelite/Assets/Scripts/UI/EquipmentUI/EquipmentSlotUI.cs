using Items.Definitions;
using UnityEngine;
using UnityEngine.UI;

namespace UI.EquipmentUI
{
    internal sealed class EquipmentSlotUI : MonoBehaviour
    {
        public Image spriteImage;
        public Sprite DefaultIcon;

        public EquipmentSlotId SlotId;

        private void Awake()
        {
            spriteImage = GetComponent<Image>();
            UpdateItemSprite(null);
        }

        public void UpdateItemSprite(Equipment item)
        {
            spriteImage.sprite = item != null ? item.Sprite : DefaultIcon;
            var colour = spriteImage.color;
            colour.a = item != null ? 1 : 0.2f;
            spriteImage.color = colour;
        }
    }
}