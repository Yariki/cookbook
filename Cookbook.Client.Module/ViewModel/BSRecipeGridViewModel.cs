using Cookbook.Client.Module.Core.MVVM;
using Cookbook.Client.Module.Interfaces.MVVM;
using Cookbook.Client.Module.Interfaces.View;
using Cookbook.Client.Module.Interfaces.ViewModel;
using Microsoft.Practices.Unity;
using Prism.Events;

namespace Cookbook.Client.Module.ViewModel
{
    public class BSRecipeGridViewModel : BSBaseViewModel, IBSRecipeGridViewModel
    {
        public BSRecipeGridViewModel(IUnityContainer unityContainer, IEventAggregator eventAggregator, IBSRecipeGridView view) 
            : base(unityContainer, eventAggregator, view)
        {
        }

        public override bool Closing()
        {
            return true;
        }
    }
}