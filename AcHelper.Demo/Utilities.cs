using Autodesk.AutoCAD.ApplicationServices;
using Autodesk.AutoCAD.DatabaseServices;
using Autodesk.AutoCAD.EditorInput;
using Autodesk.AutoCAD.Geometry;

namespace AcHelper.Demo
{
    internal class Utilities
    {
        internal static void DrawCircle(Document document, double radius)
        {
            Editor ed = document.Editor;
            PromptPointOptions opt = new PromptPointOptions("\nSelect a location to draw the circle");

            document.UsingModelSpace((tr,ms) =>
            {
                Transaction t = tr.Transaction;
                PromptPointResult res = ed.GetPoint(opt);
                if (res.Status == PromptStatus.OK)
                {
                    Circle c = new Circle(res.Value, Vector3d.ZAxis, radius);
                    c.ColorIndex = 3;
                    ms.AppendEntity(c);
                    t.AddNewlyCreatedDBObject(c, true);
                }
            });
        }
    }
}
