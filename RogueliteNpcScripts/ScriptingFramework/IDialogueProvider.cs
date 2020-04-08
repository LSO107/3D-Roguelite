

// ReSharper disable UnusedMemberInSuper.Global
// ReSharper disable UnusedMember.Global

namespace ScriptingFramework
{
    public interface IDialogueProvider
    {
        void SendNpcOneLine(string text, int nextState);

        /// <summary>
        /// Ends the chat and closes the active dialogue window
        /// </summary>
        void EndChat();
    }
}
