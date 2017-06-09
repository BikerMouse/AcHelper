using AcHelper;
using AcHelper.Commands;

namespace NedGraphics.AutoCad.Test.CAD.Commands
{
    /// <summary>
    /// Writes "Hello AutoCAD World!" on the commandline.
    /// </summary>
    public class HelloAcadWorldCommand : IAcadCommand
    {
        /// <summary>
        /// Executes the logic.
        /// </summary>
        public void Execute()
        {
            Active.WriteMessage("Hello AutoCAD World!");
        }
    }
}
