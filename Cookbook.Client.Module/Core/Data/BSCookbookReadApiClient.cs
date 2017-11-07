using System.Collections.Generic;
using System.Threading.Tasks;
using Cookbook.Client.Module.Core.Data.Models;
using Cookbook.Client.Module.Interfaces.Data;
using RestSharp;

namespace Cookbook.Client.Module.Core.Data
{
    public class BSCookbookReadApiClient : IBSCookbookReadApiClient
    {
        protected readonly RestClient client = new RestClient("http://localhost:51697/api/recipe");

        public BSCookbookReadApiClient()
        {   
        }

        public IEnumerable<BSRecipe> GetAllRecipes()
        {
            var request = new RestRequest(Method.GET);
            var response = client.Execute<List<BSRecipe>>(request);
            return response.Data;
        }

        public async Task<IEnumerable<BSRecipe>> GetAllRecipesAsync()
        {
            var request = new RestRequest(Method.GET);
            var response = await client.ExecuteGetTaskAsync<List<BSRecipe>>(request);
            return response.Data;
        }

        public BSRecipe GetRecipeById(int id)
        {
            var request = new RestRequest($"/{id}");
            var response = client.Execute<BSRecipe>(request);
            return response.Data;
        }

        public async Task<BSRecipe> GetRecipeByIdAsync(int id)
        {
            var request = new RestRequest($"/{id}");
            var response = await client.ExecuteTaskAsync<BSRecipe>(request);
            return response.Data;
        }
    }
}