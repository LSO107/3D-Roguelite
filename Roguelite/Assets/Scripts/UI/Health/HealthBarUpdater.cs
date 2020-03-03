using Health;
using UnityEngine;
using UnityEngine.UI;

namespace UI.Health
{
    internal sealed class HealthBarUpdater : MonoBehaviour
    {
        [SerializeField] private Slider healthFillBar;
        private HealthObject m_HealthObject;

        private void Awake()
        {
            m_HealthObject = GetComponent<HealthObject>();
        }

        private void Update()
        {
            UpdateHealthBar(m_HealthObject.healthDefinition.CurrentHealth, m_HealthObject.healthDefinition.MaxHealth);
        }

        /// <summary>
        /// Set the fill of the slider to be currentHealth / maxHealth
        /// </summary>
        private void UpdateHealthBar(int currentHealth, int maxHealth)
        {
            healthFillBar.value = (float)currentHealth / maxHealth;
            Debug.Log($"Health: {currentHealth} / {maxHealth}");
        }
    }
}
