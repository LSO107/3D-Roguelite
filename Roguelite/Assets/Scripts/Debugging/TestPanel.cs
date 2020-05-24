using Extensions;
using ItemGeneration;
using Items.Definitions;
using Items.Inventory;
using Player;
using UnityEngine;
using Random = System.Random;

internal sealed class TestPanel : MonoBehaviour
{
    private ItemFactory m_ItemFactory;
    private Random m_Random;

    private int m_AttackType = 0;

    private void Start()
    {
        m_ItemFactory = new ItemFactory();
        m_Random = new Random();
    }

    public void Damage()
    {
        //GameManager.Instance.PlayerManager.Health.Damage(10);
        PlayerManager.Instance.PlayerStats.TakeDamage(10);
    }

    public void Heal()
    {
        PlayerManager.Instance.Health.Heal(10);
    }

    public void AddExperience()
    {
        PlayerManager.Instance.Experience.IncreaseExperience(10);
    }

    public void ToggleEquipment()
    {
        var equipment = PlayerManager.Instance.EquipmentUI;
        var canvasGroup = equipment.GetComponent<CanvasGroup>();
        canvasGroup.ToggleCanvasGroup(!canvasGroup.interactable);
    }

    public void AddMoney()
    {
        PlayerManager.Instance.Currency.AddGold(10);
        PlayerManager.Instance.Currency.AddTokens(1);
        Debug.Log(PlayerManager.Instance.Currency.Quantity);
    }

    public void RemoveMoney()
    {
        PlayerManager.Instance.Currency.RemoveGold(10);
        PlayerManager.Instance.Currency.RemoveTokens(1);
        Debug.Log(PlayerManager.Instance.Currency.Quantity);
    }

    public void DropItem()
    {
        var number = m_Random.Next(2);
        Item item;

        if (number == 1)
        {
            item = m_ItemFactory.GenerateEquipmentFromTemplate(EquipmentSlotId.Weapon);
        }
        else
        {
            item = m_ItemFactory.GeneratePotion();
        }
        PlayerManager.Instance.Inventory.AddItem(item);
    }

    public void Attack()
    {
        var anim = PlayerManager.Instance.gameObject.GetComponent<Animator>();
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