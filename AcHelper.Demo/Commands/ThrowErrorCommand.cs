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
            object o = null;
            o.ToString();
        }
        #endregion
    }
}
