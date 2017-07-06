using System;
using System.Diagnostics;
using System.Windows;

namespace AcHelper.WPF.Themes
{
    /// <summary>
    /// Represents the Theme of the application
    /// </summary>
    [DebuggerDisplay("apptheme={Name}, res={Resources.Source}")]
    public class AppTheme
    {
        /// <summary>
        /// Gets the ResourceDictionary that represents this application theme.
        /// </summary>
        public ResourceDictionary Resources { get; private set; }

        /// <summary>
        /// Gets the name of the application Theme.
        /// </summary>
        public string Name { get; private set; }

        /// <summary>
        /// Creates an AppTheme
        /// </summary>
        /// <param name="name">Name of the theme.</param>
        /// <param name="resourceAddress">address to the resourceDictionary representing the theme.</param>
        public AppTheme(string name, Uri resourceAddress)
        {
            if (resourceAddress == null)
            {
                throw new ArgumentNullException("resourceAddress");
            }

            Name = name ?? throw new ArgumentNullException("name");
            Resources = new ResourceDictionary { Source = resourceAddress };
        }
    }
}
