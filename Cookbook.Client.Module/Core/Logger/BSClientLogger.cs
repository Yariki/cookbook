using Cookbook.Client.Module.Interfaces.Logger;
using NLog;

namespace Cookbook.Client.Module.Core.Logger
{
    public class BSClientLogger : IBSClientLogger
    {
        private static NLog.Logger Logger => LogManager.GetCurrentClassLogger();


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