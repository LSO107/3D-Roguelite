using Items.Definitions;

namespace Items.Consumables
{
    internal sealed class HealthPotion : Consumable
    {
        private const string IconPath = "ItemIcons/Consumables/HealthPotion";
        private const string ItemName = "Health Potion";
        private const int HealingAmount = 10;

        public HealthPotion()
            : base(ItemName, IconPath)
        {
        }

        public override void Use()
        {
            GameManager.Instance.PlayerHealth.healthDefinition.Heal(HealingAmount);
        }
    }
}