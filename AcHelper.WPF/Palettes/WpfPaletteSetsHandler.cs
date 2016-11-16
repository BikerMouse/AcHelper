using Autodesk.AutoCAD.Windows;
using System;
using System.Collections.Generic;

namespace AcHelper.WPF.Palettes
{
    public class WpfPaletteSetsHandler : IWpfPaletteSetHandler
    {
        #region Singleton ...
        private WpfPaletteSetsHandler() { }
        private static WpfPaletteSetsHandler _instance;
        /// <summary>
        /// Static instance of WpfPaletteSetsHandler.
        /// </summary>
        public static WpfPaletteSetsHandler Instance
        {
            get
            {
                if (_instance == null)
                {
                    _instance = new WpfPaletteSetsHandler();
                }
                return _instance;
            }
        }
        #endregion

        #region IWpfPaletteSetHandler Members
        private Dictionary<Guid, WpfPaletteSet> _paletteSets;
        /// <summary>
        /// A dictionary containing multiple <see cref="WpfPaletteSet"/>WpfPaletteSets.
        /// Palettesets ToolId is used for index.
        /// </summary>
        public Dictionary<Guid, WpfPaletteSet> PaletteSets
        {
            get
            {
                if (_paletteSets == null)
                {
                    _paletteSets = new Dictionary<Guid, WpfPaletteSet>();
                }
                return _paletteSets;
            }
        }

        /// <summary>
        /// Returns a paletteset from the Dictionary
        /// With the given Guid as index.
        /// </summary>
        /// <param name="guid">Id of the paletteset</param>
        /// <returns>Null if non existing.</returns>
        public WpfPaletteSet this[Guid guid] 
        {
            get 
            {
                if (PaletteSets.ContainsKey(guid))
                {
                    return PaletteSets[guid]; 
                }
                return null;
            }
        }

        /// <summary>
        /// Creates a new <see cref="WpfPaletteSet"/>WpfPaletteSet and adds it to the dictionary.
        /// </summary>
        /// <param name="guid">Id if the paletteset.</param>
        /// <param name="name"></param>
        /// <param name="size"></param>
        /// <param name="minimumSize"></param>
        /// <param name="dockSide">Enabled docksides.</param>
        /// <param name="dockEnabled">Determines whether docking needs to be enabled.</param>
        /// <returns></returns>
        public WpfPaletteSet CreatePaletteSet(Guid guid, string name, System.Drawing.Size? size = null, System.Drawing.Size? minimumSize = null, DockSides? dockSide = DockSides.Left, DockSides? dockEnabled = DockSides.None | DockSides.Left | DockSides.Right)
        {
            // Create PaletteSet
            WpfPaletteSet newPaletteSet = new WpfPaletteSet(name, guid, size, minimumSize, dockSide, dockEnabled);
            // Add PaletteSet to Dictionary
            AddPaletteSet(guid, newPaletteSet);

            return newPaletteSet;
        }

        /// <summary>
        /// Adds a <see cref="WpfPaletteSet"/>WpfPaletteSet to the dictionary.
        /// </summary>
        /// <param name="guid"></param>
        /// <param name="paletteSet"></param>
        public void AddPaletteSet(Guid guid, WpfPaletteSet paletteSet)
        {
            PaletteSets.Add(guid, paletteSet);
        }

        /// <summary>
        /// Turn off visibility of given <see cref="WpfPaletteSet"/>WpfPaletteSet.
        /// </summary>
        /// <param name="guid"></param>
        public void HidePaletteSet(Guid guid)
        {
            if (PaletteSets.ContainsKey(guid))
            {
                PaletteSets[guid].Visible = false;
                return;
            }
            Console.WriteLine("Key: [{0}] not found in dictionary.");
        }
        /// <summary>
        /// Turn on visibility of the given <see cref="WpfPaletteSet"/>WpfPaletteSet.
        /// </summary>
        /// <param name="guid"></param>
        public void ActivatePaletteSet(Guid guid)
        {
            PaletteSets[guid].Visible = true;
        }

        #endregion
    }
}
