using UnityEngine;

namespace Character.Health
{
    internal sealed class PlayerHealth : HealthObject
    {
        private Animator m_Anim;
        private static readonly int Death = Animator.StringToHash("Death");

        private void Start()
        {
            m_Anim = GetComponent<Animator>();
            UpdateHealthBar();
        }

        private void Update()
        {
            RegenerateHealth();
        }

        protected override void ActionOnDeath()
        {
            // Lose items?
            // Particle effect or inventory flash effect etc?

            m_HealthDefinition.ResetHealth();

            if (m_Anim.GetCurrentAnimatorStateInfo(2).IsName("Death"))
                m_Anim.ResetTrigger(Death);

            m_Anim.SetTrigger(Death);
        }

        public void UpdateHealthBar()
        {
            m_HealthBarUpdater.UpdateBar(CurrentHealth, MaxHealth);
        }
    }
}
