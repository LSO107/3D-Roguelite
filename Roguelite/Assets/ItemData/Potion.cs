using UnityEngine;

namespace ItemData
{
    [CreateAssetMenu(fileName = "Health Potion", menuName = "Item Manager/Consumable/Potions/Health")]
    internal sealed class Potion : ConsumableDefinition
    {
        public int value;
    }
}