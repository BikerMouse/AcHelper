using System;

namespace AcHelper.Extra
{
    [Serializable]
    public class RegistryHandlerException : Exception
    {
        private const string MESSAGE = "An unexpected error occured while reading the AutoCAD Registry hive.";
        public RegistryHandlerException() : base(MESSAGE) { }
        public RegistryHandlerException(string message) : base(message) { }
        public RegistryHandlerException(string message, Exception inner) : base(message, inner) { }
        protected RegistryHandlerException(
          System.Runtime.Serialization.SerializationInfo info,
          System.Runtime.Serialization.StreamingContext context) : base(info, context) { }
    }
}
