﻿using System.Collections.Generic;
using System.Threading.Tasks;
using Cookbook.Client.Module.Core.Data.Models;

namespace Cookbook.Client.Module.Interfaces.Data
{
    public interface IBSCookbookReadApiClient
    {
        IEnumerable<BSRecipe> GetAllRecipes();
        Task<IEnumerable<BSRecipe>> GetAllRecipesAsync();
        BSRecipe GetRecipeById(int id);
        Task<BSRecipe> GetRecipeByIdAsync(int id);
    }
}