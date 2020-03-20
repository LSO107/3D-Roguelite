using System;
using UnityEngine;

namespace Character.Combat
{
    [Serializable]
    public class Stat
    { 
        [SerializeField] private int m_BaseValue;

        public void SetBaseValue(int value)
        {
            m_BaseValue = value;
        }

        public int GetBaseValue()
        {
            return m_BaseValue;
        }
    }
}
