using System;

namespace AcHelper.Themes
{
    public delegate void ThemeEventHandler(object sender, ThemeChangedEventArgs e);

    /// <summary>
    /// ThemeHandler will keep track of the AutoCAD theme changes.
    /// </summary>
    public class ThemeHandler
    {
        #region Fields ...
        private string _currentTheme;
        #endregion

        #region Singleton ...
        private static readonly ThemeHandler _instance = new ThemeHandler();
        private ThemeHandler() { }
        /// <summary>
        /// Gets the singleton ThemeHandler
        /// </summary>
        /// <returns>ThemeHandler Instance</returns>
        public static ThemeHandler GetInstance()
        {
            return _instance;
        }
        #endregion

        #region Properties ...
        /// <summary>
        /// Sets and gets the current theme.
        /// Fires the ThemeChanged event if the value changes.
        /// </summary>
        public string CurrentTheme
        {
            get { return _currentTheme; }
            set 
            {
                if (_currentTheme != value && value != null)
                {
                    _currentTheme = value;
                    OnThemeChanged(new ThemeChangedEventArgs(_currentTheme));
                }
            }
        }
        #endregion

        #region Events ...
        /// <summary>
        /// Fires when the Current theme changes.
        /// </summary>
        public event ThemeEventHandler ThemeChanged;
        protected virtual void OnThemeChanged(ThemeChangedEventArgs e)
        {
            if (ThemeChanged != null)
            {
                ThemeChanged(this, e);
            }
        }
        #endregion
    }

    public class ThemeChangedEventArgs : EventArgs
    {
        private string _theme;
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
