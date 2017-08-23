using System.Windows;
using System.Windows.Controls;

namespace AcHelper.WPF.Themes
{
    //public  static class PaletteControlExtension
    //{
    //    //private static ThemeManager themeManager = ThemeManager.GetInstance();
    //    private static readonly ResourceHandler resourceHandler = ResourceHandler.GetInstance();

    //    public static void ApplyTheme(this ContentControl control, string theme)
    //    {
    //        ThemeSet resources = resourceHandler.ThemeSets[theme];

    //        control.Resources.MergedDictionaries.Clear();
    //        foreach (ResourceDictionary dict in resources)//.Values)
    //        {
    //            control.Resources.MergedDictionaries.Add(dict);
    //        }
    //    }

    //    #region Theme DependencyProperty
    //    /// <summary>
    //    /// Theme Attached Dependency Property
    //    /// </summary>
    //    public static readonly DependencyProperty ThemeProperty =
    //        DependencyProperty.RegisterAttached(
    //            "Theme"
    //            , typeof(string)
    //            , typeof(PaletteControlExtension),
    //            new FrameworkPropertyMetadata(ThemeManager.GetCurrentTheme(),
    //                new PropertyChangedCallback(OnThemeChanged)));
    //    /// <summary>
    //    /// Gets the Theme property.  This dependency property 
    //    /// indicates ....
    //    /// </summary>
    //    public static string GetTheme(DependencyObject d)
    //    {
    //        return (string)d.GetValue(ThemeProperty);
    //    }
    //    /// <summary>
    //    /// Sets the Theme property.  This dependency property 
    //    /// indicates ....
    //    /// </summary>
    //    public static void SetTheme(DependencyObject d, string value)
    //    {
    //        d.SetValue(ThemeProperty, value);
    //    }
    //    /// <summary>
    //    /// Handles changes to the Theme property.
    //    /// </summary>
    //    public static void OnThemeChanged(DependencyObject d, DependencyPropertyChangedEventArgs e)
    //    {
    //        string theme = e.NewValue as string;
    //        if (theme == string.Empty || theme == null)
    //            return;

    //        ContentControl control = d as ContentControl;
    //        if (control != null)
    //        {
    //            control.ApplyTheme(theme);
    //        }
    //    }
    //    #endregion
    //}

    /// <summary>
    /// 
    /// </summary>
    public static class PaletteThemeExtension
    {
        /// <summary>
        /// The Theme attached property's name.
        /// </summary>
        public const string ThemePropertyName = "Theme";

        /// <summary>
        /// Gets the value of the Theme attached property 
        /// for a given dependency object.
        /// </summary>
        /// <param name="obj">The object for which the property value
        /// is read.</param>
        /// <returns>The value of the Theme property of the specified object.</returns>
        public static string GetTheme(DependencyObject obj)
        {
            return (string)obj.GetValue(ThemeProperty);
        }

        /// <summary>
        /// Sets the value of the Theme attached property
        /// for a given dependency object. 
        /// </summary>
        /// <param name="obj">The object to which the property value
        /// is written.</param>
        /// <param name="value">Sets the Theme value of the specified object.</param>
        public static void SetTheme(DependencyObject obj, string value)
        {
            obj.SetValue(ThemeProperty, value);
        }

        /// <summary>
        /// Identifies the Theme attached property.
        /// </summary>
        public static readonly DependencyProperty ThemeProperty = DependencyProperty.RegisterAttached(
            ThemePropertyName,
            typeof(string),
            typeof(PaletteThemeExtension),
            new FrameworkPropertyMetadata(string.Empty, new PropertyChangedCallback(OnThemeChanged)));

        /// <summary>
        /// 
        /// </summary>
        /// <param name="d"></param>
        /// <param name="e"></param>
        public static void OnThemeChanged(DependencyObject d, DependencyPropertyChangedEventArgs e)
        {
            if (e.NewValue is string newTheme && !string.IsNullOrEmpty(newTheme)
                && d is ContentControl control)
            {
                string oldTheme = e.OldValue as string ?? string.Empty;
                control.ApplyTheme(oldTheme, newTheme);
            }
        }
    }
}
