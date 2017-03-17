using Autodesk.AutoCAD.Runtime;
using BuerTech.Utilities.Logger;

[assembly: ExtensionApplication(typeof(AcHelper.DemoApp.CAD.ExtensionApplication))]
[assembly: CommandClass(typeof(AcHelper.DemoApp.CAD.CommandHandler))]
namespace AcHelper.DemoApp.CAD
{
    using System;
    using Core;
    using WPF.Themes;
    using System.Windows;
    using System.Reflection;

    public class ExtensionApplication : IExtensionApplication
    {
        private readonly ResourceHandler _resourceHandler = ResourceHandler.GetInstance();

        #region IExtensionApplication members ...
        public void Initialize()
        {
            SetupLogger();

            SetupViewModelLocator();

            SetupThemes();

            SetupEvents();

            Active.WriteMessage("{0} initialized ...", Constants.APPLICATION_NAME);
        }

        public void Terminate()
        {
            Logger.Dispose();
        }
        #endregion

        #region [           Properties          ]
        #region Assembly name ...
        private string _assemblyName;

        public string AssemblyPackName
        {
            get { return _assemblyName ?? GetAssemblyName(); }
            set { _assemblyName = value; }
        }

        private string GetAssemblyName()
        {
            return AssemblyName.GetAssemblyName(Assembly.GetExecutingAssembly().FullName).Name;
        }

        #endregion
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
        private void SetupThemes()
        {
            ResourceDictionary dictionaryDark = _resourceHandler.GetThemeResourceDictionary(GetAssemblyName(), ResourceHandler.THEME_DARK);
        }
        private void SetupEvents()
        {
            ThemeManager.TurnOnThemeTracker();
        }
        private void SetupViewModelLocator()
        {
            ViewModelLocator.Initialize();
        } 
        #endregion
    }
}
