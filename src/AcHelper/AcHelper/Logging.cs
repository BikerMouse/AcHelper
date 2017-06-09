using BuerTech.Utilities.Logger;
using System;

namespace AcHelper
{
    /// <summary>
    /// The logger provides an easy access to the <see cref="BuerTech.Utilities.Logger.LogWriter"/>.
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
        /// Initializes the Logger before it's available for use.
        /// </summary>
        /// <param name="application">Name of the appication using the LogWriter.</param>
        /// <param name="saveLocation">Path to the directory where to save the logfile.</param>
        /// <param name="maxAge">Max age of cached logs in seconds.</param>
        /// <param name="maxQueueSize">Max amount of logs in cache.</param>
        public static void Initialize(string application, string saveLocation, int maxAge, int maxQueueSize)
        {
            if (_instance == null)
            {
                LogSetup setup = new LogSetup();
                setup.ApplicationName = application;
                setup.MaxAge = maxAge;
                setup.MaxQueueSize = maxQueueSize;
                setup.SaveLocation = saveLocation;

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
        /// Logs an <see cref="Exception"/> to the logfile
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
