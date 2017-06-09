using AcHelper;
using AcHelper.WPF.Palettes;
using AcHelper.WPF.Themes;
using Autodesk.AutoCAD.Runtime;
using Autodesk.AutoCAD.Windows;
using BuerTech.Utilities.Logger;
using System;
using System.Windows;

[assembly: ExtensionApplication(typeof(NedGraphics.AutoCad.Test.CAD.ExtensionApplication))]
[assembly: CommandClass(typeof(NedGraphics.AutoCad.Test.CAD.CommandHandler))]
[assembly: CLSCompliant(false)]
namespace NedGraphics.AutoCad.Test.CAD
{
    using AcHelper.Extra;
    using Core;

    public class ExtensionApplication : IExtensionApplication
    {
        public static readonly ResourceHandler ResourceHandler = ResourceHandler.GetInstance();
        public static readonly WpfPaletteSetsHandler PalettesetsHandler = WpfPaletteSetsHandler.GetInstance();

        #region IExtensionApplication members ...
        public void Initialize()
        {
            using (CadRegistryHandler reg = new CadRegistryHandler("NEDINFRAGEO"))
            {
                Active.WriteMessage(reg.Hive.ToString());
                Active.WriteMessage(reg.RootKey);
                string test = reg.GetValue<string>("XREF_");
                if (string.IsNullOrEmpty(test))
                {
                    Active.WriteMessage("oeps... geen value gevonden ...");
                }
                else
                {
                    Active.WriteMessage(test);
                }
            }

            // Todo: If you're using the inbuilt logger from AcHelper, uncomment the below and edit => SetupLogger();
            // SetupLogger();

            // Todo: If you're using WPF in the Acad Plug-in, uncomment below
            // SetupViewModelLocator();

            // Todo: If you're using Themes linked to AutoCAD, uncomment below and use the theme.xaml files in ./Themes.
            // SetupThemes();

            // Todo: If your application uses one or more palettesets, uncomment below and edit => SetupPalettes();
            // SetupPalettes();

            // Todo: Uncomment Event registers to use eventshandlers from AutoCAD. Go to ./CAD/AcEvents.cs for corresponding methods.
            // SetupAutoCadEvents();

            Active.WriteMessage("{0} succesfully loaded ...", Constants.APPLICATION_NAME);
        }

        public void Terminate()
        {
            if (ThemeManager.IsTrackingThemes)
            {
                ThemeManager.TurnOffThemeTracker();
            }
            Logger.Dispose();
        }
        #endregion

        #region [           Properties          ]
        public Application App
        {
            get
            {
                if (Application.Current == null)
                {
                    SecureAppFile();
                }
                return Application.Current;
            }
        }
        #endregion

        #region [           Methods             ]
        public static ResourceDictionary GetGenericResourceDictionary()
        {
            Uri resourceUri = new Uri(Constants.RESOURCES_GENERIC, UriKind.Relative);
            return Application.LoadComponent(resourceUri) as ResourceDictionary;
        }
        #endregion

