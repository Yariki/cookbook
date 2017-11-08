using System.Collections.Generic;
using System.Threading.Tasks;
using Cookbook.Client.Module.Core.Data.Models;

namespace Cookbook.Client.Module.Interfaces.Data
{
    public interface IBSCookbookApiClient : IBSCookbookReadApiClient
    {
        Task<bool> CreateRecipe(BSRecipe recipe);
        Task<bool> UpdateRecipe(BSRecipe recipe);
        Task<bool> DeleteRecipe(int id);
    }
}