
using System.Collections.Generic;

namespace ScriptingFramework
{
    public interface IDialogueProvider
    {
        void SendNpcOneLine(string text, int nextState);

        void SendOptions(IEnumerable<DialogueOption> options);

        /// <summary>
        /// Ends the chat and closes the active dialogue window
        /// </summary>
        void EndChat();
    }
}
