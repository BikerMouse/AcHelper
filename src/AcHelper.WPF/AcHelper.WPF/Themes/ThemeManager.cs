using AcHelper.WPF.CAD;
using System.Collections.Generic;
using System.Windows;
using System;
using System.Linq;
using System.Runtime.Serialization;

namespace AcHelper.WPF.Themes
{
    #region BACKUP
    ///// <summary>
    ///// ThemeHandler will keep track of the AutoCAD theme changes.
    ///// </summary>
    //public partial class ThemeManager : ObservableObject
    //{
    //    #region Constants ...
    //    private const string COLORTHEME = "COLORTHEME";
    //    private const string DARK = "DARK";
    //    private const string LIGHT = "LIGHT";
    //    #endregion

    //    #region Singleton ...
    //    private static readonly ThemeManager _instance;
    //    private ThemeManager()
    //    {
    //    }
    //    static ThemeManager()
    //    {
    //        _instance = new ThemeManager();
    //    }

    //    /// <summary>
    //    /// Gets the singleton ThemeHandler
    //    /// </summary>
    //    /// <returns>ThemeHandler Instance</returns>
    //    public static ThemeManager GetInstance()
    //    {
    //        return _instance;
    //    }
    //    #endregion

    //    #region [           Properties          ]
    //    #region Current theme ...
    //    /// <summary>
    //    /// The <see cref="CurrentTheme" /> property's name.
    //    /// </summary>
    //    public const string CurrentThemePropertyName = "CurrentTheme";

    //    private string _currentTheme = string.Empty;

    //    /// <summary>
    //    /// Sets and gets the CurrentTheme property.
    //    /// Changes to that property's value raise the PropertyChanged event. 
    //    /// </summary>
    //    public string CurrentTheme
    //    {
    //        get
    //        {
    //            if (string.IsNullOrEmpty(_currentTheme))
    //            {
    //                _currentTheme = GetCurrentTheme();
    //            }
    //            return _currentTheme;
    //        }
    //        set
    //        {
    //            Set(CurrentThemePropertyName, ref _currentTheme, value);
    //        }
    //    }
    //    #endregion

    //    #region Theme tracker ...
    //    private static bool _isTrackingThemes = false;

    //    public static bool IsTrackingThemes
    //    {
    //        get { return _isTrackingThemes; }
    //    }
    //    #endregion
    //    #endregion

    //    #region Events ...
    //    private static void Application_SystemVariableChanged(object sender, SystemVariableChangedEventArgs e)
    //    {
    //        if (e.Name == COLORTHEME && e.Changed)
    //        {
    //            _instance.CurrentTheme = GetCurrentTheme();
    //        }
    //    }
    //    #endregion

    //    #region Methods ...
    //    /// <summary>
    //    /// Gets the current theme of AutoCAD
    //    /// This will be recieved through the AutoCAD System variable COLORTHEME.
    //    /// </summary>
    //    /// <returns></returns>
    //    public static string GetCurrentTheme()
    //    {
    //        string theme = (short)Autodesk.AutoCAD.ApplicationServices.Core.Application.GetSystemVariable(COLORTHEME) == 0
    //                ? DARK
    //                : LIGHT;
    //        return theme;
    //    }

    //    public static void TurnOnThemeTracker()
    //    {
    //        if (!_isTrackingThemes)
    //        {
    //            Autodesk.AutoCAD.ApplicationServices.Core.Application.SystemVariableChanged += Application_SystemVariableChanged;
    //            _isTrackingThemes = true;
    //        }
    //    }
    //    public static void TurnOffThemeTracker()
    //    {
    //        if (_isTrackingThemes)
    //        {
    //            Autodesk.AutoCAD.ApplicationServices.Core.Application.SystemVariableChanged -= Application_SystemVariableChanged;
    //            _isTrackingThemes = false;
    //        }
    //    }
    //    #endregion
    //}

    ///// <summary>
    ///// Event arguments for the Theme Changed event.
    ///// </summary>
    //public class ThemeChangedEventArgs : EventArgs
    //{
    //    private readonly string _theme;
    //    /// <summary>
    //    /// Constructs the ThemeChangedEventArgs.
    //    /// </summary>
    //    /// <param name="theme">Dark or Light</param>
    //    public ThemeChangedEventArgs(string theme)
    //    {
    //        _theme = theme;
    //    }
    //    /// <summary>
    //    /// New theme used by AutoCAD.
    //    /// </summary>
    //    public string Theme
    //    {
    //        get { return _theme; }
    //    }
    //}
    #endregion

