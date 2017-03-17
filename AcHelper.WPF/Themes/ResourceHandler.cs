using System;
using System.Collections.Generic;
using System.Windows;

namespace AcHelper.WPF.Themes
{

    public class ResourceHandler
    {
        public const string THEME_DARK = "Dark";
        public const string THEME_LIGHT = "Light";
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

        private void SecureAppFile()
        {
            Application app = new Application();
            app.ShutdownMode = ShutdownMode.OnExplicitShutdown;
        }
        #endregion

        #region Themesets ...
        private Dictionary<string, ThemeSet> _themeSets = new Dictionary<string, ThemeSet>();

        public Dictionary<string, ThemeSet> ThemeSets
        {
            get { return _themeSets; }
            set { _themeSets = value; }
        }
        #endregion
        #endregion

        public ResourceDictionary GetThemeResourceDictionary(string assemblyName, string theme)
        {
            string themePack = string.Format(THEMEPACK, assemblyName, theme);
            return Application.LoadComponent(new Uri(themePack, UriKind.Relative)) as ResourceDictionary;
        }
    }
}
