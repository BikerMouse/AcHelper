using System;
using System.Collections.Generic;
using System.Text;
using Autodesk.AutoCAD.ApplicationServices;
using Autodesk.AutoCAD.DatabaseServices;
using AcHelper.Wrappers;

namespace AcHelper
{
    public static class AcDictionaries
    {
        /// <summary>
        /// Retreive Dictionary from database with specific type and name
        /// </summary>
        /// <param name="dictionaryType"></param>
        /// <param name="dictionaryName"></param>
        /// <returns></returns>
        public static DBDictionary GetDictionary(string dictionaryType, string dictionaryName)
        {
            Document document = Active.Document;
            Database database = document.Database;
            DBDictionary nod = GetNodeDictionary(dictionaryType, OpenMode.ForRead);

            using (Transaction transaction = database.TransactionManager.StartTransaction())
            {
                try
                {
                    DBDictionary dict = transaction.GetObject<DBDictionary>(nod.GetAt(dictionaryType), OpenMode.ForRead);
                    foreach (DBDictionaryEntry entry in dict)
                    {
                        if (entry.Key == dictionaryName)
                        {
                            return dict;
                        }
                    }
                }
                catch (Exception ex)
                {
                    Active.WriteMessage(ex.Message);
                    throw;
                }
            }
            return null;
        }

        /// <summary>
        /// Adding specific dictionary with type and name
        /// </summary>
        /// <param name="dictionaryType"></param>
        /// <param name="dictionaryName"></param>
        /// <param name="repair"></param>
        public static void AddDictionary(string dictionaryType, string dictionaryName)
        {
            Document document = Active.Document;
            Database database = document.Database;
            DBDictionary nod = GetNodeDictionary(dictionaryType, OpenMode.ForWrite);

            using (Transaction transaction = database.TransactionManager.StartTransaction())
            {
                DBDictionary newDict = new DBDictionary();
                if (!nod.Contains(dictionaryName))
                {
                    nod.SetAt(dictionaryName, newDict);
                    transaction.AddNewlyCreatedDBObject(newDict, true);
                }
                transaction.Commit();
            }
        }

        public static void RemoveDictionary(string dictionaryType, string dictionaryName)
        {
            Document document = Active.Document;
            Database database = document.Database;
            DBDictionary nod = GetNodeDictionary(dictionaryType, OpenMode.ForWrite);

            using (Transaction transaction = database.TransactionManager.StartTransaction())
            {
                if (!nod.Contains(dictionaryName))
                {
                    nod.Remove(dictionaryName);
                }
                transaction.Commit();
            }
        }

        /// <summary>
        /// Adding MultilineStyle with name and file
        /// </summary>
        /// <param name="dictionaryType"></param>
        /// <param name="dictionaryName"></param>
        /// <param name="repair"></param>
        public static void AddMultilineStyle(string dictionaryName, string dictionaryFile)
        {
            Document document = Active.Document;
            Database database = document.Database;

            //Load multilinestyle
            database.LoadMlineStyleFile(dictionaryName, dictionaryFile);
        }

        public static void RemoveMultilineStyle(string dictionaryType, string dictionaryName)
        {
            Document document = Active.Document;
            Database database = document.Database;
            DBDictionary nod = GetNodeDictionary(dictionaryType, OpenMode.ForWrite);
            using (Transaction transaction = database.TransactionManager.StartTransaction())
            {
                try
                {
                    DBDictionary dict = transaction.GetObject<DBDictionary>(nod.GetAt(dictionaryType), OpenMode.ForWrite);
                    foreach (DBDictionaryEntry entry in dict)
                    {
                        if (entry.Key == dictionaryName)
                        {
                            dict.Remove(entry.Key);
                        }
                    }
                }
                catch (Exception ex)
                {
                    Active.WriteMessage(ex.Message);
                    throw;
                }
                transaction.Commit();
            }
        }

        public static DBDictionary GetNodeDictionary(string dictionaryType, OpenMode mode)
        {
            Document document = Active.Document;
            Database database = document.Database;
            using (Transaction transaction = database.TransactionManager.StartTransaction())
            {
                DBDictionary nod = transaction.GetObject<DBDictionary>(database.NamedObjectsDictionaryId, mode);
                return nod;
            }
        }

        public static TextStyleTableRecord GetTextStyle(string styleName)
        {
            Document document = Active.Document;
            Database database = document.Database;
            TextStyleTable tsTable = GetTextStyleTable(OpenMode.ForRead);

            using (Transaction transaction = database.TransactionManager.StartTransaction())
            {
                try
                {
                    foreach (ObjectId tsRecId in tsTable)
                    {
                        TextStyleTableRecord tsRec = transaction.GetObject<TextStyleTableRecord>(tsRecId, OpenMode.ForRead);
                        if (tsRec.Name == styleName)
                        {
                            return tsRec;
                        }
                    }
                }
                catch (Exception ex)
                {
                    Active.WriteMessage(ex.Message);
                    throw;
                }
            }
            return null;
        }

        public static void AddTextStyle(string styleName)
        {
            Document document = Active.Document;
            Database database = document.Database;
            TextStyleTable tsTable = GetTextStyleTable(OpenMode.ForWrite);
        }

