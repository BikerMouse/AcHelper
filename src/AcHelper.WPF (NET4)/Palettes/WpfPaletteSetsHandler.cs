using Autodesk.AutoCAD.Windows;
using System;
using System.Collections.Generic;
using System.Drawing;

namespace AcHelper.WPF.Palettes
{
    public class WpfPaletteSetsHandler : Dictionary<Guid, WpfPaletteSet>, IWpfPaletteSetHandler
    {
        #region Singleton ...
        private WpfPaletteSetsHandler() { }
        private static WpfPaletteSetsHandler _instance;
        /// <summary>
        /// Static instance of WpfPaletteSetsHandler.
        /// </summary>
        public static WpfPaletteSetsHandler GetInstance()
        {
            if (_instance == null)
            {
                _instance = new WpfPaletteSetsHandler();
            }
            return _instance;
        }
        #endregion

        #region IWpfPaletteSetHandler Members
        /// <summary>
        /// Creates a new <see cref="WpfPaletteSet"/>WpfPaletteSet and adds it to the dictionary.
        /// </summary>
        /// <param name="name"></param>
        /// <param name="guid"></param>
        /// <returns></returns>
        public WpfPaletteSet CreatePaletteSet(string name, Guid guid)
        {
            WpfPaletteSet paletteset = new WpfPaletteSet(name, guid);
            Add(guid, paletteset);
            return paletteset;
        }
        /// <summary>
        /// Creates a new <see cref="WpfPaletteSet"/>WpfPaletteSet and adds it to the dictionary.
        /// </summary>
        /// <param name="name"></param>
        /// <param name="guid"></param>
        /// <param name="size"></param>
        /// <param name="minimumSize"></param>
        /// <returns></returns>
        public WpfPaletteSet CreatePaletteSet(string name, Guid guid, Size size, Size minimumSize)
        {
            WpfPaletteSet paletteset = new WpfPaletteSet(name, guid, size, minimumSize);
            Add(guid, paletteset);
            return paletteset;
        }
        /// <summary>
        /// Creates a new <see cref="WpfPaletteSet"/>WpfPaletteSet and adds it to the dictionary.
        /// </summary>
        /// <param name="name"></param>
        /// <param name="guid"></param>
        /// <param name="size"></param>
        /// <param name="minimumSize"></param>
        /// <param name="dockside"></param>
        /// <param name="docksideEnabled"></param>
        /// <returns></returns>
        public WpfPaletteSet CreatePaletteSet(string name, Guid guid, Size size, Size minimumSize, DockSides dockside, DockSides docksideEnabled)
        {
            WpfPaletteSet paletteset = new WpfPaletteSet(name, guid, size, minimumSize, dockside, docksideEnabled);
            Add(guid, paletteset);
            return paletteset;
        }

        /// <summary>
        /// Turn off visibility of given <see cref="WpfPaletteSet"/>WpfPaletteSet.
        /// </summary>
        /// <param name="guid"></param>
        public void HidePaletteSet(Guid guid)
        {
            if (!ContainsKey(guid))
            {
                Console.WriteLine("Key: [{0}] not found in dictionary.");
            }
            this[guid].Visible = false;
        }
        /// <summary>
        /// Turn on visibility of the given <see cref="WpfPaletteSet"/>WpfPaletteSet.
        /// </summary>
        /// <param name="guid"></param>
        public void ActivatePaletteSet(Guid guid)
        {
            this[guid].ActivatePaletteSet();
        }
        #endregion
    }
}
