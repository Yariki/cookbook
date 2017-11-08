using Cookbook.Client.Module.Core.Data.Models;
using Cookbook.Client.Module.Interfaces.MVVM;
using Cookbook.Client.Module.ViewModel;
using Prism.Events;


namespace Cookbook.Client.Module.Core.Events
{
    public class BSCloseRecipeEvent : PubSubEvent<IBSDataViewModel>
    {   
    }
}