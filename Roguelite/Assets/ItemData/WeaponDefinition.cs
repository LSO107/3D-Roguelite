using System;
using UnityEngine;

namespace ItemData
{
    [Serializable]
    [CreateAssetMenu(fileName = "New Weapon", menuName = "Item Manager/Weapon")]
    public sealed class WeaponDefinition : EquipmentDefinition
    {
        public WeaponType WeaponType;
    }
}