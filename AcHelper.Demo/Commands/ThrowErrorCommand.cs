using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

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

        public bool CanExecute()
        {
            throw new NotImplementedException();
        }

        #endregion
    }
}
