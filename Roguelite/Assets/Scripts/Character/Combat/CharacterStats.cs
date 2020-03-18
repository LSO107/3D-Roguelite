using Character.Health;
using UnityEngine;

namespace Character.Combat
{
    [RequireComponent(typeof(HealthObject))]
    internal sealed class CharacterStats : MonoBehaviour
    {
        [SerializeField] private Stat m_Damage;
        [SerializeField] private Stat m_Armour;

        private HealthObject m_HealthObject;

        public void Awake()
        {
            m_HealthObject = GetComponent<HealthObject>();
        }

        public void TakeDamage(int damage)
        {
            m_HealthObject.Damage(damage);
            Debug.Log($"Character took {damage} amount of damage.");

            if (m_HealthObject.CurrentHealth <= 0)
            {
                Debug.Log("Character dead.");
            }
        }
    }
}
