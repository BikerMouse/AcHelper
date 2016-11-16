using AcHelper.Command;
using AcHelper.Demo.Commands;
using Autodesk.AutoCAD.ApplicationServices;
using Autodesk.AutoCAD.DatabaseServices;
using Autodesk.AutoCAD.EditorInput;
using Autodesk.AutoCAD.Geometry;
using Autodesk.AutoCAD.Runtime;
using System;

namespace AcHelper.Demo
{
    public class CommandHandler : CommandHandlerBase
    {
        public const string CMD_DRAWCIRCLE = "DEMO_DRAWCIRCLE";
        public const string CMD_THROWERROR = "DEMO_THROWERROR";
        public const string CMD_ADDXRECORDTOENTITY = "DEMO_ADDXRECORDTOENTITY";
        public const string CMD_ADDXRECORDTODOCUMENT = "DEMO_ADDXRECORDTODOCUMENT";
        public const string CMD_DISPLAYENTITYXRECORD = "DEMO_DISPLAYENTITYXRECORD";
        public const string CMD_HELP = "DEMO_HELP";
        public const string CMD_DISPLAYDOCUMENTXRECORD = "DEMO_DISPLAYDOCUMENTXRECORD";
        public const string CMD_OPENPALETTESET = "DEMO_OPENPALETTESET";
        public const string CMD_CREATEINNEREXCEPTION = "DEMO_CREATEINNEREXCEPTION";

        [CommandMethod(CMD_OPENPALETTESET)]
        public static void Demo_OpenPaletteSet()
        {
            ExecuteCommand<OpenPaletteSet>();
        }
        [CommandMethod(CMD_HELP)]
        public static void Demo_Help()
        {
            ExecuteCommand<HelpCommand>();
        }
        
        [CommandMethod(CMD_DRAWCIRCLE)]
        public static void Demo_DrawCircle()
        {
            ExecuteCommand<CreateCircle>();
        }
        [CommandMethod(CMD_THROWERROR)]
        public static void Demo_ThrowError()
        {
            ExecuteCommand<ThrowErrorCommand>();
        }
        [CommandMethod(CMD_CREATEINNEREXCEPTION)]
        public static void Demo_CreateInnerException()
        {
            ExecuteCommand<CreateInnerExceptionsCommand>();
        }
        [CommandMethod(CMD_ADDXRECORDTOENTITY)]
        public static void Demo_AddXrecordToEntity()
        {
            ExecuteCommand<AddXrecordToEntity>();
        }
        [CommandMethod(CMD_DISPLAYENTITYXRECORD)]
        public static void Demo_DisplayEntityXrecord()
        {
            ExecuteCommand<DisplayEntityXrecord>();
        }
        [CommandMethod(CMD_DISPLAYDOCUMENTXRECORD)]
        public static void Demo_DisplayDocumentXrecord()
        {
            try
            {
                Utilities.DisplayDocumentXrecord("DEMO_NOD","DEMO_KEY");
            }
            catch (System.Exception ex)
            {
                Logger.WriteToLog(ex);
                ExceptionHandler.WriteToCommandLine(ex);
            }
        }
        [CommandMethod(CMD_ADDXRECORDTODOCUMENT)]
        public static void Demo_AddXrecordToDocument()
        {
            ExecuteCommand<AddXrecordToDocument>();
        }



        [CommandMethod("DEMO_OPENCLOSETRANSACTION")]
        public static void Demo_OpenCloseTransaction()
        {
            try
            {
                Circle c = null;
                using (Transaction t = Active.Database.TransactionManager.StartOpenCloseTransaction())
                {
                    c = new Circle();
                    c.ColorIndex = 3;
                    c.Radius = 200;
                    c.Center = new Point3d(0, 0, 0);



                    BlockTable block_table = t.GetObject(HostApplicationServices.WorkingDatabase.BlockTableId, OpenMode.ForRead) as BlockTable;

                    if (!block_table.Has(BlockTableRecord.ModelSpace))
                        return;

                    BlockTableRecord block_table_record = t.GetObject(block_table[BlockTableRecord.ModelSpace], OpenMode.ForRead) as BlockTableRecord;
                    block_table_record.UpgradeOpen();

                    if (!block_table_record.IsWriteEnabled)
                    {
                        Active.WriteMessage("Can't open for write");
                        return;
                    }
                    block_table_record.AppendEntity(c);




                    t.AddNewlyCreatedDBObject(c, true);


                    c.Dispose();
                }

                Active.WriteMessage(c.Radius.ToString());

                c.UpgradeOpen();
                c.Radius = 300;
            }
            catch (System.Exception ex)
            {
                ExceptionHandler.WriteToCommandLine(ex);
            }
        }


