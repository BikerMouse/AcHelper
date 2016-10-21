using BuerTech.Utilities.Logger;

namespace AcHelper
{
    public class Logging
    {
        private static bool _initialized = false;
        public static bool IsInitialized
        {
            get { return _initialized; }
        }

        private static LogWriter _logger = null;
        /// <summary>
        /// Instance of the LogWriter.
        /// Lo
        /// </summary>
        public static LogWriter Logger
        {
            get 
            {
                if (!_initialized)
                {
                    throw new LoggerNotInitializedException();
                }
                return _logger; 
            }
        }

        public static void InitializeLogger(LogSetup setup)
        {
            if (!_initialized)
            {
                _logger = new LogWriter(setup);
                _initialized = true;
            }
        }

        #region Dispose ...

        public static void Dispose()
        {
            if (_initialized)
            {
                if (_logger != null)
                {
                    _logger.Dispose();
                    _logger = null;
                    _initialized = false;
                }
            }
        }

        #endregion

    }
}