        public static void RemoveTextStyle(string styleName)
        {
            Document document = Active.Document;
            Database database = document.Database;
            TextStyleTable tsTable = GetTextStyleTable(OpenMode.ForWrite);
        }

        public static TextStyleTable GetTextStyleTable(OpenMode mode)
        {
            Document document = Active.Document;
            Database database = document.Database;

            using (Transaction transaction = database.TransactionManager.StartTransaction())
            {
                TextStyleTable tsTable = transaction.GetObject<TextStyleTable>(database.TextStyleTableId, mode);
                return tsTable;
            }
        }

        public static DimStyleTableRecord GetDimStyle (string styleName)
        {
            Document document = Active.Document;
            Database database = document.Database;
            DimStyleTable dsTable = GetDimStyleTable(OpenMode.ForRead);

            using (Transaction transaction = database.TransactionManager.StartTransaction())
            {
                try
                {
                    foreach (ObjectId dsRecId in dsTable)
                    {
                        DimStyleTableRecord dsRec = transaction.GetObject<DimStyleTableRecord>(dsRecId, OpenMode.ForRead); 
                        if (dsRec.Name == styleName)
                        {
                            return dsRec;
                        }
                    }
                }
                catch (Exception ex)
                {
                    Active.WriteMessage(ex.Message);
                    throw;
                }
            }
            return null;
        }

        public static void AddDimStyle(string styleName)
        {
            Document document = Active.Document;
            Database database = document.Database;
            DimStyleTable dsTable = GetDimStyleTable(OpenMode.ForWrite);
        }

        public static void RemoveDimStyle(string styleName)
        {
            Document document = Active.Document;
            Database database = document.Database;
            DimStyleTable dsTable = GetDimStyleTable(OpenMode.ForWrite);
        }

        public static DimStyleTable GetDimStyleTable(OpenMode mode)
        {
            Document document = Active.Document;
            Database database = document.Database;

            using (Transaction transaction = database.TransactionManager.StartTransaction())
            {
               DimStyleTable dsTable = transaction.GetObject<DimStyleTable>(database.DimStyleTableId, mode);
               return dsTable;
            }
        }

        public static LayerTableRecord GetLayer (string layerName)
        {
            Document document = Active.Document;
            Database database = document.Database;
            LayerTable laTable = GetLayerTable(OpenMode.ForRead);

            using (Transaction transaction = database.TransactionManager.StartTransaction())
            {
                try
                {
                    foreach (ObjectId laRecId in laTable)
                    {
                        LayerTableRecord laRec = transaction.GetObject<LayerTableRecord>(laRecId, OpenMode.ForRead); 
                        if (laRec.Name == layerName)
                        {
                            return laRec;
                        }
                    }
                }
                catch (Exception ex)
                {
                    Active.WriteMessage(ex.Message);
                    throw;
                }
            }
            return null;
        }

        public static void AddLayer(string layerName)
        {
            Document document = Active.Document;
            Database database = document.Database;
            LayerTable laTable = GetLayerTable(OpenMode.ForWrite);
        }

        public static void RemoveLayer(string layerName)
        {
            Document document = Active.Document;
            Database database = document.Database;
            LayerTable laTable = GetLayerTable(OpenMode.ForWrite);
        }

        public static LayerTable GetLayerTable(OpenMode mode)
        {
            Document document = Active.Document;
            Database database = document.Database;

            using (Transaction transaction = database.TransactionManager.StartTransaction())
            {
                LayerTable laTable = transaction.GetObject<LayerTable>(database.LayerTableId, mode);
                return laTable;
            }
        }

        public static BlockTableRecord GetBlock(string blockName)
        {
            Document document = Active.Document;
            Database database = document.Database;
            BlockTable blTable = GetBlockTable(OpenMode.ForRead);

            using (Transaction transaction = database.TransactionManager.StartTransaction())
            {
                try
                {
                    foreach (ObjectId blRecId in blTable)
                    {
                        BlockTableRecord blRec = transaction.GetObject<BlockTableRecord>(blRecId, OpenMode.ForRead); 
                        if (blRec.Name == blockName)
                        {
                            return blRec;
                        }
                    }
                }
                catch (Exception ex)
                {
                    Active.WriteMessage(ex.Message);
                    throw;
                }
            }
            return null;
        }

        public static void AddBlock(string blockName)
        {
            Document document = Active.Document;
            Database database = document.Database;
            BlockTable blTable = GetBlockTable(OpenMode.ForWrite);
        }

        public static void RemoveBlock(string blockName)
        {
            Document document = Active.Document;
            Database database = document.Database;
            BlockTable blTable = GetBlockTable(OpenMode.ForWrite);
        }

        public static BlockTable GetBlockTable(OpenMode mode)
        {
            Document document = Active.Document;
            Database database = document.Database;

            using (Transaction transaction = database.TransactionManager.StartTransaction())
            {
                BlockTable blTable = transaction.GetObject<BlockTable>(database.BlockTableId, mode);
                return blTable;
            }
        }

    }
}
