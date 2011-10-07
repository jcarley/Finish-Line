using log4net;
using log4net.Config;

namespace FinishLine.Library.Infrastructure.Logging
{
    public class Log4NetAdapter : ILogger
    {
        private readonly log4net.ILog _log;

        public Log4NetAdapter()
        {
            XmlConfigurator.Configure();

            //TODO:  Externalize this setting so that you can switch between loggers at runtime
            _log = LogManager.GetLogger("TantaCommLogger");
        }

        public void Log(string message)
        {
            _log.Info(message);
        }
    }
}
