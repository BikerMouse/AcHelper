using System;
using System.Windows.Input;

namespace AcHelper.Command
{
    public class CommandHandlerBase
    {
        private const string WHITESPACE = " ";
        private const string NEWLINE = "\n";

        #region Command Executer ...
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

        public static void ExecuteFromCommandLine(string cmd, params object[] parameters)
        {
            // Prepare Command and potential parameters.
            cmd += NEWLINE;
            if (parameters != null && parameters.Length > 0)
            {
                foreach (var item in parameters)
                {
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
                ExceptionHandler.ShowDialog(ex, cmdLine: true);
            }
        }
    }
}
