using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Windows;

namespace AcHelper.WPF.Themes
{
    public class PluginResourcesCollection : IPluginResourcesCollection
    {
        private readonly string _name;
        private readonly IDictionary<string, ResourceDictionary> _themePacks;
        private readonly IList<ResourceDictionary> _resourcePacks;
        private readonly ResourceDictionary _locatorPack;

        /// <summary>
        /// Constructs an instance of <see cref="IPluginResourcesCollection"/>.
        /// </summary>
        /// <param name="resources">List of relative addresses of the resources. 
        /// </param>
        /// <param name="themes">Dictionary of themenames and relative addresses of the themes.</param>
        /// <param name="locator">Relative address of the ResourceDictionary containing the ModelViewLocator source.</param>
        /// 
        /// <remarks>
        /// Format relative address: /[assembly];component/[namespace].[filename].xaml
        /// </remarks>
        public PluginResourcesCollection(List<ResourceDictionary> resources, Dictionary<string, ResourceDictionary> themes, ResourceDictionary locator)
        {
            string pluginName = Assembly.GetCallingAssembly().GetName().Name;
            if (string.IsNullOrEmpty(pluginName))
            {
                throw new ArgumentNullException("pluginName");
            }
            _name = pluginName;
            _resourcePacks = resources ?? throw new ArgumentNullException("resources");
            _themePacks = themes ?? throw new ArgumentNullException("themes");
            _locatorPack = locator;
        }

        /// <summary>
        /// Gets the name of the plugin associated to these resources.
        /// </summary>
        public string PluginName => _name;
        /// <summary>
        /// Gets a dictionary with all registered theme resource dictionaries.
        /// </summary>
        public IDictionary<string, ResourceDictionary> Themes => _themePacks;
        /// <summary>
        /// Gets a list with al resources.
        /// </summary>
        public IList<ResourceDictionary> Resources => _resourcePacks;
        /// <summary>
        /// Gets the resource dictionary with the ModelViewLocator.
        /// </summary>
        public ResourceDictionary Locator => _locatorPack;

        /// <summary>
        /// Adds a resource dictionary to the plugin resource collection.
        /// </summary>
        /// <param name="relativeAddress">Relative address to the resource dictionary.</param>
        /// <returns>True if the resource dictionary didn't exist already and is successfuly added; Otherwise, false.</returns>
        public bool AddResource(string relativeAddress)
        {
            ResourceDictionary dict = CreateResourceDictionary(relativeAddress);

            // Only add when the resource dictionary doesn't exist yet.
            if (!(Resources.FirstOrDefault(x => Equals(x.Source, dict.Source)) is ResourceDictionary))
            {
                Resources.Add(dict);
                return true;
            }
            return false;
        }
        /// <summary>
        /// Adds a Theme resource dictionary to the plugin resource collection.
        /// </summary>
        /// <param name="name">Name of the theme.</param>
        /// <param name="relativeAddress">relative address to the resource dictionary.</param>
        /// <returns>True if the resource dictionary didn't exist already and is successfuly added; Otherwise, false.</returns>
        public bool AddTheme(string name, string relativeAddress)
        {
            if (string.IsNullOrEmpty(name))
            {
                throw new ArgumentNullException("name");
            }

            if (!Themes.ContainsKey(name))
            {
                Themes.Add(name, CreateResourceDictionary(relativeAddress));
                return true;
            }
            return false;
        }

        public static ResourceDictionary CreateResourceDictionary(string relativeAddress)
        {
            Assembly caller = Assembly.GetCallingAssembly();
            string name = caller.GetName().Name;
            string PACKFORMAT = string.Concat("pack://application:,,,/", name, ";component");

            if (string.IsNullOrEmpty(relativeAddress))
            {
                throw new ArgumentNullException("relativeAddress");
            }
            if (!relativeAddress.StartsWith("/"))
            {
                relativeAddress = "/" + relativeAddress;
            }

            string pack = string.Concat(PACKFORMAT, relativeAddress);
            Uri uri = new Uri(pack, UriKind.RelativeOrAbsolute);

            return new ResourceDictionary { Source = uri };
        }
    }
}
