using System;

namespace Currency
{
    internal sealed class CurrencyDefinition
    {
        public float GoldQuantity { get; private set; }
        public float TokenQuantity { get; private set; }

        public CurrencyDefinition()
        {
            GoldQuantity = 0;
            TokenQuantity = 0;
        }

        public CurrencyDefinition(float goldQuantity, float tokenQuantity)
        {
            GoldQuantity = goldQuantity;
            TokenQuantity = tokenQuantity;

            if (goldQuantity < 0 || tokenQuantity < 0)
                throw new ArgumentException("Currency cannot be less than 0");
        }

        public void AddGold(float quantity)
        {
            GoldQuantity += quantity;
            ClampQuantity();
        }

        public void RemoveGold(float quantity)
        {
            GoldQuantity -= quantity;
            ClampQuantity();
        }

        public void AddTokens(float quantity)
        {
            TokenQuantity += quantity;
            ClampQuantity();
        }

        public void RemoveTokens(float quantity)
        {
            TokenQuantity -= quantity;
            ClampQuantity();
        }

        private void ClampQuantity()
        {
            if (GoldQuantity < 0)
                GoldQuantity = 0;

            if (TokenQuantity < 0)
                TokenQuantity = 0;
        }
    }
}