    public class ThemeManager
    {
        static Application _application;
        static ThemeManager _current;
        static ThemeWatcher _watcher;
        readonly Dictionary<string, IPluginResourcesCollection> _resourcesCollection;
        bool _isTrackingCadThemes;

        private ThemeManager()
        {
            _resourcesCollection = new Dictionary<string, IPluginResourcesCollection>();
        }
        static ThemeManager()
        {
            _application = Application.Current ?? new Application() { ShutdownMode = ShutdownMode.OnExplicitShutdown };
            _current = new ThemeManager();
            _watcher = ThemeWatcher.Current;
        }

        public static ThemeManager Current => _current;
        public static Application Application => _application;
        public ThemeWatcher Watcher => _watcher;
        public Dictionary<string, IPluginResourcesCollection> ResourcesCollection => _resourcesCollection;
        public bool IsTrackingCadThemes
        {
            get => _isTrackingCadThemes;
            set => SetTracker(value);
        }

        /// <summary>
        /// Registers a Resource collection of a plugin.
        /// </summary>
        /// <param name="collection"></param>
        /// <returns>True if the collection don't exeist already and can be registered; Otherwise false.</returns>
        public bool RegisterPluginResourcesCollection(IPluginResourcesCollection collection)
        {
            // Only continue if plugin name is not registered yet.
            if (!_resourcesCollection.ContainsKey(collection.Name))
            {
                // Add Locator to the System.Windows.Application.Resources.
                Application.Resources.MergedDictionaries.Add(collection.Locator);
                _resourcesCollection.Add(collection.Name, collection);
                return true;
            }
            return false;
        }

        /// <summary>
        /// Gets the <see cref="PluginResourcesCollection"/>ResourcesSet associated with the given Pluginname.
        /// </summary>
        /// <param name="pluginName">Name of the plugin.</param>
        /// <returns>PluginResourcesSet if the plugin is registered; otherwise it throws a <see cref="PluginResourcesNotRegisteredException"/></returns>
        /// <exception cref="ArgumentNullException"/>
        /// <exception cref="PluginResourcesNotRegisteredException"/>
        public IPluginResourcesCollection GetResourceCollection(string pluginName)
        {
            if (string.IsNullOrEmpty(pluginName))
            {
                throw new ArgumentNullException("pluginName");
            }

            try
            {
                return ResourcesCollection[pluginName] as PluginResourcesCollection;
            }
            catch (KeyNotFoundException)
            {
                throw new PluginResourcesNotRegisteredException(pluginName);
            }
        }
        /// <summary>
        /// Gets a PluginTheme associated with the given Pluginname and Theme name.
        /// </summary>
        /// <param name="pluginName"></param>
        /// <param name="themeName">Name of the theme (Case insensitive)</param>
        /// <returns>PluginTheme if exists; Otherwise it returns null.</returns>
        /// <exception cref="ArgumentNullException"/>
        /// <exception cref="PluginResourcesNotRegisteredException"/>
        public ResourceDictionary GetPluginTheme(string pluginName, string themeName)
        {
            if (string.IsNullOrEmpty(pluginName))
            {
                throw new ArgumentNullException("pluginName");
            }
            if (string.IsNullOrEmpty(themeName))
            {
                throw new ArgumentNullException("themeName");
            }

            IPluginResourcesCollection collection = GetResourceCollection(pluginName);

            return collection.Themes.FirstOrDefault(
                x => string.Equals(x.Key, themeName, StringComparison.InvariantCultureIgnoreCase))
                .Value as ResourceDictionary;
        }
        /// <summary>
        /// Returns all registered theme names associated with the provided plugin name.
        /// </summary>
        /// <param name="pluginName"></param>
        /// <returns></returns>
        public List<string> GetThemes(string pluginName)
        {
            IPluginResourcesCollection collection = GetResourceCollection(pluginName);
            return collection.Themes.Keys.ToList();
        }
        private void SetTracker(bool value)
        {
            if (value)
            {
                Watcher.CadThemeChanged += CadThemeChangedHandler;
            }
            else
            {
                Watcher.CadThemeChanged -= CadThemeChangedHandler;
            }
        }

        private void CadThemeChangedHandler(object sender, CadThemeChangedArgs e)
        {
            if (_isTrackingCadThemes)
            {
                
            }
        }
    }
}
