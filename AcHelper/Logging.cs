using BuerTech.Utilities.Logger;
using System;

namespace AcHelper
{
    [Obsolete("This Class will be replaced by Logger")]
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

    /// <summary>
    /// The logger provides an easy access to the <see cref="BuerTech.Utilities.Logger.LogWriter"/>LogWriter.
    /// </summary>
    public class Logger
    {
        private readonly LogWriter _logwriter;
        private static Logger _instance;

        private Logger(LogSetup setup)
        {
            // Create the LogWriter
            _logwriter = new LogWriter(setup);
        }
        /// <summary>
        /// Initializes the Logger before it's available for use.
        /// </summary>
        /// <param name="setup"></param>
        public static void Initialize(LogSetup setup)
        {
            if (_instance == null)
            {
                _instance = new Logger(setup);
            }
        }
        /// <summary>
        /// Logs a message to the logfile.
        /// </summary>
        /// <param name="message"></param>
        /// <param name="priority"></param>
        public static void WriteToLog(string message, LogPrior priority = LogPrior.Info)
        {
            if (_instance != null)
            {
                _instance._logwriter.WriteToLog(message, priority);
            }
            else
            {
                ExceptionHandler.WriteToCommandLine(new LoggerNotInitializedException());
            }
        }
        /// <summary>
        /// Logs an <see cref="Exception"/>Exception to the logfile
        /// </summary>
        /// <param name="exception"></param>
        /// <param name="priority"></param>
        public static void WriteToLog(Exception exception, LogPrior priority = LogPrior.Error)
        {
            if (_instance != null)
            {
                _instance._logwriter.WriteToLog(exception, priority);
            }
            else
            {
                ExceptionHandler.WriteToCommandLine(new LoggerNotInitializedException());
            }
        }
        /// <summary>
        /// Writes a seperate log to de logfile with debug information.
        /// </summary>
        /// <param name="message"></param>
        public static void Debug(string message)
        {
            if (_instance != null)
            {
                _instance._logwriter.WriteToLog(message, LogPrior.Debug);
            }
        }

        /// <summary>
        /// Disposes the LogWriter.
        /// </summary>
        public static void Dispose()
        {
            if (_instance != null)
            {
                _instance._logwriter.Dispose();
                _instance = null;
            }
        }
    }
}
