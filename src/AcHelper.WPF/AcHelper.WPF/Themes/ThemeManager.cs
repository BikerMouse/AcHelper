using Autodesk.AutoCAD.ApplicationServices;
using GalaSoft.MvvmLight;
using System;

namespace AcHelper.WPF.Themes
{
    /// <summary>
    /// ThemeHandler will keep track of the AutoCAD theme changes.
    /// </summary>
    public partial class ThemeManager : ObservableObject
    {
        #region Constants ...
        private const string COLORTHEME = "COLORTHEME";
        private const string DARK = "DARK";
        private const string LIGHT = "LIGHT";
        #endregion

        #region Singleton ...
        private static readonly ThemeManager _instance;
        private ThemeManager()
        {
        }
        static ThemeManager()
        {
            _instance = new ThemeManager();
        }

        /// <summary>
        /// Gets the singleton ThemeHandler
        /// </summary>
        /// <returns>ThemeHandler Instance</returns>
        public static ThemeManager GetInstance()
        {
            return _instance;
        }
        #endregion

        #region [           Properties          ]
        #region Current theme ...
        /// <summary>
        /// The <see cref="CurrentTheme" /> property's name.
        /// </summary>
        public const string CurrentThemePropertyName = "CurrentTheme";

        private string _currentTheme = string.Empty;

        /// <summary>
        /// Sets and gets the CurrentTheme property.
        /// Changes to that property's value raise the PropertyChanged event. 
        /// </summary>
        public string CurrentTheme
        {
            get
            {
                if (string.IsNullOrEmpty(_currentTheme))
                {
                    _currentTheme = GetCurrentTheme();
                }
                return _currentTheme;
            }
            set
            {
                Set(CurrentThemePropertyName, ref _currentTheme, value);
            }
        }
        #endregion

        #region Theme tracker ...
        private static bool _isTrackingThemes = false;

        public static bool IsTrackingThemes
        {
            get { return _isTrackingThemes; }
        }
        #endregion
        #endregion

        #region Events ...
        private static void Application_SystemVariableChanged(object sender, SystemVariableChangedEventArgs e)
        {
            if (e.Name == COLORTHEME && e.Changed)
            {
                _instance.CurrentTheme = GetCurrentTheme();
            }
        }
        #endregion

        #region Methods ...
        /// <summary>
        /// Gets the current theme of AutoCAD
        /// This will be recieved through the AutoCAD System variable COLORTHEME.
        /// </summary>
        /// <returns></returns>
        public static string GetCurrentTheme()
        {
            string theme = (short)Autodesk.AutoCAD.ApplicationServices.Core.Application.GetSystemVariable(COLORTHEME) == 0
                    ? DARK
                    : LIGHT;
            return theme;
        }

        public static void TurnOnThemeTracker()
        {
            if (!_isTrackingThemes)
            {
                Autodesk.AutoCAD.ApplicationServices.Core.Application.SystemVariableChanged += Application_SystemVariableChanged;
                _isTrackingThemes = true;
            }
        }
        public static void TurnOffThemeTracker()
        {
            if (_isTrackingThemes)
            {
                Autodesk.AutoCAD.ApplicationServices.Core.Application.SystemVariableChanged -= Application_SystemVariableChanged;
                _isTrackingThemes = false;
            }
        }
        #endregion
    }

    /// <summary>
    /// Event arguments for the Theme Changed event.
    /// </summary>
    public class ThemeChangedEventArgs : EventArgs
    {
        private readonly string _theme;
        /// <summary>
        /// Constructs the ThemeChangedEventArgs.
        /// </summary>
        /// <param name="theme">Dark or Light</param>
        public ThemeChangedEventArgs(string theme)
        {
            _theme = theme;
        }
        /// <summary>
        /// New theme used by AutoCAD.
        /// </summary>
        public string Theme
        {
            get { return _theme; }
        }
    }
}
