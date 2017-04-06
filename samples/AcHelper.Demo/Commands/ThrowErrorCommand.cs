using AcHelper;
using AcHelper.Commands;
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
