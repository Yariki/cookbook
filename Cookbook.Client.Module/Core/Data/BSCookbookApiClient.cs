using System.Collections.Generic;
using System.Net;
using System.Security.Policy;
using System.Threading.Tasks;
using Cookbook.Client.Module.Core.Data.Models;
using Cookbook.Client.Module.Interfaces.Data;
using RestSharp;

namespace Cookbook.Client.Module.Core.Data
{
    public class BSCookbookApiClient : IBSCookbookApiClient
    {
        
        private readonly  RestClient client = new RestClient("http://localhost:51697/api/recipe");
        
        public BSCookbookApiClient()
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
            var response = await client.ExecuteGetTaskAsync<IEnumerable<BSRecipe>>(request);
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

        public bool CreateRecipe(BSRecipe recipe)
        {
            var request = new RestRequest(Method.POST);
            request.AddObject(recipe);
            var response = client.Execute(request);
            return response.StatusCode == HttpStatusCode.OK;
        }

        public bool UpdateRecipe(BSRecipe recipe)
        {
            var request = new RestRequest(Method.PUT);
            request.AddObject(recipe);
            var response = client.Execute(request);
            return response.StatusCode == HttpStatusCode.OK;
        }

        public bool DaleteRecipe(int id)
        {
            var request = new RestRequest($"/{id}", Method.DELETE);
            var responce = client.Execute(request);
            return responce.StatusCode == HttpStatusCode.OK;
        }


    }
}