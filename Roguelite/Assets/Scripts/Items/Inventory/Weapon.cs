using System.Collections.Generic;
using ItemData;
using Items.Definitions;
using UnityEngine;

namespace Items.Inventory
{
    internal sealed class Weapon : Equipment
    {
        public WeaponType WeaponType { get; }

        public Weapon(string name,
                      Sprite icon,
                      GameObject prefab,
                      int goldCost,
                      EquipmentSlotId slotId,
                      Dictionary<StatBonus, int> statBonuses,
                      int attack,
                      int strength,
                      int defence,
                      int agility,
                      WeaponType weaponType) 
            : base(name,
                   icon,
                   prefab,
                   goldCost,
                   slotId,
                   statBonuses,
                   attack,
                   strength,
                   defence,
                   agility)
        {
            WeaponType = weaponType;
        }
    }
}
