using Cookbook.Client.Module.Interfaces.ViewModel;

namespace Cookbook.Client
{
    public interface IMainWindow
    {
        IBSMainWorkspaceViewModel Model { get; set; }
    }
}