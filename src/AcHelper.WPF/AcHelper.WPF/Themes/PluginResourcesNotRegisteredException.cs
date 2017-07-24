using System;

namespace AcHelper.WPF.Themes
{
    [Serializable]
    internal class PluginResourcesNotRegisteredException : ArgumentNullException
    {
        private const string MESSAGE = "Resources of plugin '{0}' are not registered.";
        public PluginResourcesNotRegisteredException() { }
        public PluginResourcesNotRegisteredException(string pluginName) : base(string.Format(MESSAGE, pluginName)) { }
        public PluginResourcesNotRegisteredException(string message, Exception inner) : base(message, inner) { }
        protected PluginResourcesNotRegisteredException(
          System.Runtime.Serialization.SerializationInfo info,
          System.Runtime.Serialization.StreamingContext context) : base(info, context) { }
    }
}
