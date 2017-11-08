using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Windows;
using System.Windows.Input;
using System.Windows.Media.Imaging;
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

namespace Cookbook.Client.Module.ViewModel
{
    public class BSRecipeViewModel : BSDataViewModel, IBSRecipeViewModel
    {
        public BSRecipeViewModel(IUnityContainer unityContainer, IEventAggregator eventAggregator, IBSRecipeView view) 
            : base(unityContainer, eventAggregator, view)
        {
        }

        [Dependency]
        public IBSCookbookApiClient Client { get; set; }

        [Dependency]
        public IBSCookbookHistoryApiClient HistoryApiClient { get; set; }

        
        public override void SetBusinessObject(ViewMode mode, object data)
        {
            base.SetBusinessObject(mode, data);
            Ingredients = new ObservableCollection<BSIngredient>((data as BSRecipe).Ingredients);
        }

        public int Id
        {
            get { return Get<int>(); }
            set { Set(value); }
        }
        
        public string Name
        {
            get { return Get<string>(); }
            set { Set(value); }
        }
        
        public string Description
        {
            get { return Get<string>(); }
            set { Set(value); }
        }

        public bool IsNew
        {
            get { return Get<bool>(); }
            set { Set(value); }
        }

        
        public ObservableCollection<BSIngredient> Ingredients
        {
            get;
            set;
        }

        public BSIngredient SelectedIngredient
        {
            get { return Get<BSIngredient>(); }
            set { Set(value); }
        }

        public ICommand AddIngredientCommand { get; private set; }

        public ICommand RemoveIngredientCommand { get; private set; }

        public ICommand LoadHistoryCommand { get; private set; }

        public ObservableCollection<BSRecipe> HistoryRecipies { get; set; }

        public ObservableCollection<BSIngredient> HistoryIngredients { get; set; }

        public BSRecipe SelectedHistoryRecipe
        {
            get { return Get<BSRecipe>(); }
            set
            {
                Set(value); 
                HistoryIngredients.Clear();
                if (value.IsNotNull())
                {
                    HistoryIngredients.AddRange(value.Ingredients);
                }
            }
        }
        

        public override void Initialize()
        {
            base.Initialize();
            AddIngredientCommand = new BSRelayCommand(OnAddExecuted,o => true);
            RemoveIngredientCommand = new BSRelayCommand(OnRemoveExecuted, o => SelectedIngredient.IsNotNull());
            LoadHistoryCommand = new BSRelayCommand(OnLoadHistoryExecuted, o => Recipe.IsNotNull());
            HistoryRecipies = new ObservableCollection<BSRecipe>();
            HistoryIngredients = new ObservableCollection<BSIngredient>();
            IsNew = Recipe.Id > 0;
        }
        
        private BSRecipe Recipe
        {
            get { return GetBusinessObject<BSRecipe>(); }
        }


        protected override string GetTitle()
        {
            return Mode == ViewMode.Add ? $"New Recipe" : $"Edit Recipe {Recipe?.Id}";
        }

        protected async override void SaveExecute(object arg)
        {
            try
            {
                IsBusy = true;
                var recipe = GetBusinessObject<BSRecipe>();
                recipe.Ingredients = new List<BSIngredient>(Ingredients);
                bool result = false;
                switch (Mode)
                {
                    case ViewMode.Add:
                        result = await Client.CreateRecipe(recipe);
                        break;
                    case ViewMode.Edit:
                        result = await Client.UpdateRecipe(recipe);
                        break;
                }
                if (!result)
                {
                    MessageBox.Show(
                        "There is an error occur during creating/updating recipe.\nPlease, see the log file.");
                }
                HasChanges = !result;
                EventAggregator.GetEvent<BSRefreshGridEvent>().Publish(null);
                EventAggregator.GetEvent<BSCloseRecipeEvent>().Publish(this);
            }
            catch (Exception exception)
            {
                Logger.Error(exception.ToString());
            }
            finally
            {
                IsBusy = false;
            }
        }

        private void OnRemoveExecuted(object obj)
        {
            Ingredients.Remove(SelectedIngredient);
            HasChanges = true;
        }

        private void OnAddExecuted(object obj)
        {
            Ingredients.Add(new BSIngredient(){RecipeId = Recipe.Id,Created = DateTime.Now});
            HasChanges = true;
        }

        private void OnLoadHistoryExecuted(object obj)
        {
            var list = HistoryApiClient.GetHistoryForRecipeById(Recipe.Id);
            if (list.IsNotNull())
            {
                HistoryRecipies.Clear();
                HistoryRecipies.AddRange(list);
            }
        }
        
    }
}