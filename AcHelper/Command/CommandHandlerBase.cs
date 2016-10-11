using System;
using System.Windows.Input;

namespace AcHelper.Command
{
    /// <summary>
    /// This class handles as a base of every AutoCad command.
    /// </summary>
    public class CommandHandlerBase
    {
        private const string WHITESPACE = " ";
        private const string NEWLINE = "\n";

        #region Command Executer ...
        /// <summary>
        /// Executes the given <see cref="IAcadCommand"/>class as a command.
        /// When an Unhandled exceptions occurs, it will be catched and thrown as a dialog.
        /// This way the the change for an Acad crash stays minimal.
        /// </summary>
        /// <typeparam name="T">Class based on interface IAcadCommand</typeparam>
        public static void ExecuteCommand<T>() where T : IAcadCommand
        {
            try
            {
                var cmd = Activator.CreateInstance<T>();
                cmd.Execute();
            }
            catch (System.Exception ex)
            {
                ExceptionHandler.ShowDialog(ex, cmdLine: true);
            }
        }
        #endregion

        /// <summary>
        /// Executes a command from the commandline.
        /// </summary>
        /// <param name="cmd">Command name.</param>
        /// <param name="parameters">Optional parameters.</param>
        public static void ExecuteFromCommandLine(string cmd, params object[] parameters)
        {
            // Prepare Command and potential parameters.
            cmd += NEWLINE;
            if (parameters != null && parameters.Length > 0)
            {
                // if parameters are present
                // add every parameter to the command.
                foreach (var item in parameters)
                {
                    // end parameter with a whitespace so it will be passed through.
                    cmd += item.ToString() + WHITESPACE;
                }
            }

            try
            {
                // Execute
                Active.Document.SendStringToExecute(cmd, true, false, true);
            }
            catch (Exception ex)
            {
                ExceptionHandler.ShowDialog(ex, true, true);
            }
        }
    }
}
