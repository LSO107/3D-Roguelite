namespace ScriptingFramework
{
    // ReSharper disable once ClassNeverInstantiated.Global
    public sealed class DialogueOption
    {
        public string Text { get; }
        public int NewState { get; }

        public DialogueOption(string text, int newState)
        {
            Text = text;
            NewState = newState;
        }
    }
}