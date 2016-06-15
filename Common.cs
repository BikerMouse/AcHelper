using AcHelper.Exceptions;
using AcHelper.Utilities;
using Autodesk.AutoCAD.ApplicationServices;
using Autodesk.AutoCAD.DatabaseServices;
using System;

namespace AcHelper
{
    public static class Common
    {
        /// <summary>
        /// Wrapper for the active Database's TransactionManager.
        /// The wrapper locks the document before starting a transaction!
        /// </summary>
        /// <param name="action">Method which uses the Transaction.</param>
        /// <param name="commandName">Name of action the TransactionManager is executing.</param>
        /// <param name="description">Description of what the TransactionManager is executing.</param>
        /// <exception cref="TransactionException"/>
        public static void UsingTransaction(Action<Transaction> action, string commandName = "")
        {
            commandName = commandName == "" ? "Acad_Transaction" : commandName;

            try
            {
                using (DocumentLock doclock = Active.Document.LockDocument(DocumentLockMode.Write, commandName, commandName, true))
                {
                    using (AcTransaction tr = new AcTransaction())
                    {
                        Transaction t = tr.Transaction;
                        action(t);
                    }
                }
            }
            catch (System.Exception ex)
            {
                throw new TransactionException(ex.Message, commandName, ex);
            }
        }
        /// <summary>
        /// Wrapper for the active Document's ModelSpace.
        /// The wrapper locks the document before starting a transaction!
        /// </summary>
        /// <param name="action">Method which uses the modelspace and transaction</param>
        /// <exception cref="TransactionException"/>
        public static void UsingModelSpace(Action<Transaction, BlockTableRecord> action, string commandName = "")
        {
            commandName = commandName == "" ? "Acad_Transaction" : commandName;
            try
            {
                using (DocumentLock doclock = Active.Document.LockDocument(DocumentLockMode.Write, commandName, commandName, true))
                {
                    using (AcTransaction tr = new AcTransaction())
                    {
                        Transaction t = tr.Transaction;     // Transaction
                        var modelspace = tr.ModelSpace;     // Modelspace

                        action(t, modelspace);
                    }
                }
            }
            catch (System.Exception ex)
            {
                throw new TransactionException(ex.Message, commandName, ex);
            }
        }
    }
}
