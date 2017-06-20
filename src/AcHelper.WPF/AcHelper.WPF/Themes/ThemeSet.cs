using System;
using System.Collections.Generic;
using System.Windows;

namespace AcHelper.WPF.Themes
{
    /// <summary>
    /// 
    /// </summary>
    public class ThemeSet : List<ResourceDictionary> // Dictionary<Uri, ResourceDictionary>
    {
        /// <summary>
        /// 
        /// </summary>
        /// <param name="resources"></param>
        /// <returns></returns>
        public static ThemeSet CreateThemeSet(ResourceDictionary[] resources)
        {
            ThemeSet themeset = new ThemeSet();
            themeset.AddRange(resources);

            return themeset;
        }
    }
}
