using System.Windows;
using Cookbook.Client.Module;
using Cookbook.Client.Module.Interfaces.ViewModel;
using Microsoft.Practices.Prism.Regions;
using Prism.Modularity;
using Prism.Unity;

namespace Cookbook.Client
{
    public class ClientBootstrapper : UnityBootstrapper
    {
        protected override DependencyObject CreateShell()
        {
            var window = Container.TryResolve<MainWindow>();
            return window;
        }

        protected override IModuleCatalog CreateModuleCatalog()
        {
            var catalog = new ModuleCatalog();
            catalog.AddModule(typeof(BSClientModule));
            return catalog;
        }

        public override void Run(bool runWithDefaultConfiguration)
        {
            base.Run(runWithDefaultConfiguration);
            App.Current.MainWindow = (Window) this.Shell;
            var mainViewModel = Container.TryResolve<IBSMainWorkspaceViewModel>();
            (this.Shell as IMainWindow).Model = mainViewModel;
            var regionManager = Container.TryResolve<IRegionManager>();
            var region = regionManager.Regions["MainRegion"];
            region.Add(mainViewModel.View);

            if (App.Current.MainWindow != null)
            {
                App.Current.MainWindow.Show();                
            }
        }
    }
}