using Autodesk.AutoCAD.ApplicationServices;
using Autodesk.AutoCAD.DatabaseServices;

namespace AcHelper.Utilities
{
    public class XRecordHandler
    {
        #region Fields ...
        private Database _database = null;
        #endregion

        #region Ctor ...
        public XRecordHandler()
            : this(Active.Document)
        { }
        public XRecordHandler(Document doc)
        {
            if (doc != null && doc.IsActive)
            {
                _database = doc.Database;
            }
        }
        #endregion
    }
}
