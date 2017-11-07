using Cookbook.Client.Module.Core;

namespace Cookbook.Client.Module.Interfaces.MVVM
{
    public interface IBSDataViewModel : IBSBaseViewModel
    {
        void SetBusinessObject(ViewMode mode, object data);
        ViewMode Mode { get; }
        bool HasChanges { get; set; }
        TObj GetBusinessObject<TObj>();
    }
}