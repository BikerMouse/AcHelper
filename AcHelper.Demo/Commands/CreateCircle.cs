using AcHelper.Commands;
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
            double radius;

            PromptDoubleOptions optRad = new PromptDoubleOptions("Insert the radius for the circle");
            PromptDoubleResult resRad = ed.GetDouble(optRad);
            if (resRad.Status == PromptStatus.OK)
            {
                radius = resRad.Value;

                Utilities.DrawCircle(document, radius);
            }
            else if (resRad.Status == PromptStatus.Cancel)
            {
                Active.WriteMessage("User canceled Command.");
            }
        }
        #endregion
    }
}
