using System;
using System.Collections.ObjectModel;
using System.Linq;
using System.Windows;
using System.Windows.Input;
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

namespace Cookbook.Client.Module.ViewModel
{
    public class BSRecipeGridViewModel : BSBaseViewModel, IBSRecipeGridViewModel
    {
        public BSRecipeGridViewModel(IUnityContainer unityContainer, IEventAggregator eventAggregator)
            : base(unityContainer, eventAggregator, null)
        {

        }

        [Dependency]
        public IBSCookbookApiClient ClientApi { get; set; }

        public ObservableCollection<BSRecipe> Recipes { get; set; } = new ObservableCollection<BSRecipe>();

        public BSRecipe SelectedItem
        {
            get { return Get<BSRecipe>(); }
            set { Set(value); }
        }

        public ICommand AddRecipeCommand { get; set; }

        public ICommand EditRecipeCommand { get; set; }

        public ICommand DeleteRecipeCommand { get; set; }

        public ICommand RefreshCommand { get; set; }

        public override void Initialize()
        {
            base.Initialize();
            InitCommands();
            RefreshRecipies();
        }
        
        protected override string GetTitle()
        {
            return "Recipes";
        }

        public override bool Closing()
        {
            return true;
        }

        private void InitCommands()
        {
            AddRecipeCommand = new BSRelayCommand(OnAddExecute);
            EditRecipeCommand = new BSRelayCommand(OnEditExecute,OnCanExecute);
            DeleteRecipeCommand = new BSRelayCommand(OnDeleteExecute,OnCanExecute);
            RefreshCommand = new BSRelayCommand(OnRefreshExecute);
            EventAggregator.GetEvent<BSRefreshGridEvent>().Subscribe(o => RefreshRecipies());
        }


        private void OnAddExecute(object arg)
        {
            EventAggregator.GetEvent<BSAddRecipeEvent>().Publish(null);
        }

        private void OnEditExecute(object arg)
        {
            EventAggregator.GetEvent<BSEditRecipeEvent>().Publish(SelectedItem);
        }

        private bool OnCanExecute(object arg)
        {
            return SelectedItem.IsNotNull();
        }

        private async void RefreshRecipies()
        {
            IsBusy = true;
            var collection = await ClientApi?.GetAllRecipesAsync();
            if (collection.IsNotNull() && collection.Any())
            {
                Recipes.Clear();
                Recipes.AddRange(collection);
            }
            IsBusy = false;
        }
        
        private async void OnDeleteExecute(object arg)
        {
            if (SelectedItem.IsNotNull())
            {
                var result = await ClientApi?.DeleteRecipe(SelectedItem.Id);
                if (!result)
                {
                    MessageBox.Show("There is an error. Please, see the log files.");
                }
                RefreshRecipies();
            }
        }

        private void OnRefreshExecute(object arg)
        {
            RefreshRecipies();
        }


    }
}