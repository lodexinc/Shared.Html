using System;
using log4net;

namespace Shared.Html.Logging
{

    public static class Log
    {
        private const string CONFIG_FILE_DOES_NOT_EXIST_ERROR = "The specified Log4Net congiuration file does not exist.";
        private const string LOG_INITIALISATION_ERROR = "An error occurred initialising the Log4Net log.";

        private static ILog _logger = null;
        /// <summary>
        /// Returns an ILog object which is used to perform logging.
        /// </summary>
        private static ILog logger
        {
            get
            {
                if (_logger == null)
                {
                    try
                    {
                        log4net.Config.XmlConfigurator.Configure();
                        _logger = LogManager.GetLogger(System.Reflection.MethodBase.GetCurrentMethod().DeclaringType);
                    }
                    catch (Exception inner)
                    {
                        throw new Exception(LOG_INITIALISATION_ERROR, inner);
                    }
                }
                return _logger;
            }
        }

        public static void Event(eventType type, string message)
        {
            switch (type)
            {
                case eventType.Information:
                    logger.Info(message);
                    break;
                case eventType.Warning:
                    logger.Warn(message);
                    break;
                case eventType.Debug:
                    logger.Debug(message);
                    break;
                case eventType.Fatal:
                    logger.Fatal(message);
                    break;
                default:
                    logger.Error(message);
                    break;
            }
        }

        public static void Event(Exception ex)
        {
            logger.Error(ex.Message, ex);
        }

        public static void Event(string currentUser, string requestedURL, Exception ex)
        {
            logger.Error("Unhandled exception at " + requestedURL + " with user " + currentUser, ex);
        }

        public static void Startup()
        {
            logger.Info("============  " + System.Reflection.Assembly.GetExecutingAssembly().GetName().Name + " Application Startup  ============");
        }

        public enum eventType
        {
            Information,
            Warning,
            Error,
            Debug,
            Fatal
        };
    }
}