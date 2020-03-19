using System;

namespace Character.Combat
{
    [Serializable]
    public class Stat
    { 
        private int m_BaseValue;

        public void SetBaseValue(int value)
        {
            m_BaseValue = value;
        }

        public float GetBaseValue()
        {
            return m_BaseValue;
        }
    }
}
