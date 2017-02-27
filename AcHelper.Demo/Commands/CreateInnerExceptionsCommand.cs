using AcHelper.Commands;
using AcHelper.Exceptions;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AcHelper.Demo.Commands
{
    public class CreateInnerExceptionsCommand : IAcadCommand
    {

        #region IAcadCommand Members

        public void Execute()
        {
            TransactionException first = new TransactionException("Oeps, Transaction went wrong.");
            Exception outer = new Exception("Shit happens, read innerexception.", first);

            throw outer;

        }

        #endregion
    }
}
