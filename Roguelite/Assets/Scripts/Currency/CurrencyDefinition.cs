using System;

namespace Currency
{
    internal sealed class CurrencyDefinition
    {
        public float CurrencyQuantity { get; private set; }

        public CurrencyDefinition()
        {
            CurrencyQuantity = 0;
        }

        public CurrencyDefinition(float currencyQuantity)
        {
            CurrencyQuantity = currencyQuantity;

            if (currencyQuantity < 0)
                throw new ArgumentException("Currency cannot be less than 0");
        }

        public void AddCurrency(float quantity)
        {
            CurrencyQuantity += quantity;
            ClampQuantity();
        }

        public void RemoveCurrency(float quantity)
        {
            CurrencyQuantity -= quantity;
            ClampQuantity();
        }

        private void ClampQuantity()
        {
            if (CurrencyQuantity < 0)
                CurrencyQuantity = 0;
        }
    }
}
