using ScriptingFramework;

using static ScriptingFramework.Attributes;

namespace RogueliteNpcScripts
{
    [Npc(1)]
    public class PotionShopkeeper : NpcScript
    {
        public override void Execute()
        {
            switch (State)
            {
                case 0:
                    Dialogue.SendNpcOneLine("Hello, welcome to the potion shop", 1);
                    break;
                case 1:
                    Dialogue.SendNpcOneLine("Would you like to see my potions?", 2);
                    break;
                case 2:
                    Interface.OpenPotionShop();
                    Dialogue.EndChat();
                    break;
            }
        }
    }
}
