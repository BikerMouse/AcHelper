using Autodesk.Gis.Map;
using System;
using System.Collections.Generic;
using System.Text;

namespace AcHelper
{
    public class ExceptionHandler
    {
        private const string START_EXCEPTION = "***  Exception  ***";
        private const string END_EXCEPTION = "*** End Exception ***";

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
