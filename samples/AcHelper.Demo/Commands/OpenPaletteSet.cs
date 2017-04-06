using AcHelper.Commands;
using System;
using System.Windows.Forms;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace AcHelper.Demo.Commands
{
    using AcHelper.Demo.View;
    using WPF.Palettes;

    public class OpenPaletteSet : IAcadCommand
    {
        #region IAcadCommand Members

        public void Execute()
        {
            DemoApplication.PaletteSetsHandler[DemoConstants.GUID_MAINPALETTESET].Visible = true;
            //WpfPaletteSet paletteSet =  DemoApplication.PaletteSet;
            //WpfPalette mainPalette = paletteSet.Palettes
            //    .FirstOrDefault(p => p.PaletteName.ToUpper() == "MainPalette") as WpfPalette;
            //if (mainPalette == null)
            //{
            //    MainPaletteView v = new MainPaletteView();
            //    paletteSet.AddPalette(new WpfPalette(v, "MainPalette"));
            //}
            //paletteSet.Visible = true;
        }

        #endregion
    }
}
