namespace AcHelper.Command
{
    using System;

    public interface IAcadCommand
    {
        /// <summary>
        /// Executes the Command code.
        /// </summary>
        void Execute();
    }
}
