using ItemData;
using Player;

namespace Items.Inventory
{
    internal sealed class HealthPotion : Consumable
    {
        private readonly int m_HealAmount;

        public HealthPotion(PotionDefinition definition) : base(definition)
        {
            m_HealAmount = definition.value;
        }

        public override void Use()
        {
            PlayerManager.Instance.Health.Heal(m_HealAmount);
        }
    }
}
