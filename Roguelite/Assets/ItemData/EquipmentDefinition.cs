using System;
using System.Collections.Generic;
using System.Linq;
using Items.Definitions;
using UnityEngine;

namespace ItemData
{
    [Serializable]
    [CreateAssetMenu(fileName = "New Equipment", menuName = "Item Manager/Equipment")]
    public class EquipmentDefinition : ItemDefinition
    {
        public EquipmentSlotId EquipmentSlotId;
        public Dictionary<StatBonus, int> StatBonuses;

        [SerializeField] public int m_Attack;
        [SerializeField] public int m_Strength;
        [SerializeField] public int m_Defence;
        [SerializeField] public int m_Agility;

        public void OnEnable()
        {
            StatBonuses = new Dictionary<StatBonus, int>
            {
                { StatBonus.Attack, m_Attack },
                { StatBonus.Strength, m_Strength },
                { StatBonus.Defence, m_Defence },
                { StatBonus.Agility, m_Agility }
            };
        }

        public EquipmentDefinition CreateInstance()
        {
            var item = Instantiate(this);
            return item;
        }

        public IEnumerable<StatBonus> GetStatBonuses()
        {
            return StatBonuses.Where(d => d.Value > 0)
                              .Select(d => d.Key);
        }
    }
}