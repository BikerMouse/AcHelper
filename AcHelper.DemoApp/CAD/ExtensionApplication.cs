using AcHelper;
using Autodesk.AutoCAD.Runtime;
using BuerTech.Utilities.Logger;

[assembly: ExtensionApplication(typeof(AcHelper.DemoApp.CAD.ExtensionApplication))]
[assembly: CommandClass(typeof(AcHelper.DemoApp.CAD.CommandHandler))]
namespace AcHelper.DemoApp.CAD
{
    using Core;

    public class ExtensionApplication : IExtensionApplication
    {
        #region IExtensionApplication members ...
        public void Initialize()
        {
            // Todo: If you're using the inbuilt logger from AcHelper, uncomment the below and edit => SetupLogger();
            // SetupLogger();

            // Todo: If you're using WPF in the Acad Plug-in, uncomment below
            // SetupViewModelLocator();
        }

        public void Terminate()
        {
            Logger.Dispose();
        }
        #endregion

        private void SetupLogger()
        {
            // Todo: If you're using the inbuilt logger from AcHelper, edit below
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
        }
    }
}
