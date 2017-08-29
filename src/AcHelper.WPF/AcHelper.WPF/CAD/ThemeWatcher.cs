using System;
using System.Windows;

namespace AcHelper.WPF.CAD
{
    /// <summary>
    /// Handles the event of the changing ColorTheme in AutoCAD.
    /// </summary>
    /// <param name="sender"></param>
    /// <param name="e"></param>
    public delegate void CadThemeChangedHandler(object sender, CadThemeChangedArgs e);

    /// <summary>
    /// Keeps track of the AutoCAD ColorTheme
    /// </summary>
    public class ThemeWatcher
    {
        const string DARK = "DARK";
        const string LIGHT = "LIGHT";
        const string COLORTHEME = "COLORTHEME";

        private static ThemeWatcher _current;
        private string _name;

        private ThemeWatcher()
        {
            _name = GetColorThemeVariable();
            Autodesk.AutoCAD.ApplicationServices.Application.SystemVariableChanged += new Autodesk.AutoCAD.ApplicationServices.SystemVariableChangedEventHandler(SystemVariableChangedHandler);
        }
        static ThemeWatcher()
        {
            _current = new ThemeWatcher();
        }

        /// <summary>
        /// Gets the <see cref="ThemeWatcher"/> object for the current Running AutoCAD session.
        /// </summary>
        public static ThemeWatcher Current => _current;
        /// <summary>
        /// Gets the current AutoCAD ColorTheme.
        /// </summary>
        public string CurrentCadTheme
        {
            get => _name;
            private set => SetName(value);
        }

        public event CadThemeChangedHandler CadThemeChanged;

        /// <summary>
        /// Gets the current AutoCAD ColorTheme SystemVariable.
        /// </summary>
        /// <returns></returns>
        public static string GetColorThemeVariable()
        {
            return (short)Autodesk.AutoCAD.ApplicationServices.Core.Application.GetSystemVariable(COLORTHEME) == 0
                ? DARK
                : LIGHT;
        }
        private void SetName(string value)
        {
            if (value != _name)
            {
                _name = value;
                OnCadThemeChanged(value);
            }
        }
        private void SystemVariableChangedHandler(object sender, Autodesk.AutoCAD.ApplicationServices.SystemVariableChangedEventArgs e)
        {
            if (string.Equals(e.Name, COLORTHEME, StringComparison.InvariantCultureIgnoreCase) 
                && e.Changed)
            {
                CurrentCadTheme = GetColorThemeVariable();
            }
        }
        /// <summary>
        /// Fires when <see cref="CurrentCadTheme"/> changes.
        /// </summary>
        /// <param name="newTheme"></param>
        protected virtual void OnCadThemeChanged(string newTheme)
        {
            CadThemeChanged?.Invoke(this, new CadThemeChangedArgs(newTheme));
        }
    }
}
