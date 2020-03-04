using System;
using System.Runtime.Serialization;

namespace Exceptions
{
    internal sealed class InventoryException : Exception
    {
        public InventoryException()
            : base("Something went wrong during an inventory operation")
        { }

        public InventoryException(string message)
            : base(message)
        { }

        public InventoryException(string message, Exception innerException)
            : base(message, innerException)
        { }

        private InventoryException(SerializationInfo info,
            StreamingContext context)
            : base(info, context)
        { }
    }
}