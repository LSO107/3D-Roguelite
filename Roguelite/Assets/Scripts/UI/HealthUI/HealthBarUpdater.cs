using UnityEngine;
using UnityEngine.UI;

namespace UI.HealthUI
{
    internal sealed class HealthBarUpdater : MonoBehaviour
    {
        [SerializeField] private Slider healthFillBar;

        public void UpdateHealthBar(int currentHealth, int maxHealth)
        {
            healthFillBar.value = (float)currentHealth / maxHealth;
        }
    }
}
