using AcHelper.Commands;
using AcHelper.DemoApp.Core;

namespace AcHelper.DemoApp.CAD.Commands
{
    public class OpenToolpaletteCommand : IAcadCommand
    {
        public void Execute()
        {
            ExtensionApplication.PalettesetsHandler[Constants.GUID_PALETTESET].ActivatePaletteSet();
        }
    }
}
