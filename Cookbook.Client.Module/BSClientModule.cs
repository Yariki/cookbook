using Cookbook.Client.Module.Core.Data;
using Cookbook.Client.Module.Interfaces.Data;
using Cookbook.Client.Module.Interfaces.View;
using Cookbook.Client.Module.Interfaces.ViewModel;
using Cookbook.Client.Module.View;
using Cookbook.Client.Module.ViewModel;
using Microsoft.Practices.Unity;
using Prism.Modularity;

namespace Cookbook.Client.Module
{
    public class BSClientModule : IModule
    {
        private IUnityContainer unityContainer;

        public BSClientModule(IUnityContainer container)
        {
            unityContainer = container;
        }

        public void Initialize()
        {
            unityContainer.RegisterType<IBSMainWorkspaceView, BSMainWorkspaceView>();
            unityContainer.RegisterType<IBSRecipeGridView, BSRecipeGridView>();
            unityContainer.RegisterType<IBSRecipeView, BSRecipeView>();

            unityContainer.RegisterType<IBSMainWorkspaceViewModel, BSMainWorkspaceViewModel>();
            unityContainer.RegisterType<IBSRecipeGridViewModel, BSRecipeGridViewModel>();
            unityContainer.RegisterType<IBSRecipeViewModel, BSRecipeViewModel>();
            unityContainer.RegisterType<IBSCookbookApiClient, BSCookbookApiClient>();
        }
    }
}