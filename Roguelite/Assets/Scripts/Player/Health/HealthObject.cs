using UI.Player.Health;
using UnityEngine;

namespace Player.Health
{
    [RequireComponent(typeof(HealthBarUpdater))]
    internal sealed class HealthObject : MonoBehaviour
    {
        public HealthDefinition healthDefinition;

        private float m_NextRegenerationTime;
        private float m_RegenerationTime = 10;

        public void Start()
        {
            healthDefinition = new HealthDefinition();
        }

        private void Update()
        {
            if (Input.GetKeyDown(KeyCode.A))
            {
                healthDefinition.Damage();
            }

            RegenerateHealth();
        }

        /// <summary>
        /// Regenerate the health over time
        /// </summary>
        private void RegenerateHealth()
        {
            // TODO: Prevent regeneration during combat

            if (Time.time >= m_NextRegenerationTime)
            {
                healthDefinition.Heal(1);
                m_NextRegenerationTime = Time.time + m_RegenerationTime;
            }
        }

        private void HandleDamage()
        {
            healthDefinition.Damage();

            if (healthDefinition.IsDead)
            {
                // Play Death Animation

                // Remove Items & Equipment

                // Reset Player (Health and Location)
            }
        }
    }
}
