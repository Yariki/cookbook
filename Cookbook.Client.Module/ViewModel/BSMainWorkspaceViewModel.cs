using System;
using System.Collections.ObjectModel;
using System.Linq;
using System.Windows.Input;
using Cookbook.Client.Module.Core;
using Cookbook.Client.Module.Core.Data.Models;
using Cookbook.Client.Module.Core.Events;
using Cookbook.Client.Module.Core.Extensions;
using Cookbook.Client.Module.Core.MVVM;
using Cookbook.Client.Module.Interfaces.Data;
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
        private SubscriptionToken closeRecipeToken;
        private SubscriptionToken editRecipeToken;

        public BSMainWorkspaceViewModel(IUnityContainer unityContainer, IEventAggregator eventAggregator, IBSMainWorkspaceView view) 
            : base(unityContainer, eventAggregator, view)
        {

        }

        [Dependency]
        public IBSCookbookReadApiClient ReadApiClient { get; set; }

        public ICommand ClosingCommand { get; private set; }

        public ICommand ShowGridCommand { get; private set; }

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
            ShowGridCommand = new BSRelayCommand(OnShowGridExecuted, o => !Items.Any(r => r is IBSRecipeGridViewModel));
            Items = new ObservableCollection<IBSBaseViewModel>();
            ShowGridCommand.Execute(null);
            Logger.Info("Application has been started...");
        }

        private void OnShowGridExecuted(object obj)
        {
            var recipeGrid = UnityContainer.Resolve<IBSRecipeGridViewModel>();
            recipeGrid.Initialize();
            Items.Add(recipeGrid);
        }

        private void OnClosingExecute(object arg)
        {
            try
            {
                var a = arg as DocumentClosingEventArgs;
                if (a.IsNull())
                {
                    return;
                }
                var dataVM = a.Document.Content as BSBaseViewModel;
                if (dataVM.IsNull())
                {
                    return;
                }
                a.Cancel = dataVM.Closing();
                if (!a.Cancel)
                {
                    dataVM.Dispose();
                }
            }
            catch (Exception e)
            {
                Logger.Error(e.ToString());
            }
        }


        private void InitEventAggregator()
        {
            var addEvent = EventAggregator?.GetEvent<BSAddRecipeEvent>();
            if (addEvent != null)
            {
                addRecipeToken = addEvent.Subscribe(OnAddRecipe);
            }
            var closeRecipeEvent = EventAggregator?.GetEvent<BSCloseRecipeEvent>();
            if (closeRecipeEvent.IsNotNull())
            {
                closeRecipeToken = closeRecipeEvent.Subscribe(OnCloseRecipe);
            }
            var editRecipe = EventAggregator?.GetEvent<BSEditRecipeEvent>();
            if (editRecipe.IsNotNull())
            {
                editRecipeToken = editRecipe.Subscribe(OnEditRecipe);
            }
        }

        private async void OnEditRecipe(BSRecipe obj)
        {
            if (obj.IsNull())
            {
                return;
            }

            var existVM = Items.OfType<IBSRecipeViewModel>().SingleOrDefault(v => v.Id == obj.Id);
            if (existVM.IsNotNull())
            {
                CurrentItem = existVM;
                return;
            }
            
            var recipe = await ReadApiClient?.GetRecipeByIdAsync(obj.Id);
            CreateRecipeViewModel(ViewMode.Edit,recipe);
        }

        private void OnCloseRecipe(IBSDataViewModel obj)
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
            CreateRecipeViewModel(ViewMode.Add,new BSRecipe(){Created = DateTime.Now});
        }

        private void CreateRecipeViewModel(ViewMode mode, BSRecipe recipe)
        {
            var recipeVM = UnityContainer.Resolve<IBSRecipeViewModel>();
            recipeVM.SetBusinessObject(mode,recipe);
            recipeVM.Initialize();
            Items.Add(recipeVM);
            CurrentItem = recipeVM;
        }
    }
}