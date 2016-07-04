using System;

namespace AcHelper.Command
{
    public class CommandHandlerBase
    {
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
