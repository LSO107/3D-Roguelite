using Items.Consumables;
using Items.Inventory;
using UnityEngine;

public class NewPot : MonoBehaviour
{
    private static PlayerInventory Inventory => GameManager.Instance.PlayerManager.PlayerInventory;

    private void OnCollisionEnter(Collision other)
    {
        if (!other.transform.CompareTag("Player"))
            return;

        var potion = new HealthPotion();
        Inventory.AddItem(potion);
        Destroy(gameObject);
    }
}
