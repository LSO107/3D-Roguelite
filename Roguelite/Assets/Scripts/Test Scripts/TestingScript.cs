using Extensions;
using ItemData;
using UnityEngine;
using UnityEngine.UI;

namespace Test_Scripts
{
    internal sealed class TestingScript : MonoBehaviour
    {
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
            /*var generator = new ItemGenerator();
            var item = generator.GenerateEquipmentItem();
            GameManager.Instance.PlayerManager.Inventory.AddItem(item);
            Debug.Log(item.Id);*/
            var generator = new ItemGenerator();
            var item = generator.GeneratePotion();
            GameManager.Instance.PlayerManager.Inventory.AddItem(item);
        }
    }
}
