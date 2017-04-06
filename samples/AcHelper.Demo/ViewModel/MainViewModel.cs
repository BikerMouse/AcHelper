using Autodesk.AutoCAD.Geometry;
using GalaSoft.MvvmLight;
using GalaSoft.MvvmLight.Command;

namespace AcHelper.Demo.ViewModel
{
    /// <summary>
    /// This class contains properties that a View can data bind to.
    /// <para>
    /// See http://www.galasoft.ch/mvvm
    /// </para>
    /// </summary>
    public class MainViewModel : ViewModelBase
    {
        /// <summary>
        /// Initializes a new instance of the MainViewModel class.
        /// </summary>
        public MainViewModel()
        {
        }

        private RelayCommand _addCircleCommand;

        /// <summary>
        /// Gets the AddCircleCommand.
        /// </summary>
        public RelayCommand AddCircleCommand
        {
            get
            {
                return _addCircleCommand
                    ?? (_addCircleCommand = new RelayCommand(ExecuteAddCircleCommand));
            }
        }

        private void ExecuteAddCircleCommand()
        {
            string location = Point2d.Origin.ToString();
            CommandHandler.ExecuteFromCommandLine(CommandHandler.CMD_DRAWCIRCLE, 10, "10,10");
        }
    }
}