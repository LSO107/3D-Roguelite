using Character.Combat;
using ItemData;
using ItemGeneration;
using Items;
using Items.Definitions;
using Items.Inventory;
using Player;
using UI.HUD;
using UnityEngine;
using Random = System.Random;

namespace Character.Health
{
    internal sealed class EnemyHealth : HealthObject
    {
        [SerializeField] private GameObject m_HealthBarPrefab;

        private GameObject m_HealthBarInstantiated;

        private Vector3 m_Offset;

        [SerializeField] private float m_DamageTimer = 5f;
        private float m_CurrentDamageTimer;

        private Random m_Random;
        private ItemFactory m_ItemFactory;

        private void Start()
        {
            m_Random = new Random();
            m_ItemFactory = new ItemFactory();
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

        public void Destruct()
        {
            Destroy(m_HealthBarInstantiated);
            Destroy(gameObject);
        }

        protected override void ActionOnDeath()
        {
            var randomGold = m_Random.Next(0, 10);
            PlayerManager.Instance.Currency.AddGold(randomGold);
            NpcFight.Instance.RemoveCurrentNpc(gameObject);
            GameManager.Instance.InstantiatePuff(transform.position);
            PlayerManager.Instance.Experience.IncreaseExperience(GetComponent<CharacterStats>().CombatLevel * 125);
            InstantiateRandomItem();
            Destroy(m_HealthBarInstantiated);
            Destroy(gameObject);
        }

        /// <summary>
        /// Creates and registers a new item
        /// </summary>
        private void InstantiateRandomItem()
        {
            var item = GetRandomItem();
            var droppedItem = Instantiate(item.Prefab, transform.position + new Vector3(0, 0.1f, 0), item.Prefab.transform.rotation);
            droppedItem.GetComponentInChildren<GroundItem>().RegisterGroundItem(item.Id);
            GroundItemManager.Instance.AddItem(item);
        }

        private Item GetRandomItem()
        {
            var number = m_Random.Next(0, 4);
            var slotId = (EquipmentSlotId) number;
            return m_ItemFactory.GenerateEquipmentFromTemplate(slotId);
        }
    }
}
