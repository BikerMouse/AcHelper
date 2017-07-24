using GalaSoft.MvvmLight;
using System;
using System.Collections.Generic;
using System.Text;

namespace AcHelper.WPF
{
    using Themes;

    public class PaletteViewModelBase : ViewModelBase
    {
        private readonly ThemeManager _themeManager;
        public PaletteViewModelBase()
        {
            _themeManager = ThemeManager.Current;
            _theme = _themeManager.Watcher.Name;
        }

        /// <summary>
        /// The <see cref="Theme" /> property's name.
        /// </summary>
        public const string ThemePropertyName = "Theme";

        private string _theme;

        /// <summary>
        /// Sets and gets the Theme property.
        /// Changes to that property's value raise the PropertyChanged event. 
        /// </summary>
        public string Theme
        {
            get
            {
                return _theme;
            }
            set
            {
                Set(ThemePropertyName, ref _theme, value);
            }
        }
    }
}
