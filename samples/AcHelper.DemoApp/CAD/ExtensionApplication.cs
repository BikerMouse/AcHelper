using Autodesk.AutoCAD.Runtime;
using BuerTech.Utilities.Logger;
using System.Windows;

[assembly: ExtensionApplication(typeof(AcHelper.DemoApp.CAD.ExtensionApplication))]
[assembly: CommandClass(typeof(AcHelper.DemoApp.CAD.CommandHandler))]
namespace AcHelper.DemoApp.CAD
{
    using Core;
    using WPF.Palettes;
    using WPF.Themes;

    public class ExtensionApplication : IExtensionApplication
    {
        public static readonly ResourceHandler ResourceHandler = ResourceHandler.GetInstance();
        public static readonly WpfPaletteSetsHandler PalettesetsHandler = WpfPaletteSetsHandler.GetInstance();

        #region IExtensionApplication members ...
        public void Initialize()
        {
            SetupLogger();

            SetupViewModelLocator();

            SetupThemes();

            SetupEvents();

            PreparePalettes();

            Active.WriteMessage("{0} initialized ...", Constants.APPLICATION_NAME);
        }

        public void Terminate()
        {
            Logger.Dispose();
        }
        #endregion

        #region Private methods ...
        private void SetupLogger()
        {
            LogSetup setup = new LogSetup
            {
                ApplicationName = Constants.APPLICATION_NAME,
                MaxAge = 0,
                MaxQueueSize = 1,
                SaveLocation = Constants.DIR_LOGGING
            };
            Logger.Initialize(setup);
        }
        private void SetupViewModelLocator()
        {
            ViewModelLocator.Initialize();
            // Secure the generic.xaml file for the Locator.
            ResourceHandler.SetGenericResourceDictionary(GetAssemblyName());
        } 
        private void SetupThemes()
        {
            ResourceDictionary[] darkTheme = new ResourceDictionary[1];
            ResourceDictionary[] lightTheme = new ResourceDictionary[1];

            // Dark
            darkTheme[0] = ResourceHandler.GetThemeResourceDictionary(GetAssemblyName(), ResourceHandler.THEME_DARK);
            ThemeSet darkSet = ThemeSet.CreateThemeSet(darkTheme);
            ResourceHandler.ThemeSets.Add(ResourceHandler.THEME_DARK, darkSet);

            // Light
            lightTheme[0] = ResourceHandler.GetThemeResourceDictionary(GetAssemblyName(), ResourceHandler.THEME_LIGHT);
            ThemeSet lightSet = ThemeSet.CreateThemeSet(lightTheme);
            ResourceHandler.ThemeSets.Add(ResourceHandler.THEME_LIGHT, lightSet);

            ThemeManager.TurnOnThemeTracker();
        }
        private void SetupEvents()
        {
            ThemeManager.TurnOnThemeTracker();
        }
        private void PreparePalettes()
        {
            WpfPaletteSet paletteset = PalettesetsHandler.CreatePaletteSet(Constants.APPLICATION_NAME, Constants.GUID_PALETTESET);
            WpfPalette mainPalette = new WpfPalette(new GUI.Views.PaletteView(), Constants.NAME_MAINPALETTE);
            WpfPalette secondPalette = new WpfPalette(new GUI.Views.SecondPaletteView(), "Second Palette");

            paletteset.AddPalette(mainPalette);
            paletteset.AddPalette(secondPalette);
        }
        private string GetAssemblyName()
        {
            return typeof(ExtensionApplication).Assembly.GetName().Name;
        }
        #endregion
    }
}
