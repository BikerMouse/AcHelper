using System;

namespace AcHelper.DemoApp.Core
{
    /// <summary>
    /// The Constants class contains all constant/static variables 
    /// which are globaly used through the application.
    /// </summary>
    public class Constants
    {
        public const string APPLICATION_NAME = "AcHelper Demo Application";
        public const string APPLICATION_ABBREVIATION = "AcHelper DemoApp";

        public const string DIR_LOGGING = @"C:\Temp\";

        // Palettes
        public static Guid GUID_PALETTESET = Guid.NewGuid();
        public static Guid GUID_MAINPALETTE = Guid.NewGuid();
        public const string NAME_MAINPALETTE = "Main Palette";
    }
}
