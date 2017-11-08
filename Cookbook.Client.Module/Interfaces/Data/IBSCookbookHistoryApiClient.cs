using System.Collections.Generic;
using Cookbook.Client.Module.Core.Data.Models;

namespace Cookbook.Client.Module.Interfaces.Data
{
    public interface IBSCookbookHistoryApiClient
    {
        List<BSRecipe> GetHistoryForRecipeById(int id);
    }
}