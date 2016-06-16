
using Autodesk.AutoCAD.Runtime;
using System;

namespace AcHelper
{
#if DEBUG
    public class Init : IExtensionApplication
    {
        public void Initialize()
        {
            Active.Document.UsingModelSpace((t, ms) => DrawCircles(t, ms, 5, 10), "DrawCircles");

            Active.Database.ForEach<Autodesk.AutoCAD.DatabaseServices.Circle>(c =>
            {
                if (c != null)
                {
                    using (AcHelper.Utilities.WriteEnabler we = new Utilities.WriteEnabler(c))
                    {
                        if (c.IsWriteEnabled)
                        {
                            c.ColorIndex = 1;
                        }
                    }
                }
            });

            Common.UsingModelSpace((t, m) => DrawCircles(t, m, 2, 20), "DrawCirclesPart2");
        }

        public void Terminate()
        { }

        private void DrawCircles(Utilities.AcTransaction tr, Autodesk.AutoCAD.DatabaseServices.BlockTableRecord modelspace, int amount, double radius)
        {
            Autodesk.AutoCAD.DatabaseServices.Transaction t = tr.Transaction;
            Autodesk.AutoCAD.EditorInput.Editor ed = tr.Document.Editor;

            Autodesk.AutoCAD.EditorInput.PromptPointOptions opt = new Autodesk.AutoCAD.EditorInput.PromptPointOptions("Select a location to create a circle");
            try
            {
                for (int i = 0; i < amount; i++)
                {
                    Autodesk.AutoCAD.EditorInput.PromptPointResult result = ed.GetPoint(opt);
                    if (result.Status == Autodesk.AutoCAD.EditorInput.PromptStatus.OK)
                    {
                        Autodesk.AutoCAD.DatabaseServices.Circle circle = new Autodesk.AutoCAD.DatabaseServices.Circle(result.Value, Autodesk.AutoCAD.Geometry.Vector3d.ZAxis, radius);
                        circle.ColorIndex = 2;
                        modelspace.AppendEntity(circle);
                        t.AddNewlyCreatedDBObject(circle, true);

                        t.TransactionManager.QueueForGraphicsFlush();
                        Autodesk.AutoCAD.Internal.Utils.FlushGraphics();
                    }
                }
            }
            catch (System.Exception ex)
            {
                Active.WriteMessage(ex.Message);
            }
        }
    }
#endif
}
