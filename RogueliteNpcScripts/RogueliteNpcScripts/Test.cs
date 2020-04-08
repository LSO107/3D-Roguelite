using ScriptingFramework;

using static ScriptingFramework.Attributes;

namespace RogueliteNpcScripts
{
    [Npc(0)]
    public class Test : NpcScript
    {
        public override void Execute()
        {
            switch (State)
            {
                case 0:
                    Dialogue.SendNpcOneLine("我说一点汉语", 1);
                    break;
                default:
                    Dialogue.EndChat();
                    break;
            }
        }
    }
}
