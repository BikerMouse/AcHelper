using Autodesk.AutoCAD.DatabaseServices;
using Autodesk.AutoCAD.Runtime;

namespace NedGraphics.AutoCad.Test.CAD
{
    /// <summary>
    /// Contains RXClasses for the best performance with comparing to other objects.
    /// </summary>
    public class EntityTypes
    {
        public static RXClass Circle = RXObject.GetClass(typeof(Circle));
        public static RXClass Polyline = RXObject.GetClass(typeof(Polyline));
        public static RXClass DBText = RXObject.GetClass(typeof(DBText));
        public static RXClass MText = RXObject.GetClass(typeof(MText));
        public static RXClass BlockReference = RXObject.GetClass(typeof(BlockReference));
        public static RXClass LayerTableRecord = RXObject.GetClass(typeof(LayerTableRecord));
    }
}
