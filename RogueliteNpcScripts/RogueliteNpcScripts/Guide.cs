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
                    Dialogue.SendOptions(new []{ 
                        new DialogueOption("I'm ready to fight!", 2), 
                        new DialogueOption("Tell me about the arena", 3), 
                        new DialogueOption("Teach me the basics", 4), 
                        new DialogueOption("Bye", -1) });
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
                case 4:
                    Dialogue.SendOptions(new []
                    {
                        new DialogueOption("Navigation", 5),
                        new DialogueOption("Inventory", 6),
                        new DialogueOption("Fighting", 7),
                        new DialogueOption("That's all, thanks", 0) 
                    });
                    break;
                case 5:
                    Dialogue.SendNpcOneLine("Navigate using WASD keys. Use the F key when prompted to interact with people and objects," +
                                            "for example, doors. Why not give the Apothecary or Blacksmith a try during out-of-combat hours?", 4);
                    break;
                case 6:
                    Dialogue.SendNpcOneLine("Use the ALT key to enable input to interact with items, clicking on items on the ground will pick them up." +
                                            "Left-click items in the inventory to use them quickly, right-click to see available options.", 4);
                    break;
                case 7:
                    Dialogue.SendNpcOneLine("Talk to me when you are ready to begin fighting, I advise you visit the stores and invest in some equipment. " +
                                            "Fighting will grant you experience and prepare you for the worst. Remember... the greatest foes will drop the greatest loot.", 4);
                    break;
                default:
                    Dialogue.EndChat();
                    break;
            }
        }
    }
}
