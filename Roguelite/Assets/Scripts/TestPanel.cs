using Extensions;
using ItemData;
using Items.Inventory;
using UnityEngine;
using Random = System.Random;

internal sealed class TestPanel : MonoBehaviour
{
    private ItemGenerator m_ItemGenerator;
    private Random m_Random;

    private int m_AttackType = 0;

    private void Start()
    {
        m_ItemGenerator = new ItemGenerator();
        m_Random = new Random();
    }

    public void Damage()
    {
        //GameManager.Instance.PlayerManager.Health.Damage(10);
        GameManager.Instance.PlayerManager.PlayerStats.TakeDamage(10);
    }

    public void Heal()
    {
        GameManager.Instance.PlayerManager.Health.Heal(10);
    }

    public void AddExperience()
    {
        GameManager.Instance.PlayerManager.Experience.IncreaseExperience(10);
    }

    public void ToggleEquipment()
    {
        var equipment = GameManager.Instance.PlayerManager.EquipmentUI;
        var canvasGroup = equipment.GetComponent<CanvasGroup>();
        canvasGroup.ToggleCanvasGroup(!canvasGroup.interactable);
    }

    public void AddMoney()
    {
        GameManager.Instance.PlayerManager.Currency.AddGold(10);
        GameManager.Instance.PlayerManager.Currency.AddTokens(1);
        Debug.Log(GameManager.Instance.PlayerManager.Currency.CurrencyQuantity);
    }

    public void RemoveMoney()
    {
        GameManager.Instance.PlayerManager.Currency.RemoveGold(10);
        GameManager.Instance.PlayerManager.Currency.RemoveTokens(1);
        Debug.Log(GameManager.Instance.PlayerManager.Currency.CurrencyQuantity);
    }

    public void DropItem()
    {
        var number = m_Random.Next(2);
        Item item;

        if (number == 1)
        {
            item = m_ItemGenerator.GenerateEquipmentFromTemplate();
            var t = item as Equipment;
            foreach (var stat in t.StatBonuses.Values)
            {
                Debug.Log(stat);
            }
        }
        else
        {
            item = m_ItemGenerator.GeneratePotion();
        }
        GameManager.Instance.PlayerManager.Inventory.AddItem(item);
    }

    public void Attack()
    {
        var anim = GameManager.Instance.PlayerManager.gameObject.GetComponent<Animator>();
        var isAttacking = anim.GetBool("Attack");

        if (isAttacking)
            return;
        
        anim.SetInteger("Attack Type", m_AttackType);
        anim.SetTrigger("Attack");
        m_AttackType++;

        if (m_AttackType >= 5)
            m_AttackType = 0;
    }
}