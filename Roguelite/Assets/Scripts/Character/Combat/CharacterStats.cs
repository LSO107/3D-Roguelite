using Character.Health;
using Player;
using Player.Experience;
using UnityEngine;
using Random = System.Random;

namespace Character.Combat
{
    [RequireComponent(typeof(HealthObject))]
    internal sealed class CharacterStats : MonoBehaviour
    {
        [SerializeField] public SplatMarker splatMarker;
        public int CombatLevel = 1;
        public Stat Damage;
        public Stat Defence;
        public float experienceGiven;

        private EnemyHealth m_HealthObject;

        private Random m_Random;

        private void Awake()
        {
            m_HealthObject = GetComponent<EnemyHealth>();
            m_Random = new Random();
            Damage = new Stat();
            Defence = new Stat();
        }

        private void Start()
        {
            var random1 = m_Random.Next(5, 11);
            var random2 = m_Random.Next(0, 5);

            Damage.SetBaseValue(random1);
            Defence.SetBaseValue(random2);
        }

        public void TakeDamage(int damage)
        {
            var damageRange = m_Random.Next(damage);
            var defenceRange = m_Random.Next(Defence.GetBaseValue() / 2);

            var range = damageRange - defenceRange;
           
            if (range < 0)
                range = 0;

            splatMarker.Show(range);
            m_HealthObject.Damage(range);
        }
    }
}
