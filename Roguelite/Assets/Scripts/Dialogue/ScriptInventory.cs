using System;
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
            return PlayerManager.Instance.Currency.Quantity;
        }
    }
}
