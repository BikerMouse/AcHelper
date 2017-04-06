using AcHelper.Commands;
using Autodesk.AutoCAD.DatabaseServices;
using Autodesk.AutoCAD.EditorInput;

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
                Logger.WriteToLog(ex);
                ExceptionHandler.WriteToCommandLine(ex);
            }
        }

        #endregion
    }
}
