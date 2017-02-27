using Autodesk.AutoCAD.Windows;
using System;
using System.Collections.Generic;
using System.Drawing;

namespace AcHelper.WPF.Palettes
{
    public interface IWpfPaletteSetHandler
    {
        WpfPaletteSet CreatePaletteSet(string name, Guid guid);
        WpfPaletteSet CreatePaletteSet(string name, Guid guid, Size size, Size minimumSize);
        WpfPaletteSet CreatePaletteSet(string name, Guid guid, Size size, Size minimumSize, DockSides dockside, DockSides docksideEnabled);

        void HidePaletteSet(Guid guid);
        void ActivatePaletteSet(Guid guid);
    }
}
