using ScriptingFramework;
using System;

namespace Dialogue
{
    internal sealed class DialogueProvider : IDialogueProvider
    {
        private readonly Action<string> m_Log;

        public DialogueProvider(Action<string> log)
        {
            m_Log = log;
        }

        public void SendNpcOneLine(string text, int nextState)
        {
            m_Log(text);
        }

        public void EndChat()
        {
            throw new System.NotImplementedException();
        }
    }
}
