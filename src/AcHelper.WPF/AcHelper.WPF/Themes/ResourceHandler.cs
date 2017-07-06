using System;
using System.Collections.Generic;
using System.Windows;

namespace AcHelper.WPF.Themes
{

    public class ResourceHandler
    {
        public const string THEME_DARK = "DARK";
        public const string THEME_LIGHT = "LIGHT";
        private const string THEMEPACK = @"{0};component/Themes/{1}Theme.xaml";
        private const string GENERICPACK = @"{0};component/Themes/Generic.xaml";

        #region Singleton ...
        private ResourceHandler() { }
        static ResourceHandler()
        {
            _instance = new ResourceHandler();
        }
        private static ResourceHandler _instance;
        public static ResourceHandler GetInstance()
        {
            return _instance;
        }
        #endregion

        #region [           Properties          ]
        #region Application File ...
        [Obsolete("Use WpfFile instead.")]
        public Application App
        {
            get
            {
                if (Application.Current == null)
                {
                    SecureAppFile();
                }
                return Application.Current;
            }
        }
        [Obsolete("Use SecureWpfAppFile instead.")]
        private void SecureAppFile()
        {
            Application app = new Application();
            app.ShutdownMode = ShutdownMode.OnExplicitShutdown;
        }

        public Application WpfApp
        {
            get => Application.Current ?? SecureWpfAppFile();
        }
        private Application SecureWpfAppFile()
        {
            Application app = new Application();
            app.ShutdownMode = ShutdownMode.OnExplicitShutdown;
            return app;
        }
        #endregion

        #region Themesets ...
        private Dictionary<string, ThemeSet> _themeSets = new Dictionary<string, ThemeSet>();

        public Dictionary<string, ThemeSet> ThemeSets
        {
            get => _themeSets;
            set => _themeSets = value;
        }
        #endregion
        #endregion

        public ResourceDictionary GetThemeResourceDictionary(string assemblyName, string theme)
        {
            string themePack = string.Format(THEMEPACK, assemblyName, theme);
            Uri source = new Uri(themePack, UriKind.Relative);
            return Application.LoadComponent(source) as ResourceDictionary;
        }

        public bool SetGenericResourceDictionary(string assemblyName)
        {
            string genericPack = string.Format(GENERICPACK, assemblyName);
            try
            {
                ResourceDictionary dict = Application.LoadComponent(new Uri(genericPack, UriKind.Relative)) as ResourceDictionary;
                App.Resources.MergedDictionaries.Clear();
                App.Resources.MergedDictionaries.Add(dict);
                return true;
            }
            catch (Exception) { }

            return false;
        }
    }
}
