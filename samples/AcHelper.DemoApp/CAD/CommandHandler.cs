using AcHelper.Commands;
using Autodesk.AutoCAD.Runtime;

namespace AcHelper.DemoApp.CAD
{
    using Commands;

    /// <summary>
    /// The command handler contains all commands.
    /// Every command gets executed via the <see cref="CommandHandlerBase.ExecuteCommand{T}"/>ExecuteCommand{TCommand}
    /// to catch any uncaught errors and display them through a messagebox.
    /// </summary>
    public class CommandHandler : CommandHandlerBase
    {
        // Constant command names to prevend typos.
        public const string GROUPNAME = "AcHelperDemo";
        public const string CMD_TESTOBJECTTYPES = "Demo_TestVariousTypeCheck";
        public const string CMD_OPENTOOLPALETTE = "Demo_OpenToolpalette";

        // assigning a command with the const variable.
        [CommandMethod(GROUPNAME, CMD_TESTOBJECTTYPES, CommandFlags.Modal)]
        public static void TestVariousTypeCheck()
        {
            // Execute the command with the CommandHandlerBase.ExecuteCommand<TCommand>()
            ExecuteCommand<TestVariousTypeCheckCommand>();
        }

        [CommandMethod(GROUPNAME, CMD_OPENTOOLPALETTE, CommandFlags.Modal)]
        public static void OpenToolpalette()
        {
            ExecuteCommand<OpenToolpaletteCommand>();
        }
    }
}
