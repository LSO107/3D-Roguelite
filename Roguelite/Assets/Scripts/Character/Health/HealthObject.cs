using UI.HUD;
using UnityEngine;

namespace Character.Health
{
    internal abstract class HealthObject : MonoBehaviour
    {
        public int CurrentHealth => m_HealthDefinition.CurrentHealth;
        public int MaxHealth => m_HealthDefinition.MaxHealth;

        protected HealthDefinition m_HealthDefinition;

        [SerializeField] 
        protected BarUpdater m_HealthBarUpdater;

        protected float m_NextRegenerationTime;
        protected float m_RegenerationTime = 10;

        protected abstract void ActionOnDeath();

        protected void Awake()
        {
            m_HealthDefinition = new HealthDefinition();
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
            if (Time.time >= m_NextRegenerationTime && CurrentHealth < MaxHealth)
            {
                m_NextRegenerationTime = Time.time + m_RegenerationTime;
                Heal(1);
            }
        }
    }
}
