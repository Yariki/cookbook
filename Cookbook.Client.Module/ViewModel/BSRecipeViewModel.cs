using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Windows.Input;
using Cookbook.Client.Module.Core;
using Cookbook.Client.Module.Core.Data.Models;
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
        
        public override void SetBusinessObject(ViewMode mode, object data)
        {
            base.SetBusinessObject(mode, data);
            Ingredients = new ObservableCollection<BSIngredient>((data as BSRecipe).Ingredients);
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


        public override void Initialize()
        {
            base.Initialize();
            AddIngredientCommand = new BSRelayCommand(OnAddExecuted,o => true);
            RemoveIngredientCommand = new BSRelayCommand(OnRemoveExecuted, o => SelectedIngredient.IsNotNull());
        }


        private BSRecipe Recipe
        {
            get { return GetBusinessObject<BSRecipe>(); }
        }


        protected override string GetTitle()
        {
            return Mode == ViewMode.Add ? $"New Recipe" : $"Edit Recipe {Recipe?.Id}";
        }

        protected override void SaveExecute(object arg)
        {
            try
            {
                var recipe = GetBusinessObject<BSRecipe>();
                recipe.Ingredients = new List<BSIngredient>(Ingredients);
                bool result = false;
                switch (Mode)
                {
                    case ViewMode.Add:
                        result = Client.CreateRecipe(recipe);
                        break;
                    case ViewMode.Edit:
                        result = Client.UpdateRecipe(recipe);
                        break;
                }
                HasChanges = !result;
            }
            catch (Exception exception)
            {
                // TODO add logging                
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



    }
}