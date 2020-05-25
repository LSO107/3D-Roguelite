using UI.HUD;
using UnityEngine;

namespace Character.Health
{
    internal sealed class EnemyHealth : HealthObject
    {
        [SerializeField] private GameObject m_HealthBarPrefab = null;

        private GameObject m_HealthBarInstantiated;

        private Vector3 m_Offset;

        [SerializeField] private float m_DamageTimer = 5f;
        private float m_CurrentDamageTimer;

        private void Start()
        {
            m_Offset = new Vector3(0, 2f, 0);
            m_HealthBarInstantiated = Instantiate(m_HealthBarPrefab, transform.position + m_Offset, Quaternion.identity);
            m_HealthBarUpdater = m_HealthBarInstantiated.GetComponentInChildren<BarUpdater>();
        }

        private void Update()
        {
            RegenerateHealth();
            m_HealthBarInstantiated.transform.position = transform.position + m_Offset;
            m_HealthBarInstantiated.transform.LookAt(Camera.main.transform);

            m_CurrentDamageTimer += Time.deltaTime;

            if (m_CurrentDamageTimer >= m_DamageTimer)
            {
                m_HealthDefinition.ResetDamageTaken();
                m_CurrentDamageTimer = 0;
            }
        }

        protected override void ActionOnDeath()
        {
            Destroy(m_HealthBarInstantiated);
            Destroy(gameObject);
        }
    }
}
