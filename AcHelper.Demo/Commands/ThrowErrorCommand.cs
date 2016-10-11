using AcHelper;
using AcHelper.Command;
using System;

namespace AcHelper.Demo.Commands
{
    public class ThrowErrorCommand : IAcadCommand
    {
        #region IAcadCommand Members

        public void Execute()
        {
            try
            {
                object o = null;
                o.ToString();
            }
            catch (Exception e)
            {
                Logging.Instance.WriteToLog(e, BuerTech.Utilities.Logger.LogPrior.Critical);
                ExceptionHandler.WriteToCommandLine(e);
                throw;
            }
        }

        public bool CanExecute()
        {
            throw new NotImplementedException();
        }

        #endregion
    }
}
