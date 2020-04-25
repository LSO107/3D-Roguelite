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
                    Dialogue.SendNpcOneLine("HELLO, ARE YOU GOOD AT FIGHT?", 1);
                    break;
                case 1:
                    Dialogue.SendOptions(new [] { new DialogueOption("YES", 2), new DialogueOption("NO", 3),  });
                    break;
                case 2:
                    Dialogue.SendNpcOneLine("OK SO YOU ARE GOOD AT FIGHT, YES", 0);
                    break;
                case 3:
                    Dialogue.SendNpcOneLine("AH YOU ARE SHIT AT FIGHT?", 0);
                    break;
                default:
                    Dialogue.EndChat();
                    break;
            }
        }
    }
}
