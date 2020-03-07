using Extensions;
using Player;
using UnityEngine;
using UnityEngine.UI;

namespace Health
{
    internal sealed class HealthObject : MonoBehaviour
    {
        public int CurrentHealth => m_HealthDefinition.CurrentHealth;
        public int MaxHealth => m_HealthDefinition.MaxHealth;

        private HealthDefinition m_HealthDefinition;
        private Slider m_HealthBar;

        private float m_NextRegenerationTime;
        private float m_RegenerationTime = 10;

        public void Start()
        {
            m_HealthDefinition = new HealthDefinition();
            m_HealthBar = GetComponent<PlayerManager>().HealthBarUI;
        }

        private void Update()
        {
            if (Input.GetKeyDown(KeyCode.O))
            {
                Damage();
            }

            RegenerateHealth();
        }

        public void Damage()
        {
            Damage(10);
        }

        public void Damage(int amount)
        {
            m_HealthDefinition.Damage(amount);
            m_HealthBar.UpdateBarValue(CurrentHealth, MaxHealth);
        }

        public void Heal()
        {
            Heal(10);
        }

        public void Heal(int amount)
        {
            m_HealthDefinition.Heal(amount);
            m_HealthBar.UpdateBarValue(CurrentHealth, MaxHealth);
        }

        /// <summary>
        /// Regenerate the health over time
        /// </summary>
        private void RegenerateHealth()
        {
            // TODO: Prevent regeneration during combat

            if (Time.time >= m_NextRegenerationTime)
            {
                Heal(1);
                m_NextRegenerationTime = Time.time + m_RegenerationTime;
            }
        }

        private void HandleDamage()
        {
            Damage();

            if (m_HealthDefinition.IsDead)
            {
                // Play Death Animation

                // Remove Items & Equipment

                // Reset Player (Health and Location)
            }
        }
    }
}
