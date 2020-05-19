using System;
using System.Collections.Generic;
using System.Linq;
using ScriptingFramework;

using static ScriptingFramework.Attributes;

namespace RogueliteNpcScripts
{
    [Npc(0)]
    public class BlacksmithShopkeeper : NpcScript
    {
        private Random m_Random = new Random();

        private readonly IReadOnlyCollection<string> m_OpeningLines = new[]
        {
            "Welcome to my blacksmith.",
            "Hello, I'm the blacksmith."
        };

        private readonly IReadOnlyCollection<string> m_Answers = new[]
        {
            "Not bad, the work keeps me sharp.",
            "Fine."
        };

        public override void Execute()
        {
            switch (State)
            {
                case 0:
                    Dialogue.SendNpcOneLine(GetRandomLine(m_OpeningLines), 1);
                    break;
                case 1:
                    Dialogue.SendOptions(new [] { new DialogueOption("Can I see your shop?", 3), new DialogueOption("How is life?", 2), new DialogueOption("Bye", -1) });
                    break;
                case 2:
                    Dialogue.SendNpcOneLine(GetRandomLine(m_Answers), 1);
                    break;
                case 3:
                    Dialogue.EndChat();
                    Interface.OpenBlacksmithShop();
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
