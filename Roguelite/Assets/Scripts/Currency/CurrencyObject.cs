using TMPro;
using UnityEngine;

namespace Currency
{
    internal sealed class CurrencyObject : MonoBehaviour
    {
        public float Quantity => m_CurrencyDefinition.GoldQuantity;

        private CurrencyDefinition m_CurrencyDefinition;

#pragma warning disable 0649
        [SerializeField] private TextMeshProUGUI m_GoldQuantityText;
        [SerializeField] private TextMeshProUGUI m_TokenQuantityText;
#pragma warning restore 0649

        private void Awake()
        {
            m_CurrencyDefinition = new CurrencyDefinition();
        }

        private void Start()
        {
            m_GoldQuantityText.text = m_CurrencyDefinition.GoldQuantity.ToString();
            m_TokenQuantityText.text = m_CurrencyDefinition.GoldQuantity.ToString();
        }

        public void AddGold(float quantity)
        {
            m_CurrencyDefinition.AddGold(quantity);
            m_GoldQuantityText.text = m_CurrencyDefinition.GoldQuantity.ToString();
        }

        public void RemoveGold(float quantity)
        {
            m_CurrencyDefinition.RemoveGold(quantity);
            m_GoldQuantityText.text = m_CurrencyDefinition.GoldQuantity.ToString();
        }

        public void AddTokens(float quantity)
        {
            m_CurrencyDefinition.AddTokens(quantity);
            m_TokenQuantityText.text = m_CurrencyDefinition.TokenQuantity.ToString();
        }

        public void RemoveTokens(float quantity)
        {
            m_CurrencyDefinition.RemoveTokens(quantity);
            m_TokenQuantityText.text = m_CurrencyDefinition.TokenQuantity.ToString();
        }
    }
}
