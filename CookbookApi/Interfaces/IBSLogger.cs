namespace CookbookApi.Interfaces
{
    public interface IBSLogger
    {

        void Error(string message);

        void Debug(string message);

        void Info(string message);

        void Warning(string message);
    }
}