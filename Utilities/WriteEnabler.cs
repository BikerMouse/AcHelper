using Autodesk.AutoCAD.ApplicationServices;
using Autodesk.AutoCAD.DatabaseServices;
using System;

namespace AcHelper.Utilities
{
    public class WriteEnabler : IDisposable
    {
        #region Fields ...
        private bool _is_upgraded = false;
        private ObjectId _layer_id = ObjectId.Null;
        private DBObject _db_object = null;
        private Document _document = null;
        #endregion

        #region Ctor ...
        public WriteEnabler(DBObject dbObject)
            : this(Active.Document, dbObject)
        { }
        public WriteEnabler(Document doc, DBObject dbObject)
        {
            if (doc != null)
            {
                _document = doc;
                if (dbObject != null)
                {
                    _db_object = dbObject;
                    // Make write enabled if it's not enabled yet.
                    if (!dbObject.IsWriteEnabled)
                    {
                        try
                        {
                            DocumentLockMode lock_mode = doc.LockMode();
                            if (lock_mode == DocumentLockMode.Write || lock_mode == DocumentLockMode.ExclusiveWrite)
                            {
                                // is the object an entity?
                                // if yes, unlock layer.
                                Entity entity = dbObject as Entity;
                                if (entity != null)
                                {
                                    UnlockLayer(entity.LayerId);
                                }
                                // make object writable
                                _db_object.UpgradeOpen();
                                _is_upgraded = true;
                            }
                            else
                            {
                                Active.WriteMessage("\n*** Document not locked ***");
                            }
                        }
                        catch (Autodesk.AutoCAD.Runtime.Exception)
                        {
                            throw;
                        }
                    }
                }
            }
        }
        #endregion

        #region Dispose ...
        /// <summary>
        /// Calls DowngradeOpen if necessary
        /// </summary>
        public void Dispose()
        {
            Dispose(true);
            GC.SuppressFinalize(this);
        }

        /// <summary>
        /// disposes the object
        /// </summary>
        /// <param name="disposing">the disposing status input parameter</param>
        protected virtual void Dispose(bool disposing)
        {
            if (disposing)
            {
                if (_db_object != null)
                {
                    if (_is_upgraded)
                    {
                        _db_object.DowngradeOpen();
                        _is_upgraded = false;
                    }

                    if (_layer_id != ObjectId.Null)
                    {
                        // layer was locked, lock it again
                        LockLayer(_layer_id);
                    } //if
                } //if
            } //if
        }

        private void LockLayer(ObjectId layerId)
        {
            if (layerId != ObjectId.Null)
            {
                using (AcTransaction tr = new AcTransaction())
                {
                    Transaction t = tr.Transaction;
                    using (var layer_record = t.GetObject<LayerTableRecord>(layerId, OpenMode.ForWrite))
                    {
                        if (layer_record != null)
                        {
                            if (!layer_record.IsLocked)
                            {
                                _layer_id = ObjectId.Null;
                                layer_record.IsLocked = true;
                            }
                        }
                    }
                }
            }
        }
        private void UnlockLayer(ObjectId layerId)
        {
            if (layerId != ObjectId.Null)
            {
                using (AcTransaction tr = new AcTransaction())
                {
                    Transaction t = tr.Transaction;
                    using (var layer_record = t.GetObject<LayerTableRecord>(layerId, OpenMode.ForWrite))
                    {
                        if (layer_record != null)
                        {
                            if (layer_record.IsLocked)
                            {
                                _layer_id = layerId;
                                layer_record.IsLocked = false;
                            }
                        }
                    }
                }
            }
        }
        #endregion
    }
}
