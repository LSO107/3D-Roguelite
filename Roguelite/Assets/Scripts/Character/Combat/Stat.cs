using System;
using UnityEngine;

namespace Character.Combat
{
    [Serializable]
    public class Stat
    {
        [SerializeField] private float m_BaseValue;

        public float GetBaseValue()
        {
            return m_BaseValue;
        }
    }
}
