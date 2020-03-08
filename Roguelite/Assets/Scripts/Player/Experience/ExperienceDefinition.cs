namespace Player.Experience
{
    internal sealed class ExperienceDefinition
    {
        public float CurrentExperience { get; private set; }
        public float ExperienceRequired { get; private set; }

        private float m_ExperienceMultiplier = 1.5f;

        public ExperienceDefinition(float experienceRequired)
        {
            CurrentExperience = 0;
            ExperienceRequired = experienceRequired;
        }

        public ExperienceDefinition(float currentExperience, float experienceRequired)
        {
            CurrentExperience = currentExperience;
            ExperienceRequired = experienceRequired;
        }

        public void IncreaseExperience(float experience)
        {
            var playerStats = GameManager.Instance.PlayerManager.Stats;

            CurrentExperience += experience;

            if (HasReachedExperienceRequired())
            {
                var remainder = CurrentExperience - ExperienceRequired;
                CurrentExperience = remainder;

                playerStats.IncreaseCombatLevel();
                ExperienceRequired *= (m_ExperienceMultiplier + 0.5f);
            }
        }

        private bool HasReachedExperienceRequired()
        {
            return CurrentExperience >= ExperienceRequired;
        }
    }
}
