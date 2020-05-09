using System;
using System.Collections.Generic;
using System.IO;
using System.Reflection;
using ScriptingFramework;
using UnityEngine;

namespace Dialogue
{
    public class DialogueReader : MonoBehaviour
    {
        private readonly Dictionary<int, Type> NpcScripts = new Dictionary<int, Type>();

        private void Awake()
        {
            const string scriptsPath = "Assets/Scripts/Dialogue/RogueliteNpcScripts.dll";
            if (!File.Exists(scriptsPath))
            { 
                Debug.LogError("Could not find RogueliteNpcScripts.dll");
                return;
            }

            var bytes = File.ReadAllBytes(scriptsPath);

            var scriptAssembly = Assembly.Load(bytes);
            var types = scriptAssembly.GetExportedTypes();

            foreach (var t in types)
            {
                if (t.IsSubclassOf(typeof(NpcScript)))
                {
                    var ids = t.GetCustomAttribute<Attributes.NpcAttribute>(true).Ids;

                    foreach (var id in ids)
                    {
                        NpcScripts.Add(id, t);
                    }
                }
                else
                {
                    Console.WriteLine($"We found a {t}, {t.BaseType}");
                }
            }
        }

        public Type GetNpcType(int id)
        {
            return NpcScripts.ContainsKey(id)
                ? NpcScripts[id]
                : null;
        }
    }
}
