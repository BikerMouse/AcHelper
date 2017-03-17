using AcHelper.Utilities;
using Autodesk.AutoCAD.ApplicationServices;
using Autodesk.AutoCAD.DatabaseServices;
using Autodesk.AutoCAD.EditorInput;
using Autodesk.AutoCAD.Geometry;

namespace AcHelper.Demo
{
    internal class Utilities
    {
        internal static void DrawCircle(Document document, double radius)
        {
            Editor ed = document.Editor;
            PromptPointOptions opt = new PromptPointOptions("\nSelect a location to draw the circle");

            document.UsingModelSpace((tr,ms) =>
            {
                Transaction t = tr.Transaction;
                PromptPointResult res = ed.GetPoint(opt);
                if (res.Status == PromptStatus.OK)
                {
                    Circle c = new Circle(res.Value, Vector3d.ZAxis, radius);
                    c.ColorIndex = 1;
                    ms.AppendEntity(c);
                    t.AddNewlyCreatedDBObject(c, true);
                }
            });
        }

        internal static void AddTextDataToEntity(string xKey, string text)
        {
            Document doc = Active.Document;
            Editor ed = Active.Editor;
            PromptEntityOptions opt = new PromptEntityOptions("\nSelect an entity to add xRecord");

            doc.UsingModelSpace((tr, ms) =>
            {
                Transaction t = tr.Transaction;
                ResultBuffer resbuf = new ResultBuffer();
                resbuf.Add(new TypedValue((int)DxfCode.Text, text));

                PromptEntityResult res = ed.GetEntity(opt);
                if (res.Status == PromptStatus.OK)
                {
                    ObjectId oid = res.ObjectId;
                    Entity ent = t.GetObject<Entity>(oid, OpenMode.ForRead);
                    if (ent != null)
                    {
                        using (WriteEnabler we = new WriteEnabler(ent))
                        {
                            if (ent.IsWriteEnabled)
                            {
                                if (ent.ExtensionDictionary == null)
                                {
                                    ent.CreateExtensionDictionary();
                                }
                                using (DBDictionary nod = t.GetObject<DBDictionary>(ent.ExtensionDictionary, OpenMode.ForRead))
                                {
                                    using (WriteEnabler nodWe = new WriteEnabler(nod))
                                    {
                                        if (nod.IsWriteEnabled)
                                        {
                                            using (Xrecord xRec = new Xrecord())
                                            {
                                                xRec.Data = resbuf;
                                                nod.SetAt(xKey, xRec);
                                                t.AddNewlyCreatedDBObject(xRec, true);
                                            }
                                        }
                                    }
                                } 
                            }
                        }
                    }
                }
            });
        }

        internal static void SetEntityXrecord(ObjectId id, string key, ResultBuffer resbuf)
        {
            Document doc = Active.Document;
            Database db = doc.Database;
            using (Transaction tr = db.TransactionManager.StartTransaction())
            {
                Entity ent = tr.GetObject(id, OpenMode.ForRead) as Entity;
                if (ent != null)
                {
                    ent.UpgradeOpen();
                    ent.CreateExtensionDictionary();
                    DBDictionary xDict = (DBDictionary)tr.GetObject(ent.ExtensionDictionary, OpenMode.ForWrite);
                    Xrecord xRec = new Xrecord();
                    xRec.Data = resbuf;
                    xDict.SetAt(key, xRec);
                    tr.AddNewlyCreatedDBObject(xRec, true);
                }
                tr.Commit();
            }
        }
        internal static void DisplayEntityXrecord(ObjectId id, string key)
        {
            string[] xValue;
            ResultBuffer resbuf;

            using (var xh = new XRecordHandler())
            {
                resbuf = xh.GetEntityXrecord(id, key);
            }

            TypedValue[] typedValues = resbuf.AsArray();
            xValue = new string[typedValues.Length];

            for (int i = 0; i < typedValues.Length; i++)
            {
                Active.WriteMessage(typedValues[i].Value.ToString());
            }
        }

        internal static void AddXrecordToDocument(string dictionaryName, string xKey, string text)
        {
            ResultBuffer resbuf = new ResultBuffer(new TypedValue((int)DxfCode.Text, text));
            using (XRecordHandler xh = new XRecordHandler(Active.Document))
            {
                xh.UpdateDocumentXrecord(dictionaryName, xKey, resbuf);
            }
        }
        internal static void DisplayDocumentXrecord(string dictionaryName, string xKey)
        {
            string[] xValue;
            ResultBuffer resbuf;
            
            using (XRecordHandler xh = new XRecordHandler(Active.Document))
            {
                resbuf = xh.GetXrecord(dictionaryName, xKey);
            }

            if (resbuf == null)
            {
                Active.WriteMessage("No Xrecord '{0}' found in dictionary: {1}", xKey, dictionaryName);
                return;
            }

            TypedValue[] typed_values = resbuf.AsArray();
            xValue = new string[typed_values.Length];

            for (int i = 0; i < typed_values.Length; i++)
            {
                Active.WriteMessage(typed_values[i].Value.ToString());
            }
        }
    }
}
