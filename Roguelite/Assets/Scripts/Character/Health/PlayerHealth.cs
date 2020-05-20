using UnityEngine;

namespace Character.Health
{
    internal sealed class PlayerHealth : HealthObject
    {
        private void Start()
        {
            m_HealthBarUpdater.UpdateBar(CurrentHealth, MaxHealth);
        }

        private void Update()
        {
            RegenerateHealth();
        }

        protected override void ActionOnDeath()
        {
            // Respawn Player
            // Lose items etc?

            m_HealthDefinition.ResetHealth();
        }
    }
}
