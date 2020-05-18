using ScriptingFramework;

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
            throw new System.NotImplementedException();
        }
    }
}
