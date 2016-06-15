using Autodesk.AutoCAD.ApplicationServices;
using Autodesk.AutoCAD.DatabaseServices;
using System;
using AcadTransaction = Autodesk.AutoCAD.DatabaseServices.Transaction;

namespace AcHelper.Utilities
{
    public class AcTransaction : IDisposable
    {
        #region Fields ...
        // Statics
        /// <summary>
        /// The current transaction.
        /// </summary>
        private static AcadTransaction _transaction = null;
        private static Document _document = null;
        private static Database _db = null;

        // Non-statics
        /// <summary>
        /// Is the transaction started and does it need a commit or abort.
        /// </summary>
        private bool _started = false;
        private BlockTableRecord _model_space = null;
        #endregion

        #region Ctor ...
        /// <summary>
        /// Starts a transaction if needed
        /// </summary>
        /// <param name="dbobject"></param>
        public AcTransaction()
        {
            try
            {
                // is a transaction already running
                if (_transaction == null)
                {
                    _document = Active.Document;
                    if (_document != null)
                    {
                        // no current transaction running; create one
                        _db = _document.Database;
                        _transaction = _document.TransactionManager.StartTransaction();
                        _model_space = GetModelSpace();
                        _started = true;
                    }
                }
            }
            catch (System.Exception)
            {
                throw;
            }
        }
        #endregion

        #region Properties ...
        /// <summary>
        /// <see cref="Autodesk.AutoCAD.DatabaseServices.Database.Transaction"/>
        /// </summary>
        public AcadTransaction Transaction
        {
            get { return _transaction; }
        }
        /// <summary>
        /// Gets the ModelSpace BlockTableRecord.
        /// </summary>
        public BlockTableRecord ModelSpace
        {
            get { return _model_space; }
        }
        /// <summary>
        /// Is the transaction started and does it need a commit or abort.
        /// </summary>
        public bool IsStarted
        {
            get { return _started; }
        }
        #endregion

        #region Methods ...
        /// <summary>
        /// Commit the current transaction
        /// </summary>
        public void Commit()
        {
            if (_transaction != null)
            {
                _transaction.Commit();
                _transaction.Dispose();
                _transaction = null;
            }
            _started = false;
        }
        /// <summary>
        /// Abort the current transaction
        /// </summary>
        public void Abort()
        {
            if (_transaction != null)
            {
                _transaction.Abort();
                _transaction.Dispose();
                _transaction = null;
            }
        }
        #endregion

        #region Helpers ...
        private BlockTableRecord GetModelSpace()
        {
            BlockTable block_table = _transaction.GetObject(_db.BlockTableId, OpenMode.ForRead) as BlockTable;
            BlockTableRecord model_space = _transaction.GetObject(block_table[BlockTableRecord.ModelSpace], OpenMode.ForRead) as BlockTableRecord;

            return model_space;
        }
        #endregion

        #region Dispose ...
        /// <summary>
        /// closes the transaction
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
                if (_started)
                {
                    Commit();
                } //if
            } //if
        }
        #endregion
    }
}
