using GalaSoft.MvvmLight;

namespace AcHelper.DemoApp.GUI.ViewModels
{
    /// <summary>
    /// This class contains properties that a View can data bind to.
    /// <para>
    /// See http://www.galasoft.ch/mvvm
    /// </para>
    /// </summary>
    public class PaletteViewModel : ViewModelBase
    {
        private readonly WPF.Themes.ThemeManager _themeManager = WPF.Themes.ThemeManager.GetInstance();
        /// <summary>
        /// Initializes a new instance of the PaletteViewModel class.
        /// </summary>
        public PaletteViewModel()
        {
        }

        #region [           Properties         ]
        #region Thememanager ...
        /// <summary>
        /// Sets and gets the ThemeManager property.
        /// Changes to that property's value raise the PropertyChanged event. 
        /// </summary>
        public WPF.Themes.ThemeManager ThemeManager
        {
            get
            {
                return _themeManager;
            }
        }
        #endregion
        #endregion
    }
}