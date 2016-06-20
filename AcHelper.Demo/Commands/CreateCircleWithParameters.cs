using AcHelper.Command;
using Autodesk.AutoCAD.ApplicationServices;
using Autodesk.AutoCAD.DatabaseServices;
using Autodesk.AutoCAD.Geometry;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AcHelper.Demo.Commands
{
    public class CreateCircleWithParameters : IAcadCommand
    {
        private Point3d _location;
        private double _radius;

        public CreateCircleWithParameters(Point3d location, double radius)
        {
            if (location == null)
            {
                throw new ArgumentNullException("location");
            }
            if (radius == null || radius < 10)
            {
                throw new ArgumentOutOfRangeException("radius", "Radius must be 10 or higher.");
            }

            _location = location;
            _radius = radius;
        }


        #region IAcadCommand Members
        public void Execute()
        {
            Document document = Active.Document;

            document.UsingModelSpace((tr, ms) =>
            {
                Transaction t = tr.Transaction;
                Circle c = new Circle(_location, Vector3d.ZAxis, _radius);
                c.ColorIndex = 3;
                ms.AppendEntity(c);
                t.AddNewlyCreatedDBObject(c, true);
            });
        }
        public bool CanExecute()
        {
            throw new NotImplementedException();
        }
        #endregion
    }
}
