using AcHelper.Commands;
using Autodesk.AutoCAD.DatabaseServices;
using Autodesk.AutoCAD.EditorInput;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AcHelper.Demo.Commands
{
    public class AddXrecordToEntity : IAcadCommand
    {
        #region IAcadCommand Members

        public void Execute()
        {
            Editor ed = Active.Editor;
            try
            {
                PromptEntityOptions opt = new PromptEntityOptions("\nSelect an entity to add xRecord");
                PromptEntityResult res = ed.GetEntity(opt);
                if (res.Status == PromptStatus.OK)
                {
                    ObjectId oid = res.ObjectId;
                    ResultBuffer resbuf = new ResultBuffer();
                    resbuf.Add(new TypedValue((int)DxfCode.Text, "Hello AutoCAD MAP 3D"));
                    Utilities.SetEntityXrecord(oid, "DEMO_XRECORD", resbuf);
                }
                //Utilities.AddTextDataToEntity("DEMO_XRECORD", "Hello AutoCAD MAP 3D");
            }
            catch (System.Exception ex)
            {
                ExceptionHandler.WriteToCommandLine(ex);
            }            
        }

        #endregion
    }
}
