using System;
using System.ComponentModel;

namespace Cookbook.Client.Module.Interfaces.MVVM
{
    public interface IBSBaseViewModel : IDisposable, INotifyPropertyChanged
    {
        IBSView View { get; }
        string Title { get; }
        bool Closing();
        void Initialize();
    }
}