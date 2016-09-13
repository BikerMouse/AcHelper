using AcHelper.Command;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AcHelper.Demo.Commands
{
    public class HelpCommand : IAcadCommand
    {
        #region IAcadCommand Members

        public void Execute()
        {
            Active.WriteMessage("\n=========================================");
            Active.WriteMessage("\nWelcome to the AcHelper Demo for AutoCAD");
            Active.WriteMessage("\nThe next commands are available:");
            Active.WriteMessage("\n\t- DEMO_DRAWCIRCLE");
            Active.WriteMessage("\n\t- DEMO_THROWERROR");
            Active.WriteMessage("\n\t- DEMO_ADDXRECORD");
            Active.WriteMessage("\n=========================================\n");
        }

        #endregion
    }
}
