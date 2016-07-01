using Autodesk.AutoCAD.ApplicationServices;
using Autodesk.AutoCAD.DatabaseServices;
using Autodesk.AutoCAD.Runtime;
using System;

namespace AcHelper.Utilities
{
    public class XRecordHandler
    {
        #region Fields ...
        private Document _document = null;
        private Database _database = null;
        private ObjectId _nodId = ObjectId.Null;
        #endregion

        #region Ctor ...
        public XRecordHandler()
            : this(Active.Document)
        { }
        public XRecordHandler(Document doc)
        {
            if (doc != null && doc.IsActive)
            {
                _document = doc;
                _database = doc.Database;
                _nodId = _database.NamedObjectsDictionaryId;
            }
        }
        #endregion

        #region Properties ...
        public DBDictionary GetNamedObjectsDictionary(string key)
        {
            DBDictionary nod = null;                // Named Objects Dictionary
            DBDictionary nod_collection = null;     // Named Objects Dictionary Collection

            try
            {
                Common.UsingTransaction(_document, tr =>
                {
                    Transaction t = tr.Transaction;
                    nod_collection = t.GetObject<DBDictionary>(_nodId, OpenMode.ForRead);

                    if (nod_collection.Contains(key))
                    {
                        ObjectId oid = nod_collection.GetAt(key);
                        nod = t.GetObject<DBDictionary>(oid, OpenMode.ForRead);
                    }
                    else
                    {
                        using (WriteEnabler we = new WriteEnabler(nod_collection))
                        {
                            if (nod_collection.IsWriteEnabled)
                            {
                                nod = new DBDictionary();
                                if (nod != null)
                                {
                                    nod_collection.SetAt(key, nod);
                                    t.AddNewlyCreatedDBObject(nod, true);
                                }
                            }
                        }
                    }
                });
            }
            catch (System.Exception ex)
            {
                string err_message = string.Format("DBDictionary named '{0}' could not be found.", key);
                throw new XRecordHandlerException(err_message, ex);
            }

            return nod;
        }
        #endregion

        #region Get ...
        /// <summary>
        /// Gets the data from an Xrecord.
        /// </summary>
        /// <param name="dictionaryName">Name of Named Objects Dictionary</param>
        /// <param name="xKey">Key of Xrecord</param>
        /// <returns>ResultBuffer with data or Null if nothing found.</returns>
        public ResultBuffer GetXrecord(string dictionaryName, string xKey)
        {
            ResultBuffer result = null;

            try
            {
                if (_document != null)
                {
                    Common.UsingTransaction(_document, t =>
                    {
                        using (DBDictionary nod = GetNamedObjectsDictionary(dictionaryName))
                        {
                            if (nod != null && nod.Contains(xKey))
                            {
                                ObjectId oid = nod.GetAt(xKey);
                                using (Xrecord xrec = t.Transaction.GetObject<Xrecord>(oid, OpenMode.ForRead))
                                {
                                    result = xrec.Data;
                                }
                            }
                            else if (!nod.Contains(xKey))
                            {
                                string err_message = string.Format("Xrecord '{0}' not found in Named Objects Dictionary '{1}'", xKey, dictionaryName);
                                throw new XRecordHandlerException(dictionaryName, xKey, err_message, ErrorCode.XrecordNotFound);
                            }
                        }
                    });
                }
            }
            catch (System.Exception ex)
            {
                string err_message = string.Format("Unexpected error occured while retrieving xRecord '{0}' from Named Objects Dictionary '{1}'.", xKey, dictionaryName);
                throw new XRecordHandlerException(dictionaryName, xKey, err_message, ex, ErrorCode.XrecordNotFound);
            }

            return result;
        }
        public ResultBuffer GetEntityXrecord(ObjectId oid, string dictionaryName, string xKey)
        {
            ResultBuffer resbuf = null;

            if (_document != null)
            {
                try
                {
                    Common.UsingTransaction(_document, tr =>
                    {
                        Transaction t = tr.Transaction;
                        Entity ent = t.GetObject<Entity>(oid, OpenMode.ForRead);
                        if (ent != null)
                        {

                        }
                        else
                        {
                            string err_message = string.Format("Object is not an entity");
                            throw new XRecordHandlerException(err_message, ErrorCode.NotAnEntity);
                        }

                    });
                }
                catch (System.Exception ex)
                {
                string err_message = string.Format("Unexpected error occured while retrieving xRecord '{0}' from Named Objects Dictionary '{1}'.", xKey, dictionaryName);
                throw new XRecordHandlerException(dictionaryName, xKey, err_message, ex, ErrorCode.XrecordNotFound);
                }
                
            }

            return resbuf;
        }
        #endregion

