using System;
using AcLog = AcHelper.Logging;

namespace AcHelper.Command
{
    /// <summary>
    /// This class handles as a base of every AutoCad command.
    /// If you use this feature, make sure you have created an instance of the <see cref="AcHelper.Logging"/>.LogWriter first.
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
                ExceptionHandler.ShowDialog(ex, true, true);

                if (AcLog.IsInitialized)
                {
                    AcLog.Logger.WriteToLog(ex, BuerTech.Utilities.Logger.LogPrior.Critical); 
                }
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
            ExecuteFromCommandLine(true, cmd, parameters);
        }

        /// <summary>
        /// Executes a command from the commandline.
        /// </summary>
        /// <param name="echo">true to echo command in commandline.</param>
        /// <param name="cmd">Command name.</param>
        /// <param name="parameters">Optional parameters.</param>
        public static void ExecuteFromCommandLine(bool echo, string cmd, params object[] parameters)
        {
            AcLog.Logger.Debug("Firing command from the commandline: " + cmd);

            // Prepare Command and potential parameters.
            string execute = cmd;
            execute += NEWLINE;
            if (parameters != null && parameters.Length > 0)
            {
                // if parameters are present
                // add every parameter to the command.
                foreach (var item in parameters)
                {
                    AcLog.Logger.Debug("Adding parameter: " + item.ToString());

                    // end parameter with a whitespace so it will be passed through.
                    execute += item.ToString() + WHITESPACE;
                }
            }

            try
            {
                AcLog.Logger.Debug("Executing ...");
                // Execute
                Active.Document.SendStringToExecute(execute, true, false, echo);

                AcLog.Logger.Debug("Command succeeded: " + cmd);
            }
            catch (Exception ex)
            {
                ExceptionHandler.ShowDialog(ex, true, true);
                if (AcLog.IsInitialized)
                {
                    AcLog.Logger.WriteToLog(ex, BuerTech.Utilities.Logger.LogPrior.Critical);
                }
            }
        }
    }
}
