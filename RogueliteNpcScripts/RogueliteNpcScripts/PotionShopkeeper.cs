using System;
using System.Collections.Generic;
using System.Linq;
using ScriptingFramework;

using static ScriptingFramework.Attributes;

namespace RogueliteNpcScripts
{
    [Npc(1)]
    public class PotionShopkeeper : NpcScript
    {
        private Random m_Random = new Random();

        private readonly IReadOnlyCollection<string> m_OpeningLines = new[]
        {
            "Welcome to the apothecary!",
            "Hello, you have come to the right place!"
        };

        private readonly IReadOnlyCollection<string> m_Answers = new[]
        {
            "This is the apothecary! I live for potions!",
            "How have you LIVED without an apothecary?!"
        };

        public override void Execute()
        {
            switch (State)
            {
                case 0:
                    Dialogue.SendNpcOneLine(GetRandomLine(m_OpeningLines), 1);
                    break;
                case 1:
                    Dialogue.SendOptions(new [] { new DialogueOption("What potions do you sell?", 2), new DialogueOption("What is this place?", 3), new DialogueOption("Bye", -1),  });
                    break;
                case 2:
                    Dialogue.EndChat();
                    Interface.OpenPotionShop();
                    break;
                case 3:
                    Dialogue.SendNpcOneLine(GetRandomLine(m_Answers), 1);
                    break;
                default:
                    Dialogue.EndChat();
                    break;
            }
        }

        private string GetRandomLine(IReadOnlyCollection<string> collection)
        {
            var randomNumber = m_Random.Next(collection.Count);
            return collection.ElementAt(randomNumber);
        }
    }
}
