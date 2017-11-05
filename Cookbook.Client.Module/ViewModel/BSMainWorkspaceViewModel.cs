using System;
using System.Collections.ObjectModel;
using System.Windows.Input;
using Cookbook.Client.Module.Core.Events;
using Cookbook.Client.Module.Core.MVVM;
using Cookbook.Client.Module.Interfaces.MVVM;
using Cookbook.Client.Module.Interfaces.View;
using Cookbook.Client.Module.Interfaces.ViewModel;
using Microsoft.Practices.Unity;
using Prism.Events;
using Xceed.Wpf.AvalonDock;

namespace Cookbook.Client.Module.ViewModel
{
    public class BSMainWorkspaceViewModel : BSBaseViewModel, IBSMainWorkspaceViewModel
    {
        private SubscriptionToken addRecipeToken;

        public BSMainWorkspaceViewModel(IUnityContainer unityContainer, IEventAggregator eventAggregator, IBSMainWorkspaceView view) 
            : base(unityContainer, eventAggregator, view)
        {

        }
        
        public ICommand ClosingCommand { get; set; } = new BSRelayCommand(null, o => true);

        public ObservableCollection<IBSBaseViewModel> Items { get; set; }

        public IBSBaseViewModel CurrentItem
        {
            get { return Get<IBSBaseViewModel>(); }
            set { Set(value);}
        }
        
        public void OnClosing(object arg)
        {
            var a = arg as DocumentClosingEventArgs;
            if (a != null)
            {
                return;
            }
            a.Cancel = CurrentItem.Closing();
        }

        public override void Initialize()
        {
            base.Initialize();
            Items = new ObservableCollection<IBSBaseViewModel>();
            var recipeGrid = UnityContainer.Resolve<IBSRecipeGridViewModel>();
            recipeGrid.Initialize();
            Items.Add(recipeGrid);
        }


        private void InitEventAggregator()
        {
            var addEvent = EventAggregator?.GetEvent<BSAddRecipeEvent>();
            if (addEvent != null)
            {
                addRecipeToken = addEvent.Subscribe(OnAddRecipe);
            }
        }

        private void OnAddRecipe(object obj)
        {
            
        }
    }
}