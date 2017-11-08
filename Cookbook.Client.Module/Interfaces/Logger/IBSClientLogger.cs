namespace Cookbook.Client.Module.Interfaces.Logger
{
    public interface IBSClientLogger
    {
        void Error(string message);
        void Debug(string message);
        void Info(string message);
        void Warning(string message);
    }
}