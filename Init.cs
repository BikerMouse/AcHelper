
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
                    c.UpgradeOpen();
                    c.ColorIndex = 1;
                }
            });
        }

        public void Terminate()
        { }

        private void DrawCircles(Autodesk.AutoCAD.DatabaseServices.Transaction t, Autodesk.AutoCAD.DatabaseServices.BlockTableRecord modelspace, int amount, double radius)
        {
            Autodesk.AutoCAD.ApplicationServices.Document doc = Active.Document;
            Autodesk.AutoCAD.EditorInput.Editor ed = doc.Editor;
            Autodesk.AutoCAD.EditorInput.PromptPointOptions opt = new Autodesk.AutoCAD.EditorInput.PromptPointOptions("Select a location to create a circle");

            doc.TransactionManager.EnableGraphicsFlush(true);

            if (!modelspace.IsWriteEnabled)
            {
                modelspace.UpgradeOpen();
            }

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

                        doc.TransactionManager.QueueForGraphicsFlush();
                        Autodesk.AutoCAD.Internal.Utils.FlushGraphics();
                    }
                }
            }
            catch (System.Exception ex)
            {
                Active.WriteMessage(ex.Message);
            }
            finally
            {
                if (modelspace.IsWriteEnabled)
                {
                    modelspace.DowngradeOpen();                    
                }
            }
        }
    }
#endif
}
