using System;
using System.Collections.Generic;

// ReSharper disable ClassNeverInstantiated.Global

namespace ScriptingFramework
{
    public static class Attributes
    {
        [AttributeUsage(AttributeTargets.Class)]
        public class NpcAttribute : Attribute
        {
            public IReadOnlyCollection<int> Ids { get; }

            public NpcAttribute(params int[] ids)
            {
                Ids = ids;
            }
        }
    }
}
