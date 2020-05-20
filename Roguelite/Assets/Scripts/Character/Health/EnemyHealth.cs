using UI.HUD;
using UnityEngine;

namespace Character.Health
{
    internal sealed class EnemyHealth : HealthObject
    {
        [SerializeField] private GameObject m_HealthBarPrefab;

        private GameObject m_HealthBarInstantiated;

        private Vector3 m_Offset;

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
        }


        protected override void ActionOnDeath()
        {
            Destroy(m_HealthBarInstantiated);
            Destroy(gameObject);
        }
    }
}
