using ScriptingFramework;

using static ScriptingFramework.Attributes;

namespace RogueliteNpcScripts
{
    [Npc(1)]
    public class Test1 : NpcScript
    {
        public override void Execute()
        {
            switch (State)
            {
                case 0:
                    Dialogue.SendNpcOneLine("Hello", 1);
                    break;
                case 1:
                    Dialogue.SendNpcOneLine("My famry are dead", 0);
                    break;
                default:
                    Dialogue.EndChat();
                    break;
            }
        }
    }
}
