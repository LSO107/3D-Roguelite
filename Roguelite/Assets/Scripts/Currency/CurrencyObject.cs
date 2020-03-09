using TMPro;
using UnityEngine;

namespace Currency
{
    internal sealed class CurrencyObject : MonoBehaviour
    {
        public float CurrencyQuantity => m_CurrencyDefinition.CurrencyQuantity;

        private CurrencyDefinition m_CurrencyDefinition;

        [SerializeField] private TextMeshProUGUI m_Text;

        private void Start()
        {
            m_CurrencyDefinition = new CurrencyDefinition();
            m_Text.text = m_CurrencyDefinition.CurrencyQuantity.ToString();
        }

        public void AddCurrency(float quantity)
        {
            m_CurrencyDefinition.AddCurrency(quantity);
            m_Text.text = m_CurrencyDefinition.CurrencyQuantity.ToString();
        }

        public void RemoveCurrency(float quantity)
        {
            m_CurrencyDefinition.RemoveCurrency(quantity);
            m_Text.text = m_CurrencyDefinition.CurrencyQuantity.ToString();
        }
    }
}
