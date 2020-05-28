using UI.HUD;
using UnityEngine;

namespace Character.Health
{
    internal abstract class HealthObject : MonoBehaviour
    {
        public int CurrentHealth => m_HealthDefinition.CurrentHealth;
        public int MaxHealth => m_HealthDefinition.MaxHealth;
        public int DamageTaken => m_HealthDefinition.DamageTaken;

        protected HealthDefinition m_HealthDefinition;

        public BarUpdater m_HealthBarUpdater;

        protected float NextRegenerationTime;
        protected float RegenerationTime = 10;

        protected abstract void ActionOnDeath();

        protected void Awake()
        {
            m_HealthDefinition = new HealthDefinition(100, 100);
        }

        public void Heal(int amount)
        {
            m_HealthDefinition.Heal(amount);
            m_HealthBarUpdater.UpdateBar(CurrentHealth, MaxHealth);
        }

        public void Damage(int amount)
        {
            
            m_HealthDefinition.Damage(amount);
            m_HealthBarUpdater.UpdateBar(CurrentHealth, MaxHealth);

            if (m_HealthDefinition.IsDead)
            {
                ActionOnDeath();
            }
        }

        protected void RegenerateHealth()
        {
            if (Time.time >= NextRegenerationTime && CurrentHealth < MaxHealth)
            {
                NextRegenerationTime = Time.time + RegenerationTime;
                Heal(1);
            }
        }
    }
}
