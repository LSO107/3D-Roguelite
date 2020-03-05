using Items.Consumables;
using Items.Equipables;
using Items.Inventory;
using UnityEngine;

namespace Items
{
    public class NewPot : MonoBehaviour
    {
        private static PlayerInventory Inventory => GameManager.Instance.PlayerManager.PlayerInventory;

        private void OnCollisionEnter(Collision other)
        {
            if (!other.transform.CompareTag("Player"))
                return;

            //var potion = new HealthPotion();
            //Inventory.AddItem(potion);

            var dagger = new SteelDagger();
            Inventory.AddItem(dagger);

            Destroy(gameObject);
        }
    }
}
