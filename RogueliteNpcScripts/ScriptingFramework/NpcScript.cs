namespace ScriptingFramework
{
    public abstract class NpcScript : Script
    {
        public IDialogueProvider Dialogue { get; set; }

        public IEventProvider Event { get; set; }

        public IScriptInterfaces Interface { get; set; }

        public IScriptInventory Inventory { get; set; }
        
        public IScriptPlayer Player { get; set; }

        public int State = 0;
        public int NpcId;
    }
}
