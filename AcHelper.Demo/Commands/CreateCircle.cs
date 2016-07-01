using AcHelper.Command;
using Autodesk.AutoCAD.ApplicationServices;
using Autodesk.AutoCAD.DatabaseServices;
using Autodesk.AutoCAD.EditorInput;
using Autodesk.AutoCAD.Geometry;

namespace AcHelper.Demo.Commands
{
    public class CreateCircle : IAcadCommand
    {
        #region IAcadCommand Members

        public void Execute()
        {
            Document document = Active.Document;
            Editor ed = document.Editor;
            Point3d location;
            double radius;

            PromptPointOptions optLoc = new PromptPointOptions("Select location for circle");
            PromptDoubleOptions optRad = new PromptDoubleOptions("Insert the radius for the circle");

            PromptPointResult res = ed.GetPoint(optLoc);
            if (res.Status != PromptStatus.OK)
            {
                return;
            }
            location = res.Value;
            PromptDoubleResult resRad = ed.GetDouble(optRad);

            if (resRad.Status != PromptStatus.OK)
            {
                return;
            }
            radius = resRad.Value;

            document.UsingModelSpace((tr, ms) =>
            {
                Transaction t = tr.Transaction;
                Circle c = new Circle(location, Vector3d.ZAxis, radius);
                c.ColorIndex = 3;
                ms.AppendEntity(c);
                t.AddNewlyCreatedDBObject(c, true);
            });

        }

        public bool CanExecute()
        {
            return true;
        }

        #endregion
    }
}
