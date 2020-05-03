using ScriptingFramework;

namespace Dialogue
{
    internal sealed class ScriptInterfaces : IScriptInterfaces
    {
        public void OpenBlacksmithShop()
        {
            DialogueSetup.Instance.OpenBlacksmithShop();
        }

        public void CloseInterfaces()
        {
            throw new System.NotImplementedException();
        }
    }
}
