using Items.Consumables;
using Items.Inventory;
using UnityEngine;

public class HealthPot : MonoBehaviour
{
    private static PlayerInventory Inventory => GameManager.Instance.PlayerManager.Inventory;
    private static Vector3 PlayerLocation => GameManager.Instance.PlayerManager.transform.position;

    private float m_CurrentTime;
    private float m_VisibilityTime = 30;

    private void Update()
    {
        m_CurrentTime += Time.deltaTime;

        if (m_CurrentTime >= m_VisibilityTime)
        {
            Destroy(gameObject);
        }
    }

    private void OnMouseDown()
    {
        var distance = Vector3.Distance(transform.position, PlayerLocation);

        if (distance > 2)
            return;

        var healthPotion = new HealthPotion();
        Inventory.AddItem(healthPotion);

        Destroy(gameObject);
    }
}