        [CommandMethod("DEMO_STARTTRANSACTION")]
        public static void Demo_StartTransaction()
        {
            try
            {
                Circle c = null;
                using (Transaction t = Active.Database.TransactionManager.StartTransaction())
                {
                    c = new Circle();
                    c.ColorIndex = 3;
                    c.Radius = 200;
                    c.Center = new Point3d(0, 0, 0);


                    BlockTable block_table = t.GetObject(HostApplicationServices.WorkingDatabase.BlockTableId, OpenMode.ForRead) as BlockTable;

                    if (!block_table.Has(BlockTableRecord.ModelSpace))
                        return;

                    BlockTableRecord block_table_record = t.GetObject(block_table[BlockTableRecord.ModelSpace], OpenMode.ForRead) as BlockTableRecord;
                    block_table_record.UpgradeOpen();

                    if (!block_table_record.IsWriteEnabled)
                    {
                        Active.WriteMessage("Can't open for write");
                        return;
                    }
                    block_table_record.AppendEntity(c);



                    t.AddNewlyCreatedDBObject(c, true);
                }



                Active.WriteMessage(c.Radius.ToString());

                c.UpgradeOpen();
                c.Radius = 300;
            }
            catch (System.Exception ex)
            {
                ExceptionHandler.WriteToCommandLine(ex);
            }
        }

        [CommandMethod("TestVariousTypeCheck")]
        public static void TestVariousTypeCheck_Method()
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
        }

        public static ObjectIdCollection TypeCheckApproach1()
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
        public static ObjectIdCollection TypeCheckApproach2()
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
        public static ObjectIdCollection TypeCheckApproach3()
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
        public static ObjectIdCollection TypeCheckApproach4()
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
        public static ObjectIdCollection TypeCheckApproach5()
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
        [CommandMethod("Example_1")]
        public static void Example_1()
        {
            Document active_document = Application.DocumentManager.MdiActiveDocument;
            Autodesk.AutoCAD.DatabaseServices.TransactionManager transaction_manager = HostApplicationServices.WorkingDatabase.TransactionManager;

            ObjectId object_id = Autodesk.AutoCAD.Internal.Utils.EntLast();

            using (active_document.LockDocument())
            {
                // This using construction garantees dispose of transaction.
                // If Commit() wouldn't be called then during disposing Abort() of transaction will be called,
                // what means a rollback of all changes in that block.
                // When making Commit(), Abort() or Dispose() of a transaction 
                // it would close all objects that are managed by it.
                // And the object that is got from transaction.GetObject() 
                // becomes automatically managed by this transaction. 
                // And the transaction is enclosed in using block.
                // So there is no risk that this object wouldn't be closed in some case.
                using (Transaction transaction = transaction_manager.StartTransaction())
                {
                    // Here we get an existing object from transaction.
                    // And this object becomes managed by the transaction.
                    DBObject db_object = transaction.GetObject(object_id, OpenMode.ForRead);

                    // Here we cast this object to Entity.
                    // And it would be succeeded db_object and entity will point to the same object.
                    // Since it is one object and db_object is got through transaction 
                    // then there is no need to close (dispose) it.
                    // But if to do it it wouldn't raise any error.
                    // And for us it's much better not to write such extra code.
                    Entity entity = db_object as Entity;

                    // If db_object cannot be treated as Entity this function's work will be finished.
                    // Commit() wouldn't be reached and the transaction during dispose will close all opened objects.
                    if (entity == null)
                        return;

                    active_document.Editor.WriteMessage("Layer name: " + entity.Layer);

                    // After Commit() all objects that where managed by the transaction would be disposed.
                    transaction.Commit();
                }
            }
        }
        [CommandMethod("Example_2")]
        public static void Example_2()
        {
            Document active_document = Application.DocumentManager.MdiActiveDocument;
            Autodesk.AutoCAD.DatabaseServices.TransactionManager transaction_manager = HostApplicationServices.WorkingDatabase.TransactionManager;

            using (active_document.LockDocument())
            {
                using (Transaction transaction = transaction_manager.StartTransaction())
                {
                    // Enclosing creation of DBText object in using construction for guaranteed calling.
                    // We will add this object through transaction via AddNewlyCreatedDBObject().
                    // And in this case there would be no need in direct disposing of this object.
                    // But if we wouldn't come to adding object line (for example, if an exception will be thrown)
                    // via transaction then the object won't become managed by transaction 
                    // and Dispose() will be needed (and that will be provided indirectly 
                    // by putting the objects creation in using statement).
                    using (DBText text = new DBText())
                    {
                        BlockTable block_table = transaction.GetObject(HostApplicationServices.WorkingDatabase.BlockTableId, OpenMode.ForRead) as BlockTable;

                        // Here the text object is not yet managed by transaction.
                        // And if we return from method here without putting the object in using 
                        // the wouldn't be disposed. That's why we need to put the object in using statement.
                        if (!block_table.Has(BlockTableRecord.ModelSpace))
                            return;

                        BlockTableRecord block_table_record = transaction.GetObject(block_table[BlockTableRecord.ModelSpace], OpenMode.ForRead) as BlockTableRecord;
                        block_table_record.UpgradeOpen();

                        if (!block_table_record.IsWriteEnabled)
                        {
                            active_document.Editor.WriteMessage("Can't open for write");
                            return;
                        }

                        text.TextString = "Hello World";
                        text.Position = new Point3d(10, 10, 0);

                        block_table_record.AppendEntity(text);
                        // Here the object becomes managed by transaction.
                        transaction.AddNewlyCreatedDBObject(text, true);

                        transaction.Commit();
                    }
                }
            }
        }
    }
}
