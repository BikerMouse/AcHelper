using System.Collections.Generic;
using System.Windows;

namespace AcHelper.WPF.Themes
{
    public interface IPluginResourcesCollection
    {
        string Name { get; }
        IDictionary<string, ResourceDictionary> Themes { get; }
        IList<ResourceDictionary> Resources { get; }
        ResourceDictionary Locator { get; }

        bool AddTheme(string name, string relativeAddress);
        bool AddResource(string relativeAddress);
    }
}
