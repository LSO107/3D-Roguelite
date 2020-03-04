using Items.Consumables;
using UnityEngine;

public class NewPot : MonoBehaviour
{
    private void OnCollisionEnter(Collision other)
    {
        if (!other.transform.CompareTag("Player"))
            return;

        var potion = new HealthPotion();
        // Add potion to inventory

        //potion.Use();
    }
}
