using AcHelper.Command;
using Autodesk.AutoCAD.Runtime;

namespace AcHelper.DemoApp.CAD
{
    using Commands;

    /// <summary>
    /// The command handler contains all commands.
    /// Every command gets executed via the <see cref="CommandHandlerBase.ExecuteCommand{T}"/>ExecuteCommand{TCommand}
    /// to catch any uncaught errors and display them through a messagebox.
    /// </summary>
    internal class CommandHandler : CommandHandlerBase
    {
        // Constant command names to prevend typos.
        public const string CMD_TESTOBJECTTYPES = "TestVariousTypeCheck";

        // assigning a command with the const variable.
        [CommandMethod(CMD_TESTOBJECTTYPES)]
        public static void TestVariousTypeCheck()
        {
            // Execute the command with the CommandHandlerBase.ExecuteCommand<TCommand>()
            ExecuteCommand<TestVariousTypeCheckCommand>();
        }
    }
}
