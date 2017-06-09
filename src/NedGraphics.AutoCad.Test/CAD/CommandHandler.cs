using AcHelper.Commands;
using Autodesk.AutoCAD.Runtime;

namespace NedGraphics.AutoCad.Test.CAD
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
        internal const string CMD_HELLOACADWORLD = "HelloAcadWorld";

        // assigning a command with the const variable.
        [CommandMethod(CMD_HELLOACADWORLD)]
        internal static void HelloAcadWorld()
        {
            // Execute the command with the CommandHandlerBase.ExecuteCommand<TCommand>()
            ExecuteCommand<HelloAcadWorldCommand>();
        }
    }
}
