using BuerTech.Utilities.Logger;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AcHelper.Demo
{
    internal class Logging : IDisposable
    {
        #region Singleton ...
        static Logging()
        {
            LogSetup setup = new LogSetup() 
            {
                ApplicationName = "AcHelper Demo",
                MaxAge = 0,
                MaxQueueSize = 1,
                SaveLocation = @"C:\Temp\LogFiles"
            };

            _instance = new LogWriter(setup);
        }

        private static LogWriter _instance;

        public static LogWriter Instance
        {
            get { return _instance; }
        }
        #endregion



        #region IDisposable Members

        // Flag: Has Dispose already been called?
        bool disposed = false;

        // Public implementation of Dispose pattern callable by consumers.
        public void Dispose()
        {
            Dispose(true);
            GC.SuppressFinalize(this);
        }

        protected virtual void Dispose(bool disposing)
        {
            if (disposed)
                return;

            if (disposing)
            {
                if (true)
                {
                    _instance.Dispose();
                    _instance = null;
                }
            }
            // Free any unmanaged objects here.

            disposed = true;
        }

        #endregion
    }
}
