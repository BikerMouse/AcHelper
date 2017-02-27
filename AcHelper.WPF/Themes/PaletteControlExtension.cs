using System;
using System.Reflection;
using System.Windows;
using System.Windows.Controls;

namespace AcHelper.WPF.Themes
{
    public  static class PaletteControlExtension
    {
        private const string LIGHTTHEME = "Light";
        private const string DARKTHEME = "Dark";
        private const string THEMEPACK = @"{0};component/Resources/{1}Theme.xaml";

        public static ResourceDictionary GetThemeResourceDictionary(string assemblyName, string theme)
        {
            if (theme != null)
            {
                string packUri = string.Format(THEMEPACK, assemblyName, theme);
                return Application.LoadComponent(new Uri(packUri, UriKind.Relative)) as ResourceDictionary;
            }
            return null;
        }

        public static void ApplyTheme(this Application app, string theme)
        {
            Assembly assembly = Assembly.GetAssembly(app.GetType());
            ResourceDictionary dictionary = GetThemeResourceDictionary(assembly.GetName().Name, theme);

            if (dictionary != null)
            {
                app.Resources.MergedDictionaries.Clear();
                app.Resources.MergedDictionaries.Add(dictionary);
            }
        }
        public static void ApplyTheme(this ContentControl control, string theme)
        {
            Assembly assembly = Assembly.GetAssembly(control.GetType());
            ResourceDictionary dictionary = GetThemeResourceDictionary(assembly.GetName().Name, theme);

            if (dictionary != null)
            {
                control.Resources.MergedDictionaries.Clear();
                control.Resources.MergedDictionaries.Add(dictionary);
            }
        }

        public static void AddResourceDictionary(this Application app, string assembly, string dict)
        {
            ResourceDictionary res = Application.LoadComponent(new Uri(dict, UriKind.Relative)) as ResourceDictionary;

            if (res != null)
            {
                app.Resources.MergedDictionaries.Add(res);
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
