using System;

namespace Cookbook.Client.Module.Interfaces.MVVM
{
    public interface IBSBaseViewModel : IDisposable
    {
        IBSView View { get; }
        string Title { get; }
        bool Closing();
        void Initialize();
    }
}