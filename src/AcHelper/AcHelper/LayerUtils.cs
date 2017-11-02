using Autodesk.AutoCAD.DatabaseServices;
using BuerTech.Logger;

namespace AcHelper
{
    using Wrappers;
    /// <summary>
    /// 
    /// </summary>
    public static class LayerUtils
    {
        /// <summary>
        /// Resetlayers for unlocking, unfreeze and show all layers
        /// in the current document.
        /// </summary>
        public static void Resetlayers()
        {
            Database database = Active.Database;

            Active.Document.StartTransaction(tr =>
            {
                Transaction transaction = tr.Transaction;
                LayerTable layers = transaction.GetObject<LayerTable>(database.LayerTableId, OpenMode.ForRead);
                foreach (ObjectId layerID in layers)
                {
                    LayerTableRecord layer = transaction.GetObject<LayerTableRecord>(layerID, OpenMode.ForRead);
                    using (new WriteEnabler(layer))
                    {
                        if (layer.IsWriteEnabled)
                        {
                            layer.IsFrozen = false;
                            layer.IsLocked = false;
                            layer.IsOff = false;
                        }
                        else
                        {
                            Logger.WriteToLog(string.Concat("Could not reset layer: ", layer.Name), LogPrior.WARNING);
                        }
                    }
                }
            });
        }
    }
}
