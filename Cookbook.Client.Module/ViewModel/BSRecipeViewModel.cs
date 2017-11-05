using Cookbook.Client.Module.Core.MVVM;
using Cookbook.Client.Module.Interfaces.MVVM;
using Cookbook.Client.Module.Interfaces.View;
using Cookbook.Client.Module.Interfaces.ViewModel;
using Microsoft.Practices.Unity;
using Prism.Events;

namespace Cookbook.Client.Module.ViewModel
{
    public class BSRecipeViewModel : BSDataViewModel, IBSRecipeViewModel
    {
        public BSRecipeViewModel(IUnityContainer unityContainer, IEventAggregator eventAggregator, IBSRecipeView view) 
            : base(unityContainer, eventAggregator, view)
        {
        }
    }
}