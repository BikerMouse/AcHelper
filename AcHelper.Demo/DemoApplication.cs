using Autodesk.AutoCAD.Runtime;
using System;
using System.Drawing;
using AcHelper.WPF.Palettes;
using System.Windows;

[assembly: ExtensionApplication(typeof(AcHelper.Demo.DemoApplication))]
[assembly: CommandClass(typeof(AcHelper.Demo.CommandHandler))]
[assembly: CLSCompliant(false)]
namespace AcHelper.Demo
{
    public class DemoApplication : IExtensionApplication
    {
        #region Fields ...
        private static WpfPaletteSetsHandler _paletteSetsHandler = WpfPaletteSetsHandler.Instance;
        private static ResourceDictionary _genericResources
        {
            get
            {
                string uri = @"/AcHelper.Demo;component/Resources/Generic.xaml";
                return System.Windows.Application.LoadComponent(new Uri(uri, UriKind.Relative)) as ResourceDictionary;
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

            SecureResources();

            PreparePaletteSets();

            Active.WriteMessage("\n*** AcHelper Demo loaded ***");
        }

        private void PreparePaletteSets()
        {
            // Main paletteset
            PaletteSetsHandler.CreatePaletteSet(DemoConstants.GUID_MAINPALETTESET
                , DemoConstants.PLTS_MAINPALETTESET
                , new System.Drawing.Size(300, 800));
        }

        public void Terminate()
        {
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
