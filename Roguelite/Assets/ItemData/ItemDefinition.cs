using System;
using UnityEngine;

namespace ItemData
{
    [Serializable]
    public class ItemDefinition : ScriptableObject
    {
        public string Name;
        public Sprite Icon;
        public GameObject Prefab;
        public int BaseGoldCost;
    }
}
