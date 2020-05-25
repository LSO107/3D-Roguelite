using ScriptingFramework;
using UI.DialogueUI;

namespace Dialogue
{
    internal sealed class ScriptInterfaces : IScriptInterfaces
    {
        public void OpenBlacksmithShop()
        {
            DialogueSetup.Instance.OpenBlacksmithShop();
        }

        public void OpenPotionShop()
        {
            DialogueSetup.Instance.OpenPotionShop();
        }

        public void CloseInterfaces()
        {
            DialogueSetup.Instance.CloseShops();
        }
    }
}
