using Items.Definitions;

namespace Items.Consumables
{
    internal sealed class HealthPotion : Consumable
    {
        private const string IconPath = "ItemIcons/Consumables/HealthPotion";
        private const string ItemName = "Health Potion";
        private const string ItemDescription = "Heals 10 health points";
        private const int HealingValue = 10;

        public HealthPotion()
            : base(ItemName, IconPath, ItemDescription, HealingValue)
        {
        }

        public override void Use()
        {
            var playerHealth = GameManager.Instance.PlayerManager.Health;
            playerHealth.Heal(Value);
        }
    }
}