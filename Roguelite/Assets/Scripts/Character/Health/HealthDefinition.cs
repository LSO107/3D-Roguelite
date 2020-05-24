using System;

namespace Character.Health
{
    internal sealed class HealthDefinition
    {
        public int CurrentHealth { get; private set; }
        public int MaxHealth { get; }
        public int DamageTaken { get; private set; }
        public bool IsDead => CurrentHealth <= 0;

        public HealthDefinition()
        {
            CurrentHealth = 100;
            MaxHealth = 100;
        }

        public HealthDefinition(int currentHealth)
        {
            CurrentHealth = currentHealth;
            MaxHealth = 100;

            ClampHealth();
        }

        public HealthDefinition(int currentHealth, int maxHealth)
        {
            if (currentHealth <= 0)
                throw new ArgumentException($"Current health was {currentHealth}, cannot be 0 or less.");

            CurrentHealth = currentHealth;
            MaxHealth = maxHealth;

            ClampHealth();
        }

        /// <summary>
        /// Decrease health by the amount
        /// </summary>
        public void Damage(int amount)
        {
            if (IsDead)
                return;

            DamageTaken += amount;
            CurrentHealth -= amount;
            ClampHealth();
        }

        public void ResetDamageTaken()
        {
            DamageTaken = 0;
        }

        /// <summary>
        /// Increase the health by the amount
        /// </summary>
        public void Heal(int amount)
        {
            if (IsDead)
                return;

            CurrentHealth += amount;
            ClampHealth();
        }

        public void ResetHealth()
        {
            CurrentHealth = MaxHealth;
        }

        /// <summary>
        /// Clamp health at 0 and <see cref="MaxHealth"/>
        /// </summary>
        private void ClampHealth()
        {
            if (CurrentHealth < 0)
            {
                CurrentHealth = 0;
            }
            else if (CurrentHealth > MaxHealth)
            {
                CurrentHealth = MaxHealth;
            }
        }
    }
}
