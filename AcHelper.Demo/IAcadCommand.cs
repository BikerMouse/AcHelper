using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AcHelper.Demo
{
    public interface IAcadCommand
    {
        void Execute();
        bool CanExecute();
    }
}
