using System;
using Currency;
using Player;
using ScriptingFramework;

namespace Dialogue
{
    internal sealed class ScriptInventory : IScriptInventory
    {
        private readonly Action<string> m_Log;

        public ScriptInventory(Action<string> log)
        {
            m_Log = log;
        }

        public float GetGold()
        {
            //return GameManager.Instance.PlayerManager.Currency.CurrencyQuantity;
            return 0;
        }
    }
}
