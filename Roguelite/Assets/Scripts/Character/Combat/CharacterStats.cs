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
            var random1 = m_Random.Next(0, 11);
            var random2 = m_Random.Next(0, 11);

            Damage.SetBaseValue(random1);
            Defence.SetBaseValue(random2);
        }

        public void TakeDamage()
        {
            var damageRange = m_Random.Next(Damage.GetBaseValue());
            var defenceRange = m_Random.Next(Defence.GetBaseValue());

            var range = (damageRange - defenceRange);
            //var range = m_Random.Next(0, 20);

            if (range < 0)
                range = 0;

            splatMarker.Show(range);
            m_HealthObject.Damage(range);
        }
    }
}
