using System;

namespace AcHelper.WPF.Palettes
{
    /// <summary>
    /// Represents the method that will handle an event that handles the
    /// <see cref="IPalette.VisibleStateChanged"/> event of a WpfPalette.
    /// </summary>
    /// <param name="sender">The source of the event.</param>
    /// <param name="arg">A <see cref="WpfPaletteVisibleStateChangedEventArgs"/>WpfPaletteVisibleStateChangedEventArgs that contains the event data.</param>
    public delegate void WpfPaletteVisibleStateChangedEventHandler(object sender, WpfPaletteVisibleStateChangedEventArgs arg);
    /// <summary>
    /// Represents the method that will handle an event that handles the
    /// <see cref="IPalette.WpfPaletteClosing"/> event of a WpfPalette.
    /// </summary>
    /// <param name="sender">The source of the event.</param>
    /// <param name="arg">A <see cref="WpfPaletteClosingEventArgs"/>WpfPaletteClosingEventArgs that contains the event data.</param>
    public delegate void WpfPaletteClosingEventHandler(object sender, WpfPaletteClosingEventArgs arg);
    /// <summary>
    /// Represents the method that will handle an event that handles the
    /// <see cref="IPalette.WpfPaletteClosed"/> event of a WpfPalette.
    /// </summary>
    /// <param name="sender">The source of the event.</param>
    /// <param name="arg">A <see cref="WpfPaletteClosedEventArgs"/>WpfPaletteClosedEventArgs that contains the event data.</param>
    public delegate void WpfPaletteClosedEventHandler(object sender, WpfPaletteClosedEventArgs arg);

    /// <summary>
    /// Enum that represents the visible state of a WpfPalette.
    /// </summary>
    public enum VisibleState
    {
        Unknown = 0,
        Show = 1,
        Hide = 2
    }

    public interface IPalette : IDisposable
    {
        /// <summary>
        /// The visible state of the WpfPalette.
        /// </summary>
        VisibleState VisibleState { get; set; }
        /// <summary>
        /// The name of the WpfPalette.
        /// </summary>
        string PaletteName { get; set; }
        /// <summary>
        /// The parent WpfPaletteset wich contains this WpfPalette
        /// </summary>
        WpfPaletteSet PaletteSet { get; set; }

        /// <summary>
        /// Closes the paletteset.
        /// </summary>
        void ClosePaletteSet();

        // Events
        event WpfPaletteClosingEventHandler WpfPaletteClosing;
        event WpfPaletteClosedEventHandler WpfPaletteClosed;
        event WpfPaletteVisibleStateChangedEventHandler VisibleStateChanged;

        /// <summary>
        /// Closes the WpfPalette
        /// </summary>
        void Close();
    }
}
