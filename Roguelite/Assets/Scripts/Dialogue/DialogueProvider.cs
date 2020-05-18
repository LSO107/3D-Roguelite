using ScriptingFramework;
using System;
using System.Collections.Generic;
using System.Linq;

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
            DialogueSetup.Instance.DisplayContinue(text, nextState);
        }

        public void SendOptions(IEnumerable<DialogueOption> options)
        {
            var optionsList = options.ToList();

            if (optionsList.Count == 2)
            {
                DialogueSetup.Instance.DisplayTwoOptions(optionsList[0], optionsList[1]);
            }

            if (optionsList.Count == 3)
            {
                DialogueSetup.Instance.DisplayThreeOptions(optionsList[0], optionsList[1], optionsList[2]);
            }
        }

        public void EndChat()
        {
            DialogueSetup.Instance.EndDialogue();
        }
    }
}
