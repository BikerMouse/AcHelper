using AcHelper.Enumerables;
using Autodesk.AutoCAD.ApplicationServices;
using Autodesk.AutoCAD.DatabaseServices;
using Autodesk.AutoCAD.Runtime;
using System;
using System.Collections.Generic;

namespace AcHelper
{
    public static class ExtensionMethods
    {
        public static void ForEach<T>(this Database database, Action<T> action) where T : Entity
        {
            try
            {
                Common.UsingModelSpace((tr, modelspace) =>
                {
                    RXClass ent_type = RXClass.GetClass(typeof(T));

                    // Loop through the entities in modelspace
                    foreach (ObjectId id in modelspace)
                    {
                        // Look for entities of th ecorrect type
                        if (id.ObjectClass.IsDerivedFrom(ent_type))
                        {
                            T entity = tr.GetObject(id, OpenMode.ForRead) as T;
                            action(entity);
                        }
                    }
                });
            }
            catch (System.Exception)
            {
                throw;
            }
            #region old (but worked) ...
            ////using (var tr = database.TransactionManager.StartTransaction())
            ////{
            ////    // Get the block table for the current database.
            ////    var blockTable =
            ////        (BlockTable)tr.GetObject(
            ////        database.BlockTableId, OpenMode.ForRead);

            ////    // Get the model space block table record.
            ////    var modelSpace =
            ////        (BlockTableRecord)tr.GetObject(
            ////        blockTable[BlockTableRecord.ModelSpace], OpenMode.ForRead);

            ////    RXClass theClass = RXClass.GetClass(typeof(T));

            ////    // Loop throug the entities in modelspace.
            ////    foreach (ObjectId objectId in modelSpace)
            ////    {
            ////        // Look for entities of the correct type
            ////        if (objectId.ObjectClass.IsDerivedFrom(theClass))
            ////        {
            ////            var entity =
            ////                (T)tr.GetObject(
            ////                objectId, OpenMode.ForRead);

            ////            action(entity);
            ////        }
            ////    }
            ////    tr.Commit();
            ////}
            #endregion
        }
        public static IEnumerable<T> OfType<T>(this IEnumerable<ObjectId> enumerable, Transaction tr, OpenMode openMode) where T : DBObject
        {
            return new DbObjectEnumerable<T>(enumerable, tr, openMode);
        }

        public static void UsingModelSpace(this Document document, Action<Transaction, BlockTableRecord> action, string CommandName = "")
        {
            Common.UsingModelSpace((t, ms) => action(t, ms), CommandName);
        }
        public static void UsingTransaction(this Document document, Action<Transaction> action, string commandName = "")
        {
            Common.UsingTransaction(t => action(t), commandName);
        }
    }
}
