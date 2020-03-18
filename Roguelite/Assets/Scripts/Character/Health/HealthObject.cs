using UI.HUD;
using UnityEngine;

namespace Character.Health
{
    internal sealed class HealthObject : MonoBehaviour
    {
        public int CurrentHealth => m_HealthDefinition.CurrentHealth;
        public int MaxHealth => m_HealthDefinition.MaxHealth;

        private HealthDefinition m_HealthDefinition;

        private Vector3 m_Offset;
        private BarUpdater m_HealthBarUpdater;
        private GameObject m_HealthBarInstantiated;
        [SerializeField] private GameObject m_HealthBarPrefab;

        private float m_NextRegenerationTime;
        private float m_RegenerationTime = 10;

        public void Start()
        {
            m_HealthDefinition = new HealthDefinition();
            m_Offset = new Vector3(0, 2.5f, 0);
            m_HealthBarInstantiated = Instantiate(m_HealthBarPrefab, transform.position + m_Offset, Quaternion.identity);
            m_HealthBarUpdater = m_HealthBarInstantiated.GetComponentInChildren<BarUpdater>();
        }

        private void Update()
        {
            m_HealthBarInstantiated.transform.position = transform.position + m_Offset;

            if (Time.time >= m_NextRegenerationTime && CurrentHealth < MaxHealth)
            {
                RegenerateHealth();
            }
        }

        public void Damage(int amount)
        {
            m_HealthDefinition.Damage(amount);
            m_HealthBarUpdater.UpdateBar(CurrentHealth, MaxHealth);
        }

        public void Heal(int amount)
        {
            m_HealthDefinition.Heal(amount);
            m_HealthBarUpdater.UpdateBar(CurrentHealth, MaxHealth);
        }

        private void RegenerateHealth()
        {
            // TODO: Prevent regeneration during combat

            Heal(1);
            m_NextRegenerationTime = Time.time + m_RegenerationTime;
        }

        private void HandlePlayerDeath()
        {
            if (m_HealthDefinition.IsDead)
            {
                // Play Death Animation

                // Remove Items & Equipment

                // Reset Player (Health and Location)
            }
        }
    }
}
