using System;
using ScriptingFramework;
using Debug = UnityEngine.Debug;

namespace Dialogue
{
    internal static class ScriptActivator
    {
        public static T CreateScriptInstance<T>(Type npcType, int npcId) where T : Script
        {
            if (!(Activator.CreateInstance(npcType) is Script instance))
            {
                Debug.Log($"Type {npcType} could not be loaded as a script");
                return null;
            }

            if (instance is NpcScript nInstance)
            {
                nInstance.Dialogue = new DialogueProvider(Debug.Log);
                nInstance.Inventory = new ScriptInventory(Debug.Log);
                nInstance.Interface = new ScriptInterfaces();
                //nInstance.Player = new ScriptPlayer(player, scriptName);

                return nInstance as T;
            }

            return null;
        }
    }
}