        #region Setup methods ...
        private void SetupLogger()
        {
            // Todo: If you're using the inbuilt logger from AcHelper, edit below
            LogSetup setup = new LogSetup
            {
                ApplicationName = Constants.APPLICATION_NAME,
                MaxAge = 0,
                MaxQueueSize = 1,
                SaveLocation = Constants.DIR_LOGGING            // Change the logging directory in ../Core/Constants.cs
            };
            Logger.Initialize(setup);
        }
        private void SetupViewModelLocator()
        {
            ViewModelLocator.Initialize();

            // Add Generic.xaml to Appfile

            // Todo: If you're using the AcHelper.WPF library let the ResourceHandler handle this and uncomment below
            // ResourceHandler.SetGenericResourceDictionary(GetAssemblyName());

            // Todo: If you're not using the AcHelper.WPF library for some weird reason then uncomment below.
            //// WARNING! DO NOT ADD THEME FILES, THIS WILL OVERRIDE AUTOCAD STYLES!!
            //App.Resources.MergedDictionaries.Clear();
            //App.Resources.MergedDictionaries.Add(GetGenericResourceDictionary());
        }
        private void SetupThemes()
        {
            SetupLightTheme();
            SetupDarkTheme();
            ThemeManager.TurnOnThemeTracker();
        }
        private void SetupPalettes()
        {
            // Create a Paletteset
            System.Drawing.Size mainPalettesetSize = new System.Drawing.Size(450, 600);
            DockSides mainPaletteDockSide = DockSides.Left;
            DockSides mainPaletteDockSidesEnabled = DockSides.Left | DockSides.Right | DockSides.None;

            WpfPaletteSet mainPaletteset = PalettesetsHandler.CreatePaletteSet(
                Constants.APPLICATION_NAME              // Paletteset name
                , Constants.GUID_MAINPALETTESET         // Paletteset GUID
                , mainPalettesetSize                    // Paletteset Size
                , mainPalettesetSize                    // Paletteset minimum size
                , mainPaletteDockSide                   // Paletteset default dockside
                , mainPaletteDockSidesEnabled);         // Paletteset enabled docksides
        }
        private void SetupAutoCadEvents()
        {
            //
            // AutoCad Application Events
            //
            //Autodesk.AutoCAD.ApplicationServices.Core.Application.BeginDoubleClick += AcEvents.Application_BeginDoubleClick;
            //Autodesk.AutoCAD.ApplicationServices.Core.Application.BeginQuit += AcEvents.Application_BeginQuit;
            //Autodesk.AutoCAD.ApplicationServices.Core.Application.EnterModal += AcEvents.Application_EnterModal;
            //Autodesk.AutoCAD.ApplicationServices.Core.Application.Idle += AcEvents.Application_Idle;
            //Autodesk.AutoCAD.ApplicationServices.Core.Application.LeaveModal += AcEvents.Application_LeaveModal;
            //Autodesk.AutoCAD.ApplicationServices.Core.Application.PreTranslateMessage += AcEvents.Application_PreTranslateMessage;
            //Autodesk.AutoCAD.ApplicationServices.Core.Application.QuitAborted += AcEvents.Application_QuitAborted;
            //Autodesk.AutoCAD.ApplicationServices.Core.Application.QuitWillStart += AcEvents.Application_QuitWillStart;
            //Autodesk.AutoCAD.ApplicationServices.Core.Application.SystemVariableChanged += AcEvents.Application_SystemVariableChanged;
            //Autodesk.AutoCAD.ApplicationServices.Core.Application.SystemVariableChanging += AcEvents.Application_SystemVariableChanging;
            //Autodesk.AutoCAD.ApplicationServices.Application.EndCustomizationMode += AcEvents.Application_EndCustomizationMode;
            //Autodesk.AutoCAD.ApplicationServices.Application.DisplayingOptionDialog += AcEvents.Application_DisplayingOptionDialog;
            //Autodesk.AutoCAD.ApplicationServices.Application.DisplayingDraftingSettingsDialog += AcEvents.Application_DisplayingDraftingSettingsDialog;
            //Autodesk.AutoCAD.ApplicationServices.Application.DisplayingCustomizeDialog += AcEvents.Application_DisplayingCustomizeDialog;
            //Autodesk.AutoCAD.ApplicationServices.Application.BeginCustomizationMode += AcEvents.Application_BeginCustomizationMode;

            //
            // Document Manager Events
            //
            //Autodesk.AutoCAD.ApplicationServices.Core.Application.DocumentManager.DocumentActivated += AcEvents.DocumentManager_DocumentActivated;
            //Autodesk.AutoCAD.ApplicationServices.Core.Application.DocumentManager.DocumentActivationChanged += AcEvents.DocumentManager_DocumentActivationChanged;
            //Autodesk.AutoCAD.ApplicationServices.Core.Application.DocumentManager.DocumentBecameCurrent += AcEvents.DocumentManager_DocumentBecameCurrent;
            //Autodesk.AutoCAD.ApplicationServices.Core.Application.DocumentManager.DocumentCreated += AcEvents.DocumentManager_DocumentCreated;
            //Autodesk.AutoCAD.ApplicationServices.Core.Application.DocumentManager.DocumentCreateStarted += AcEvents.DocumentManager_DocumentCreateStarted;
            //Autodesk.AutoCAD.ApplicationServices.Core.Application.DocumentManager.DocumentCreationCanceled += AcEvents.DocumentManager_DocumentCreationCanceled;
            //Autodesk.AutoCAD.ApplicationServices.Core.Application.DocumentManager.DocumentDestroyed += AcEvents.DocumentManager_DocumentDestroyed;
            //Autodesk.AutoCAD.ApplicationServices.Core.Application.DocumentManager.DocumentLockModeChanged += AcEvents.DocumentManager_DocumentLockModeChanged;
            //Autodesk.AutoCAD.ApplicationServices.Core.Application.DocumentManager.DocumentLockModeChangeVetoed += AcEvents.DocumentManager_DocumentLockModeChangeVetoed;
            //Autodesk.AutoCAD.ApplicationServices.Core.Application.DocumentManager.DocumentLockModeWillChange += AcEvents.DocumentManager_DocumentLockModeWillChange;
            //Autodesk.AutoCAD.ApplicationServices.Core.Application.DocumentManager.DocumentToBeActivated += AcEvents.DocumentManager_ToBeActivated;
            //Autodesk.AutoCAD.ApplicationServices.Core.Application.DocumentManager.DocumentToBeDeactivated += AcEvents.DocumentManager_ToBeDeactivated;
            //Autodesk.AutoCAD.ApplicationServices.Core.Application.DocumentManager.DocumentToBeDestroyed += AcEvents.DocumentManager_ToBeDestroyed;

            //
            // Database Events
            //
            //Autodesk.AutoCAD.DatabaseServices.Database.DatabaseConstructed += AcEvents.Database_DatabaseConstructed;
            //Autodesk.AutoCAD.DatabaseServices.Database.XrefAttachAborted += AcEvents.Database_XrefAttachAborted;
        }

        #region Helpers ...
        private void SecureAppFile()
        {
            Application app = new Application();
            app.ShutdownMode = ShutdownMode.OnExplicitShutdown;
        }
        private string GetAssemblyName()
        {
            return typeof(ExtensionApplication).Assembly.GetName().Name;
        }
        private void SetupDarkTheme()
        {
            // If you want to add more theme specific ResourceDictionaries
            //                               Raise index accordingly |
            ResourceDictionary[] dictionary = new ResourceDictionary[1];
            dictionary[0] = ResourceHandler.GetThemeResourceDictionary(GetAssemblyName(), ResourceHandler.THEME_DARK);
            ThemeSet themeset = ThemeSet.CreateThemeSet(dictionary);
            ResourceHandler.ThemeSets.Add(ResourceHandler.THEME_DARK, themeset);
        }
        private void SetupLightTheme()
        {
            // If you want to add more theme specific ResourceDictionaries
            //                               Raise index accordingly |
            ResourceDictionary[] dictionary = new ResourceDictionary[1];
            dictionary[0] = ResourceHandler.GetThemeResourceDictionary(GetAssemblyName(), ResourceHandler.THEME_LIGHT);
            ThemeSet themeset = ThemeSet.CreateThemeSet(dictionary);
            ResourceHandler.ThemeSets.Add(ResourceHandler.THEME_LIGHT, themeset);
        }
        #endregion
        #endregion
    }
}
