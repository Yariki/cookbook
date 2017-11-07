using System;
using System.Collections.ObjectModel;
using System.Windows.Input;
using Cookbook.Client.Module.Core;
using Cookbook.Client.Module.Core.Data.Models;
using Cookbook.Client.Module.Core.Events;
using Cookbook.Client.Module.Core.Extensions;
using Cookbook.Client.Module.Core.MVVM;
using Cookbook.Client.Module.Interfaces.MVVM;
using Cookbook.Client.Module.Interfaces.View;
using Cookbook.Client.Module.Interfaces.ViewModel;
using Microsoft.Practices.Unity;
using Prism.Events;
using Xceed.Wpf.AvalonDock;
using Xceed.Wpf.DataGrid;

namespace Cookbook.Client.Module.ViewModel
{
    public class BSMainWorkspaceViewModel : BSBaseViewModel, IBSMainWorkspaceViewModel
    {
        private SubscriptionToken addRecipeToken;
        private SubscriptionToken cancelRecipeToken;
        private SubscriptionToken editRecipeToken;

        public BSMainWorkspaceViewModel(IUnityContainer unityContainer, IEventAggregator eventAggregator, IBSMainWorkspaceView view) 
            : base(unityContainer, eventAggregator, view)
        {

        }
        
        public ICommand ClosingCommand { get; set; }

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
            InitEventAggregator();
            ClosingCommand = new BSRelayCommand(OnClosingExecute,o => true);
            Items = new ObservableCollection<IBSBaseViewModel>();
            var recipeGrid = UnityContainerExtensions.Resolve<IBSRecipeGridViewModel>(UnityContainer);
            recipeGrid.Initialize();
            Items.Add(recipeGrid);
        }

        private void OnClosingExecute(object arg)
        {
            var a = arg as DocumentClosingEventArgs;
            if (a.IsNull())
            {
                return;
            }
            var dataVM = a.Document.Content as BSDataViewModel;
            if (dataVM.IsNull())
            {
                return;
            }
            dataVM.Closing();
            Items.Remove(dataVM);
            dataVM.Dispose();
        }


        private void InitEventAggregator()
        {
            var addEvent = EventAggregator?.GetEvent<BSAddRecipeEvent>();
            if (addEvent != null)
            {
                addRecipeToken = addEvent.Subscribe(OnAddRecipe);
            }
            var cancelRecipe = EventAggregator?.GetEvent<BSCancelRecipeEvent>();
            if (cancelRecipe.IsNotNull())
            {
                cancelRecipeToken = cancelRecipe.Subscribe(OnCancelRecipe);
            }
            var editRecipe = EventAggregator?.GetEvent<BSEditRecipeEvent>();
            if (editRecipe.IsNotNull())
            {
                editRecipeToken = editRecipe.Subscribe(OnEditRecipe);
            }
        }

        private void OnEditRecipe(BSRecipe obj)
        {
            if (obj.IsNull())
            {
                return;
            }
            CreateRecipeViewModel(ViewMode.Edit,obj);
        }

        private void OnCancelRecipe(IBSDataViewModel obj)
        {
            if (obj.IsNull())
            {
                return;
            }
            Items.Remove(obj);
            obj.Dispose();
        }

        private void OnAddRecipe(object obj)
        {
            CreateRecipeViewModel(ViewMode.Add,new BSRecipe());
        }

        private void CreateRecipeViewModel(ViewMode mode, BSRecipe recipe)
        {
            var recipeVM = UnityContainerExtensions.Resolve<IBSRecipeViewModel>(UnityContainer);
            recipeVM.SetBusinessObject(mode,recipe);
            recipeVM.Initialize();
            Items.Add(recipeVM);
            CurrentItem = recipeVM;
        }

    }
}