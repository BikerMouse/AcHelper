
namespace AcHelper.Command
{
    public interface IAcadCommand
    {
        void Execute();
        bool CanExecute();
    }
}
