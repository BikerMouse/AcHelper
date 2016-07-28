using System;
using System.Windows.Input;

namespace AcHelper.Command
{
    public class CommandHandlerBase
    {
        private Func<object, bool> _canExecute;

        #region Command Executer ...
        public static void ExecuteCommand<T>() where T : IAcadCommand
        {
            try
            {
                var cmd = Activator.CreateInstance<T>();
                cmd.Execute();
            }
            catch (System.Exception ex)
            {
                Active.WriteMessage(ex.Message);
            }
        }
        #endregion
    }
}
