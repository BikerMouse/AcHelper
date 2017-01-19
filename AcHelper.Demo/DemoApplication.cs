using AcHelper.Demo.View;
using AcHelper.WPF.Palettes;
using Autodesk.AutoCAD.Runtime;
using System;
using System.IO;
using System.Linq;
using System.Windows;

[assembly: ExtensionApplication(typeof(AcHelper.Demo.DemoApplication))]
[assembly: CommandClass(typeof(AcHelper.Demo.CommandHandler))]
[assembly: CLSCompliant(false)]
namespace AcHelper.Demo
{
    ////using AcLog = AcHelper.Logging;

    public class DemoApplication : IExtensionApplication
    {
        #region Fields ...
        private static WpfPaletteSetsHandler _paletteSetsHandler = WpfPaletteSetsHandler.Instance;
        private static ResourceDictionary _genericResources
        {
            get
            {
                string uri = @"/AcHelper.Demo;component/Resources/Generic.xaml";
                return Application.LoadComponent(new Uri(uri, UriKind.Relative)) as ResourceDictionary;
            }
        }
        #endregion
        
        #region Properties ...
        #region PaletteSetsHandler ...
        public static WpfPaletteSetsHandler PaletteSetsHandler
        {
            get { return _paletteSetsHandler; }
        }
        #endregion

        #region PaletteSet ...
        private static WpfPaletteSet _paletteSet;
        public static WpfPaletteSet PaletteSet
        {
            get
            {
                if (_paletteSet == null)
                {
                    _paletteSet = CreatePaletteSet("AcHelper Demo", Guid.NewGuid());
                }
                return _paletteSet;
            }
            set { _paletteSet = value; }
        }
        #endregion

        #region App File ...
        public System.Windows.Application App
        {
            get
            {
                if (System.Windows.Application.Current == null)
                {
                    SecureAppFile();
                }
                return System.Windows.Application.Current;
            }
        }
        #endregion 
        #endregion

        #region IExtensionApplication members ...
        public void Initialize()
        {
            Active.WriteMessage("\n*** Loading AcHelper Demo ***");

            SetupLogger();

            SecureResources();

            PreparePaletteSets();

            Active.WriteMessage("\n*** AcHelper Demo loaded ***");
        }

        private void SetupLogger()
        {
            Logger.Initialize("AcHelper Demo", Path.Combine(Environment.GetFolderPath(Environment.SpecialFolder.MyDocuments), "LogFiles"), 0, 1);
            Active.WriteMessage("Logger setup ...");
        }

        private void PreparePaletteSets()
        {
            // Main paletteset
            WpfPaletteSet paletteSet = PaletteSetsHandler.CreatePaletteSet(DemoConstants.GUID_MAINPALETTESET
                , DemoConstants.PLTS_MAINPALETTESET
                , new System.Drawing.Size(300, 800));

            WpfPalette mainPalette = paletteSet.Palettes
                .FirstOrDefault(p => p.PaletteName.ToUpper() == "MainPalette") as WpfPalette;
            if (mainPalette == null)
            {
                MainPaletteView v = new MainPaletteView();
                paletteSet.AddPalette(new WpfPalette(v, "MainPalette"));
            }

            Active.WriteMessage("Paletteset created ...");
        }

        public void Terminate()
        {
            Logger.Dispose();
        }
        #endregion

        #region Private methods ...
        private static WpfPaletteSet CreatePaletteSet(string name, Guid guid)
        {
            var paletteSet = new WpfPaletteSet(name, guid
                , new System.Drawing.Size(300, 800));

            return paletteSet;
        }
        private void SecureResources()
        {
            App.Resources.MergedDictionaries.Clear();
            App.Resources.MergedDictionaries.Add(_genericResources);    // Locator
            Active.WriteMessage("Resources secured ...");
        }

        private static void SecureAppFile()
        {
            if (System.Windows.Application.Current == null)
            {
                new System.Windows.Application();
                System.Windows.Application.Current.ShutdownMode = System.Windows.ShutdownMode.OnExplicitShutdown;
            }
        }
        #endregion
    }
}
