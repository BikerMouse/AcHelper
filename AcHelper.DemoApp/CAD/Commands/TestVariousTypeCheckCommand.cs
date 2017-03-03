using Autodesk.AutoCAD.DatabaseServices;
using Autodesk.AutoCAD.EditorInput;
using Autodesk.AutoCAD.Runtime;
using System;

namespace AcHelper.DemoApp.CAD.Commands
{
    using Command;
    public class TestVariousTypeCheckCommand : IAcadCommand
    {
        public void Execute()
        {
            Editor ed = Active.Editor;

            DateTime begin = DateTime.Now;
            int count = TypeCheckApproach1().Count;
            TimeSpan elapsed = DateTime.Now.Subtract(begin);

            ed.WriteMessage("Time elapsed in TypeCheckApproach1 for {0} circles: {1}\n", count, elapsed.TotalMilliseconds);

            begin = DateTime.Now;
            count = TypeCheckApproach2().Count;
            elapsed = DateTime.Now.Subtract(begin);

            ed.WriteMessage("Time elapsed in TypeCheckApproach2 for {0} circles: {1}\n", count, elapsed.TotalMilliseconds);

            begin = DateTime.Now;
            count = TypeCheckApproach3().Count;
            elapsed = DateTime.Now.Subtract(begin);

            ed.WriteMessage("Time elapsed in TypeCheckApproach3 for {0} circles: {1}\n", count, elapsed.TotalMilliseconds);

            begin = DateTime.Now;
            count = TypeCheckApproach4().Count;
            elapsed = DateTime.Now.Subtract(begin);

            ed.WriteMessage("Time elapsed in TypeCheckApproach4 for {0} circles: {1}\n", count, elapsed.TotalMilliseconds);

            begin = DateTime.Now;
            count = TypeCheckApproach5().Count;
            elapsed = DateTime.Now.Subtract(begin);

            ed.WriteMessage("Time elapsed in TypeCheckApproach5 for {0} circles: {1}\n", count, elapsed.TotalMilliseconds);

            begin = DateTime.Now;
            count = AcHelperTypeCheck().Count;
            elapsed = DateTime.Now.Subtract(begin);

            ed.WriteMessage("Time elapsed in AcHelperTypeCheck for {0} circles: {1}\n", count, elapsed.TotalMilliseconds);

            begin = DateTime.Now;
            count = AcHelperTypeCheck2().Count;
            elapsed = DateTime.Now.Subtract(begin);

            ed.WriteMessage("Time elapsed in AcHelperTypeCheck2 for {0} circles: {1}\n", count, elapsed.TotalMilliseconds);
        }

        private ObjectIdCollection AcHelperTypeCheck()
        {
            ObjectIdCollection ids = new ObjectIdCollection();
            Database db = HostApplicationServices.WorkingDatabase;
            Common.UsingModelSpace(Active.Document, "AcHelperTypeCheck", (t, ms) =>
            {
                Transaction tr = t.Transaction;
                foreach (ObjectId id in ms)
                {
                    if (id.ObjectClass == EntityTypes.Circle)
                    {
                        ids.Add(id);
                    }
                }
            });

            return ids;
        }

        private ObjectIdCollection AcHelperTypeCheck2()
        {
            ObjectIdCollection ids = new ObjectIdCollection();
            Database db = HostApplicationServices.WorkingDatabase;
            Common.UsingModelSpace(Active.Document, "AcHelperTypeCheck", (t, ms) =>
            {
                Transaction tr = t.Transaction;
                var cl = RXObject.GetClass(typeof(Circle));
                foreach (ObjectId id in ms)
                {
                    if (id.ObjectClass == cl)
                    {
                        ids.Add(id);
                    }
                }
            });

            return ids;
        }

        private ObjectIdCollection TypeCheckApproach1()
        {
            ObjectIdCollection ids = new ObjectIdCollection();
            Database db = HostApplicationServices.WorkingDatabase;
            using (Transaction tr = db.TransactionManager.StartTransaction())
            {
                BlockTable bt = tr.GetObject(db.BlockTableId, OpenMode.ForRead) as BlockTable;
                BlockTableRecord btr = tr.GetObject(bt[BlockTableRecord.ModelSpace], OpenMode.ForRead) as BlockTableRecord;
                foreach (ObjectId id in btr)
                {
                    DBObject obj = tr.GetObject(id, OpenMode.ForRead);
                    if (obj is Circle)
                    {
                        ids.Add(obj.Id);
                    }
                }
                tr.Commit();
            }

            return ids;
        }
        private ObjectIdCollection TypeCheckApproach2()
        {
            ObjectIdCollection ids = new ObjectIdCollection();
            Database db = HostApplicationServices.WorkingDatabase;
            using (Transaction tr = db.TransactionManager.StartTransaction())
            {
                BlockTable bt = tr.GetObject(db.BlockTableId, OpenMode.ForRead) as BlockTable;
                BlockTableRecord btr = tr.GetObject(bt[BlockTableRecord.ModelSpace], OpenMode.ForRead) as BlockTableRecord;
                foreach (ObjectId id in btr)
                {
                    DBObject obj = tr.GetObject(id, OpenMode.ForRead);
                    if (obj.GetType() == typeof(Circle))
                    {
                        ids.Add(obj.Id);
                    }
                }
                tr.Commit();
            }

            return ids;
        }
        private ObjectIdCollection TypeCheckApproach3()
        {
            ObjectIdCollection ids = new ObjectIdCollection();
            Database db = HostApplicationServices.WorkingDatabase;
            using (Transaction tr = db.TransactionManager.StartTransaction())
            {
                BlockTable bt = tr.GetObject(db.BlockTableId, OpenMode.ForRead) as BlockTable;
                BlockTableRecord btr = tr.GetObject(bt[BlockTableRecord.ModelSpace], OpenMode.ForRead) as BlockTableRecord;
                foreach (ObjectId id in btr)
                {
                    if (id.ObjectClass == RXObject.GetClass(typeof(Circle)))
                    {
                        ids.Add(id);
                    }
                }
                tr.Commit();
            }

            return ids;
        }
        private ObjectIdCollection TypeCheckApproach4()
        {
            ObjectIdCollection ids = new ObjectIdCollection();
            Database db = HostApplicationServices.WorkingDatabase;
            using (Transaction tr = db.TransactionManager.StartTransaction())
            {
                BlockTable bt = tr.GetObject(db.BlockTableId, OpenMode.ForRead) as BlockTable;
                BlockTableRecord btr = tr.GetObject(bt[BlockTableRecord.ModelSpace], OpenMode.ForRead) as BlockTableRecord;
                foreach (ObjectId id in btr)
                {
                    if (id.ObjectClass.DxfName == "CIRCLE")
                    {
                        ids.Add(id);
                    }
                }
                tr.Commit();
            }

            return ids;
        }
        private ObjectIdCollection TypeCheckApproach5()
        {
            ObjectIdCollection ids = new ObjectIdCollection();
            Database db = HostApplicationServices.WorkingDatabase;
            using (Transaction tr = db.TransactionManager.StartTransaction())
            {
                BlockTable bt = tr.GetObject(db.BlockTableId, OpenMode.ForRead) as BlockTable;
                BlockTableRecord btr = tr.GetObject(bt[BlockTableRecord.ModelSpace], OpenMode.ForRead) as BlockTableRecord;
                var cl = RXObject.GetClass(typeof(Circle));
                foreach (ObjectId id in btr)
                {
                    if (id.ObjectClass == cl)
                    {
                        ids.Add(id);
                    }
                }
                tr.Commit();
            }

            return ids;
        }
    }
}
