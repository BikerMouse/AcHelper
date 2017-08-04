using Autodesk.AutoCAD.Colors;
using Autodesk.AutoCAD.DatabaseServices;

namespace AcHelper
{
    public static class AcObjects
    {

        public static bool ResetProperties(Entity entity, string layer)
        {
            Color newColor = Color.FromColorIndex(ColorMethod.ByAci, 256);
            entity.Color = newColor;
            entity.Layer = layer;
            entity.Linetype = "ByLayer";

            return true;
        }
    }
}
