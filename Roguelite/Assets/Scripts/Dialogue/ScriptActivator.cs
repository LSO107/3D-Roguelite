using System;
using Player;
using ScriptingFramework;
using Debug = UnityEngine.Debug;

namespace Dialogue
{
    internal static class ScriptActivator
    {
        public static Script CreateScriptInstance(Type npcType, PlayerManager player, int npcId)
        {
            if (!(Activator.CreateInstance(npcType) is Script instance))
            {
                Debug.Log($"Type {npcType} could not be loaded as a script");
                return null;
            }

            if (instance is NpcScript nInstance)
            {
                nInstance.Dialogue = new DialogueProvider(Debug.Log);
                //nInstance.Inventory = new ScriptInventory(player, scriptName);
                //nInstance.Player = new ScriptPlayer(player, scriptName);
            }

            return instance;
        }
    }
}
