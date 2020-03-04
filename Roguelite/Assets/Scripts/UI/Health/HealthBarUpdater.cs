using Health;
using UnityEngine;
using UnityEngine.UI;

namespace UI.Health
{
    internal sealed class HealthBarUpdater : MonoBehaviour
    {
        [SerializeField] private Slider healthFillBar;

        /// <summary>
        /// Set the fill of the slider to be currentHealth / maxHealth
        /// </summary>
        public void UpdateHealthBar(int currentHealth, int maxHealth)
        {
            healthFillBar.value = (float)currentHealth / maxHealth;
            Debug.Log($"Health: {currentHealth} / {maxHealth}");
        }
    }
}
