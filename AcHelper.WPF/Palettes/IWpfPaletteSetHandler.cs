using Autodesk.AutoCAD.Windows;
using System;
using System.Collections.Generic;
using System.Drawing;

namespace AcHelper.WPF.Palettes
{
    public interface IWpfPaletteSetHandler
    {
        Dictionary<Guid, WpfPaletteSet> PaletteSets { get; }

        WpfPaletteSet CreatePaletteSet(Guid guid, string Name
            , Size? size = null
            , Size? minimumSize = null
            , DockSides? dockSide = DockSides.Left
            , DockSides? dockEnabled = DockSides.None | DockSides.Left | DockSides.Right);

        void AddPaletteSet(Guid guid, WpfPaletteSet paletteSet);
        void HidePaletteSet(Guid guid);
        void ActivatePaletteSet(Guid guid);
    }
}
