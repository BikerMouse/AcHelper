using AcHelper.Commands;
using Autodesk.AutoCAD.EditorInput;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AcHelper.Demo.Commands
{
    public class AddXrecordToDocument : IAcadCommand
    {

        #region IAcadCommand Members

        public void Execute()
        {
            Editor ed = Active.Editor;
            try
            {
                PromptStringOptions opt = new PromptStringOptions("Give message to save as Xrecord");
                opt.AllowSpaces = true;
                PromptResult res = ed.GetString(opt);

                if (res.Status == PromptStatus.OK)
                {
                    Utilities.AddXrecordToDocument("DEMO_NOD", "DEMO_KEY", res.StringResult);
                }
            }
            catch (System.Exception ex)
            {
                ExceptionHandler.WriteToCommandLine(ex);
            }
        }

        #endregion
    }
}
