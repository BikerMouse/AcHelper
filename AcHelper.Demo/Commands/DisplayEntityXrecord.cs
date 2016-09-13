using AcHelper.Command;
using AcHelper.Utilities;
using Autodesk.AutoCAD.DatabaseServices;
using Autodesk.AutoCAD.EditorInput;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace AcHelper.Demo.Commands
{
    public class DisplayEntityXrecord : IAcadCommand
    {
        #region IAcadCommand Members

        public void Execute()
        {
            Editor ed = Active.Editor;
            try
            {
                PromptEntityOptions opt = new PromptEntityOptions("\nSelect an entity to get xRecords from");
                PromptEntityResult res = ed.GetEntity(opt);
                if (res.Status == PromptStatus.OK)
                {
                    ObjectId oid = res.ObjectId;
                    Utilities.DisplayEntityXrecord(oid, "DEMO_XRECORD");
                }
            }
            catch (System.Exception ex)
            {
                Active.WriteMessage(ex.Message);
                if (ex.InnerException != null)
                {
                    Active.WriteMessage(ex.InnerException.Message);
                }
            }
        }

        #endregion
    }
}
