namespace Cookbook.Client.Module.Interfaces.MVVM
{
    public interface IBSBaseViewModel
    {
        IBSView View { get; }
        string Title { get; }
        bool Closing();
        void Initialize();
    }
}