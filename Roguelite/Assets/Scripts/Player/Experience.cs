using Extensions;

namespace Player
{
    internal sealed class Experience
    {
        public float CurrentExperience { get; private set; }
        public float ExperienceRequired { get; private set; }

        private float m_ExperienceMultiplier = 1.5f;

        public Experience(float experienceRequired)
        {
            CurrentExperience = 0;
            ExperienceRequired = experienceRequired;
        }

        public Experience(float currentExperience, float experienceRequired)
        {
            CurrentExperience = currentExperience;
            ExperienceRequired = experienceRequired;
        }

        public void IncreaseExperience(float experience)
        {
            var playerManager = GameManager.Instance.PlayerManager;

            CurrentExperience += experience;

            if (HasReachedExperienceRequired())
            {
                var remainder = CurrentExperience - ExperienceRequired;
                CurrentExperience = remainder;

                playerManager.Stats.IncreaseCombatLevel();
                ExperienceRequired *= (m_ExperienceMultiplier + 0.5f);
            }

            playerManager.ExperienceBarUI.UpdateBarValue(CurrentExperience, ExperienceRequired);
        }

        private bool HasReachedExperienceRequired()
        {
            return CurrentExperience >= ExperienceRequired;
        }
    }
}
