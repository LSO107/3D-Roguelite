using System;

namespace ItemData
{
    [Serializable]
    internal class ConsumableDefinition : ItemDefinition
    {
        public string description;

        public ConsumableDefinition CreateInstance()
        {
            var item = Instantiate(this);
            return item;
        }
    }
}