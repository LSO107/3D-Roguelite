using UnityEngine;

namespace Items.Definitions
{
    public abstract class Item
    {
        public string Name;
        public Sprite Sprite;

        protected Item(string name, string iconPath)
        {
            Name = name;
            LoadIcon(iconPath);
        }

        protected void LoadIcon(string iconPath)
        {
            var icon = Resources.Load(iconPath);

            if (icon == null)
                Debug.LogError($"Resource did not exist at path {iconPath}");

            Sprite = Resources.Load<Sprite>(iconPath);
        }
    }
}
