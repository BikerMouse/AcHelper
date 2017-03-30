using System.Windows;
using System.Windows.Controls;

namespace AcHelper.WPF.Themes
{
    public  static class PaletteControlExtension
    {
        //private static ThemeManager themeManager = ThemeManager.GetInstance();
        private static readonly ResourceHandler resourceHandler = ResourceHandler.GetInstance();

        public static void ApplyTheme(this ContentControl control, string theme)
        {
            ThemeSet resources = resourceHandler.ThemeSets[theme];

            control.Resources.MergedDictionaries.Clear();
            foreach (ResourceDictionary dict in resources)//.Values)
            {
                control.Resources.MergedDictionaries.Add(dict);
            }
        }

        #region Theme DependencyProperty
        /// <summary>
        /// Theme Attached Dependency Property
        /// </summary>
        public static readonly DependencyProperty ThemeProperty =
            DependencyProperty.RegisterAttached(
                "Theme"
                , typeof(string)
                , typeof(PaletteControlExtension),
                new FrameworkPropertyMetadata(ThemeManager.GetCurrentTheme(),
                    new PropertyChangedCallback(OnThemeChanged)));
        /// <summary>
        /// Gets the Theme property.  This dependency property 
        /// indicates ....
        /// </summary>
        public static string GetTheme(DependencyObject d)
        {
            return (string)d.GetValue(ThemeProperty);
        }
        /// <summary>
        /// Sets the Theme property.  This dependency property 
        /// indicates ....
        /// </summary>
        public static void SetTheme(DependencyObject d, string value)
        {
            d.SetValue(ThemeProperty, value);
        }
        /// <summary>
        /// Handles changes to the Theme property.
        /// </summary>
        public static void OnThemeChanged(DependencyObject d, DependencyPropertyChangedEventArgs e)
        {
            string theme = e.NewValue as string;
            if (theme == string.Empty || theme == null)
                return;

            ContentControl control = d as ContentControl;
            if (control != null)
            {
                control.ApplyTheme(theme);
            }
        }
        #endregion
    }
}
