using System;

namespace AcHelper.WPF.Palettes
{
    public delegate void WpfPaletteVisibleStateChangedEventHandler(object sender, WpfPaletteVisibleStateChangedEventArgs arg);
    public delegate void WpfPaletteClosingEventHandler(object sender, WpfPaletteClosingEventArgs arg);
    public delegate void WpfPaletteClosedEventHandler(object sender, WpfPaletteClosedEventArgs arg);

    public enum VisibleState
    {
        Unknown = 0,
        Show = 1,
        Hide = 2
    }

    public interface IPalette : IDisposable
    {
        // State
        VisibleState VisibleState { get; set; }

        // Properties
        string PaletteName { get; set; }
        WpfPaletteSet PaletteSet { get; set; }

        // Methods
        void ClosePaletteSet();

        // Events
        event WpfPaletteClosingEventHandler WpfPaletteClosing;
        event WpfPaletteClosedEventHandler WpfPaletteClosed;
        event WpfPaletteVisibleStateChangedEventHandler VisibleStateChanged;

        void Close();
    }
}
