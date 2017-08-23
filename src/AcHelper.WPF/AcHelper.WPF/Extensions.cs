using AcHelper.WPF.Themes;
using System;
using System.Linq;
using System.Windows;
using System.Windows.Controls;

namespace AcHelper.WPF
{
    /// <summary>
    /// 
    /// </summary>
    public static class Extensions
    {
        private readonly static ThemeManager _manager;

        static Extensions()
        {
            _manager = ThemeManager.Current;
        }

        /// <summary>
        /// Applies the given theme to the UserControl
        /// </summary>
        /// <param name="control"></param>
        /// <param name="oldTheme"></param>
        /// <param name="newTheme"></param>
        public static void ApplyTheme(this ContentControl control, string oldTheme, string newTheme)
        {
            if (string.IsNullOrEmpty(newTheme))
            {
                throw new ArgumentNullException("newTheme");
            }
            if (string.Equals(oldTheme, newTheme, StringComparison.CurrentCulture))
            {
                return;
            }

            // Get plugin name from Assembly to get the associated resourcesCollection
            string pluginName = GetAssemblyName(control);
            IPluginResourcesCollection collection = _manager.GetResourceCollection(pluginName);
            ResourceDictionary newPluginTheme = _manager.GetPluginTheme(pluginName, newTheme);

            // Check if new theme isn't accidentally active already
            if ((control.Resources.MergedDictionaries
                .FirstOrDefault(x => Equals(x.Source, newPluginTheme.Source)) != null))
            {
                return;
            }

            // Remove old theme
            if (!string.IsNullOrEmpty(oldTheme))
            {
                ResourceDictionary oldPluginTheme = _manager.GetPluginTheme(pluginName, oldTheme);
                if (control.Resources.MergedDictionaries
                    .FirstOrDefault(x => Equals(x.Source, oldPluginTheme.Source)) is ResourceDictionary dict)
                {
                    control.Resources.MergedDictionaries.Remove(dict);
                }
            }

            // Set new theme
            control.Resources.MergedDictionaries.Add(newPluginTheme);
        }
        /// <summary>
        /// 
        /// </summary>
        /// <param name="control"></param>
        public static void ApplyResources(this ContentControl control)
        {
            // Get plugin name from assembly to get the associated resourcesCollection
            string pluginName = GetAssemblyName(control);
            IPluginResourcesCollection coll = _manager.GetResourceCollection(pluginName);

            foreach (ResourceDictionary dict in coll.Resources)
            {
                control.Resources.MergedDictionaries.Add(dict);
            }
        }

        /// <summary>
        /// Returns the name of the Assembly where the ContentControl is created.
        /// </summary>
        /// <param name="control"></param>
        /// <returns>Name of Assembly.</returns>
        private static string GetAssemblyName(ContentControl control)
        {
            return (control.GetType()).Assembly.GetName().Name;
        }
    }
}
