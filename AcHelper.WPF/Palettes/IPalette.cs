using Autodesk.AutoCAD.ApplicationServices;

namespace AcHelper.WPF.Palettes
{
    public enum VisibleState
    {
        Unknown = 0,
        Show = 1,
        Hide = 2
    }

    public interface IPalette
    {
        // State
        VisibleState VisibleState { get; set; }

        // Properties
        string PaletteName { get; set; }
        WpfPaletteSet PaletteSet { get; set; }

        // Methods
        void ClosePaletteSet();

        // Events
        event WpfPaletteClosingEventHandler PaletteClosing;
        event WpfPaletteVisibleStateChangedEventHandler VisibleStateChanged;

        void Close();
    }
}
