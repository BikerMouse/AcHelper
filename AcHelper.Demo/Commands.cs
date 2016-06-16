using Autodesk.AutoCAD.Runtime;

namespace AcHelper.Demo
{
    public class Commands
    {
        [CommandMethod("DEMO_HELP")]
        public static void Demo_Help()
        {
            Active.WriteMessage("\n=========================================");
            Active.WriteMessage("\nWelcome to the AcHelper Demo for AutoCAD");
            Active.WriteMessage("\nThe next commands are available:");
            Active.WriteMessage("\n\t- DEMO_DRAWCIRCLE");
            Active.WriteMessage("\n=========================================\n");
        }

        [CommandMethod("DEMO_DRAWCIRCLE")]
        public static void Demo_DrawCircle()
        {
            Utilities.DrawCircle(Active.Document, 50);
        }
    }
}
