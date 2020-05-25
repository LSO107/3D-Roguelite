using UI.HUD;
using UnityEngine;

namespace Player.Experience
{
    internal sealed class ExperienceObject : MonoBehaviour
    {
        private ExperienceDefinition m_Experience;

        [SerializeField] private BarUpdater m_ExperienceBarUpdater;
        [SerializeField] private float m_ExperienceRequired = 85f;

        public float CurrentExperience => m_Experience.CurrentExperience;
        public float ExperienceRequired => m_Experience.ExperienceRequired;

        private void Start()
        {
            m_Experience = new ExperienceDefinition(m_ExperienceRequired);
        }

        public void IncreaseExperience(float amount)
        {
            m_Experience.IncreaseExperience(amount);
            m_ExperienceBarUpdater.UpdateBar(CurrentExperience, ExperienceRequired);
        }
    }
}
