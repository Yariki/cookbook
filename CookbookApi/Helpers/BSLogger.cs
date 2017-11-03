using CookbookApi.Interfaces;
using NLog;

namespace CookbookApi.Helpers
{
    public class BSLogger : IBSLogger
    {
        private static NLog.Logger Logger => LogManager.GetCurrentClassLogger();

        public BSLogger()
        {
            
        }

        public void Error(string message)
        {
#if DEBUG
            System.Diagnostics.Debug.WriteLine(message);
#endif
            Logger?.Error(message);
        }

        public void Debug(string message)
        {
#if DEBUG
            System.Diagnostics.Debug.WriteLine(message);
#endif
            Logger?.Debug(message);
        }

        public void Info(string message)
        {
#if DEBUG
            System.Diagnostics.Debug.WriteLine(message);
#endif
            Logger?.Info(message);
        }

        public void Warning(string message)
        {
#if DEBUG
            System.Diagnostics.Debug.WriteLine(message);
#endif
            Logger?.Warn(message);
        }
    }
}