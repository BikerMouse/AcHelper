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
                Common.UsingTransaction((tr) =>
                {
                    Transaction t = tr.Transaction;
                    var modelspace = tr.ModelSpace;
                    RXClass ent_type = RXClass.GetClass(typeof(T));

                    // Loop through the entities in modelspace
                    foreach (ObjectId id in modelspace)
                    {
                        // Look for entities of th ecorrect type
                        if (id.ObjectClass.IsDerivedFrom(ent_type))
                        {
                            T entity = t.GetObject(id, OpenMode.ForRead) as T;
                            action(entity);
                        }
                    }
                });
            }
            catch (Autodesk.AutoCAD.Runtime.Exception ex) 
            {
                Active.WriteMessage(ex.Message);
            }
            catch (System.Exception)
            {
                throw;
            }
        }
        public static void ForEach<T>(this Document doc, Action<T> action) where T : Entity
        {
            Database database = doc.Database;
            try
            {
                RXClass ent_type = RXClass.GetClass(typeof(T));
                Common.UsingTransaction(doc, (tr) =>
                {
                    Transaction t = tr.Transaction;
                    var modelspace = tr.ModelSpace;

                    // Loop through the entities in modelspace
                    foreach (ObjectId id in modelspace)
                    {
                        // Look for entities of th ecorrect type
                        if (id.ObjectClass.IsDerivedFrom(ent_type))
                        {
                            T entity = t.GetObject<T>(id, OpenMode.ForRead);
                            action(entity);
                        }
                    }
                });
            }
            catch (Autodesk.AutoCAD.Runtime.Exception ex)
            {
                Active.WriteMessage(ex.Message);
            }
            catch (System.Exception)
            {
                throw;
            }
        }
        public static IEnumerable<T> OfType<T>(this IEnumerable<ObjectId> enumerable, Transaction tr, OpenMode openMode) where T : DBObject
        {
            return new DbObjectEnumerable<T>(enumerable, tr, openMode);
        }

        public static void UsingModelSpace(this Document document, Action<Utilities.AcTransaction, BlockTableRecord> action, string CommandName = "")
        {
            Common.UsingModelSpace(document, CommandName, action);
        }
        public static void UsingTransaction(this Document document, Action<Utilities.AcTransaction> action, string commandName = "")
        {
            Common.UsingTransaction(document, commandName, action);
        }

        public static T GetObject<T>(this Transaction tr, ObjectId objectId, OpenMode openMode, bool openErased = false) where T : DBObject
        {
            T dbObject = tr.GetObject(objectId, openMode, openErased) as T;
            return dbObject;
        }
    }
}
