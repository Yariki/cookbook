using Cookbook.Client.Module.Interfaces.MVVM;

namespace Cookbook.Client.Module.Interfaces.ViewModel
{
    public interface IBSRecipeViewModel :  IBSDataViewModel
    {
        int Id { get; set; }
    }
}