        #region Update ...
        public bool UpdateXrecord(string dictionaryName, string xKey, ResultBuffer resbuf)
        {
            bool result = false;
            string err_message = string.Empty;

            try
            {
                if (_document != null)
                {
                    Common.UsingTransaction(_document, tr =>
                    {
                        Transaction t = tr.Transaction;
                        using (DBDictionary nod = GetNamedObjectsDictionary(dictionaryName))
                        {
                            if (nod != null)
                            {
                                using (WriteEnabler we = new WriteEnabler(nod))
                                {
                                    if (nod.IsWriteEnabled)
                                    {
                                        if (nod.Contains(xKey))
                                        {
                                            ObjectId oid = nod.GetAt(xKey);
                                            using (Xrecord xrec = t.GetObject<Xrecord>(oid, OpenMode.ForRead))
                                            {
                                                if (xrec != null)
                                                {
                                                    using (WriteEnabler xWe = new WriteEnabler(xrec))
                                                    {
                                                        if (xrec.IsWriteEnabled)
                                                        {
                                                            xrec.Data = resbuf;
                                                            result = true;
                                                        }
                                                    }
                                                }
                                            }
                                        }
                                        else
                                        {
                                            using (Xrecord xRec = new Xrecord())
                                            {
                                                if (xRec != null)
                                                {
                                                    xRec.Data = resbuf;
                                                    nod.SetAt(xKey, xRec);
                                                    t.AddNewlyCreatedDBObject(xRec, true);
                                                    result = true;
                                                }
                                            }
                                        }
                                    }
                                    else
                                    {
                                        err_message = string.Format("Could not open Named Objects Dictionary '{0}' to update Xrecord '{1}'.", dictionaryName, xKey);
                                        throw new XRecordHandlerException(dictionaryName, xKey, err_message);
                                    }
                                }
                            }
                        }
                    });
                }
            }
            catch (System.Exception ex)
            {
                err_message = string.Format("Could not update Xrecord '{0}' in Named Objects Dictionary '{1}'.", xKey, dictionaryName);
                throw new XRecordHandlerException(dictionaryName, xKey, err_message, ex);
            }

            return result;
        }
        #endregion

        #region Remove ...
        public bool RemoveXrecord(string dictionaryName, string xKey)
        {
            bool result = false;
            string err_message = string.Empty;

            try
            {
                if (_document != null)
                {
                    Common.UsingTransaction(_document, tr =>
                    {
                        Transaction t = tr.Transaction;
                        using (DBDictionary nod = GetNamedObjectsDictionary(dictionaryName))
                        {
                            if (nod != null)
                            {
                                if (nod.Contains(xKey))
                                {
                                    using (WriteEnabler we = new WriteEnabler(nod))
                                    {
                                        if (nod.IsWriteEnabled)
                                        {
                                            nod.Remove(xKey);
                                            result = true;
                                        }
                                        else
                                        {
                                            err_message = string.Format("Could not open Named Objects Dictionary '{0}' for write", dictionaryName);
                                            throw new XRecordHandlerException(dictionaryName, xKey, err_message);
                                        }
                                    }
                                }
                                else
                                {
                                    result = true;
                                }
                            }
                            else
                            {
                                err_message = string.Format("Could not get Named Objects Dictionary '{0}'", dictionaryName);
                                throw new XRecordHandlerException(dictionaryName, xKey, err_message);
                            }
                        }
                    });
                }
            }
            catch (System.Exception ex)
            {
                err_message = string.Format("Could not remove Xrecord '{0}' from Named Objects Dictionary '{1}'", xKey, dictionaryName);
                throw new XRecordHandlerException(dictionaryName, xKey, err_message, ex);
            }

            return result;
        }
        #endregion
    }

    public enum ErrorCode
    {
        Error,
        NodNotFound,
        XrecordNotFound,
        NodLockedForWrite,
        NotAnEntity
    }

    [System.Serializable]
    public class XRecordHandlerException : System.Exception
    {
        
        private string _key;
        private string _dictionary_name;
        private ErrorCode _error_code;

        public string Key
        {
            get { return _key; }
        }
        public string DictionaryName
        {
            get { return _dictionary_name; }
        }
        public ErrorCode ErrorCode
        {
            get { return _error_code; }
        }

        public XRecordHandlerException(string message, ErrorCode errorCode)
            : base(message)
        {
            _error_code = errorCode;
        }
        public XRecordHandlerException(string dictionaryName, string xKey, string message, ErrorCode errorCode = Utilities.ErrorCode.Error) : base(message)
        {
            _dictionary_name = dictionaryName;
            _key = xKey;
            _error_code = errorCode;
        }
        public XRecordHandlerException(string dictionaryName, string xKey, string message, System.Exception inner, ErrorCode errorCode = Utilities.ErrorCode.Error)
            : base(message, inner)
        {
            _key = xKey;
            _dictionary_name = dictionaryName;
            _error_code = errorCode;
        }


        public XRecordHandlerException() { }
        public XRecordHandlerException(string message) : base(message) { }
        public XRecordHandlerException(string message, System.Exception inner) : base(message, inner) { }
        protected XRecordHandlerException(
          System.Runtime.Serialization.SerializationInfo info,
          System.Runtime.Serialization.StreamingContext context)
            : base(info, context) { }
    }
}
