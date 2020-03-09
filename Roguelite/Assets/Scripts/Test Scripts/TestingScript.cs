using Extensions;
using UnityEngine;
using UnityEngine.UI;

namespace Test_Scripts
{
    internal sealed class TestingScript : MonoBehaviour
    {
        [SerializeField] private Button m_DamageHealthButton;
        [SerializeField] private Button m_HealHealthButton;
        [SerializeField] private Button m_ExperienceButton;
        [SerializeField] private Button m_EquipmentButton;
        [SerializeField] private Button m_AddMoneyButton;
        [SerializeField] private Button m_RemoveMoneyButton;


        public void Damage()
        {
            GameManager.Instance.PlayerManager.Health.Damage(10);
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
            GameManager.Instance.PlayerManager.Currency.AddCurrency(10);
            Debug.Log(GameManager.Instance.PlayerManager.Currency.CurrencyQuantity);
        }

        public void RemoveMoney()
        {
            GameManager.Instance.PlayerManager.Currency.RemoveCurrency(10);
            Debug.Log(GameManager.Instance.PlayerManager.Currency.CurrencyQuantity);
        }
    }
}
