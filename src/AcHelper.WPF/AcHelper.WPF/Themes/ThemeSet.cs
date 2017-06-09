using System;
using System.Collections.Generic;
using System.Windows;

namespace AcHelper.WPF.Themes
{
    public class ThemeSet : List<ResourceDictionary> // Dictionary<Uri, ResourceDictionary>
    {
        public static ThemeSet CreateThemeSet(ResourceDictionary[] resources)
        {
            ThemeSet themeset = new ThemeSet();
            themeset.AddRange(resources);
            //foreach (ResourceDictionary resource in resources)
            //{
            //    themeset.Add(resource.Source, resource);
            //}

            return themeset;
        }
    }
}
