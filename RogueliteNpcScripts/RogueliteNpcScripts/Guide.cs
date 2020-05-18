using System;
using ScriptingFramework;

using static ScriptingFramework.Attributes;

namespace RogueliteNpcScripts
{
    [Npc(3)]
    public class Guide : NpcScript
    {
        public override void Execute()
        {
            switch (State)
            {
                case 0:
                    Dialogue.SendOptions(new []{ new DialogueOption("I want to fight!", 1), new DialogueOption("Tell me about the arena", 2)});
                    break;
                case 1:
                    Event.StartDay();
                    Dialogue.EndChat();
                    break;
                case 2:
                    Dialogue.SendNpcOneLine("No.", -1);
                    break;
                default:
                    Dialogue.EndChat();
                    break;
            }
        }
    }
}
