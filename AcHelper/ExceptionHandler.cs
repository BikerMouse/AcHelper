using System;
using System.Text;

namespace AcHelper
{
    public class ExceptionHandler
    {
        private const string START_EXCEPTION = "***  Exception  ***";
        private const string END_EXCEPTION = "*** End Exception ***";

        /// <summary>
        /// Writes the exception message to the commandline.
        /// </summary>
        /// <param name="ex">Exception</param>
        /// <param name="inner">true if messages from innerexceptions also need to be written.</param>
        public static void WriteToCommandLine(Exception ex, bool inner = true)
        {
            StartException();

            Active.WriteMessage(ex.Message);

            if (inner)
            {
                Active.WriteMessage(GetInnerExceptions(ex));
            }

            EndException();
        }

        /// <summary>
        /// Displays the exception message in a dialog.
        /// </summary>
        /// <param name="ex"></param>
        /// <param name="inner">true if messages from innerexceptions also need to be displayed.</param>
        /// <param name="cmdLine">true if the message also needs to be written on the commandline.</param>
        public static void ShowDialog(Exception ex, bool inner = true, bool cmdLine = false)
        {
            StringBuilder sb = new StringBuilder(ex.Message);

            if (inner)
            {
                sb.Append("\n" + GetInnerExceptions(ex));
            }

            Autodesk.AutoCAD.ApplicationServices.Application.ShowAlertDialog(sb.ToString());

            if (cmdLine)
            {
                WriteToCommandLine(ex, inner);
            }
        }

        #region Private Methods ...
        private static string GetInnerExceptions(Exception ex)
        {
            StringBuilder sb = new StringBuilder();

            Exception inner = ex.InnerException;
            while (inner != null)
            {
                sb.Append(inner.Message + "\n");
                inner = inner.InnerException;
            }

            return sb.ToString();
        }
        private static void StartException()
        {
            Active.WriteMessage(START_EXCEPTION);
        }
        private static void EndException()
        {
            Active.WriteMessage(END_EXCEPTION);
        } 
        #endregion
    }
}
