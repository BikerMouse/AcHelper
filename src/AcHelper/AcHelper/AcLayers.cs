using System;

using Autodesk.AutoCAD.ApplicationServices;
using Autodesk.AutoCAD.DatabaseServices;
using AcHelper.Wrappers;

namespace AcHelper
{
    public static class AcLayers
    {
        /// <summary>
        /// Resetlayers for unlocking, unfreeze and show all layers
        /// </summary>
        public static void Resetlayers()
        {
            Document document = Active.Document;
            Database database = document.Database;

            Common.UsingTransaction(Active.Document, t =>
            {
                Transaction transaction = t.Transaction;
                LayerTable layers = (LayerTable)transaction.GetObject(database.LayerTableId, OpenMode.ForRead);
                try
                {
                    foreach (ObjectId layerID in layers)
                    {
                        LayerTableRecord layer = transaction.GetObject<LayerTableRecord>(layerID, OpenMode.ForRead);
                        using (new WriteEnabler(document, layer))
                        {
                            if (layer.IsWriteEnabled)
                            {
                                if (layer.IsFrozen) { layer.IsFrozen = false; }
                                if (layer.IsLocked) { layer.IsLocked = false; }
                                if (layer.IsOff) { layer.IsOff = false; }
                            }
                            else
                            {
                                throw new Exception("ResetLayersError");
                            }
                        }
                    }
                }
                catch(Exception ex)
                {
                    Active.WriteMessage(ex.Message);
                    throw;
                }
            });
        }

    }
}
