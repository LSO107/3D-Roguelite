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
            Event.RegisterGuideEvents();

            switch (State)
            {
                case 0:
                    Dialogue.SendOptions(new []{ new DialogueOption("I'm ready to fight!", 2), new DialogueOption("Tell me about the arena", 3), new DialogueOption("Bye", -1) });
                    break;
                case 1:
                    Dialogue.SendNpcOneLine("Are you sure?", 2);
                    break;
                case 2:
                    Event.StartDay();
                    Dialogue.EndChat();
                    break;
                case 3:
                    Dialogue.SendNpcOneLine("Ah, the birthplace of all great warriors, it has been for centuries. " +
                                            "Innumerable fighters come here in hope to become one of the great samurai, but sadly perish... ", 0);
                    break;
                default:
                    Dialogue.EndChat();
                    break;
            }
        }
    }
}
