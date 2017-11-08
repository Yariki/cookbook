using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Cookbook.Client.Module.Core.Data.Models;
using Cookbook.Client.Module.Interfaces.Data;
using Cookbook.Client.Module.Interfaces.Logger;
using Microsoft.Practices.Unity;
using RestSharp;

namespace Cookbook.Client.Module.Core.Data
{
    public class BSCookbookReadApiClient : IBSCookbookReadApiClient
    {
        protected readonly RestClient client = new RestClient("http://localhost:51697/api/recipe");

        [Dependency]
        protected IBSClientLogger Logger { get; set; }

        public BSCookbookReadApiClient()
        {   
        }

        public IEnumerable<BSRecipe> GetAllRecipes()
        {
            try
            {
                var request = new RestRequest(Method.GET);
                var response = client.Execute<List<BSRecipe>>(request);
                return response.Data;
            }
            catch (Exception exception)
            {
                Logger.Error(exception.ToString());
            }
            return null;
        }

        public async Task<IEnumerable<BSRecipe>> GetAllRecipesAsync()
        {
            try
            {
                var request = new RestRequest(Method.GET);
                var response = await client.ExecuteGetTaskAsync<List<BSRecipe>>(request);
                return response.Data;
            }
            catch (Exception e)
            {
                Logger.Error(e.ToString());
            }
            return null;
        }

        public BSRecipe GetRecipeById(int id)
        {
            try
            {
                var request = new RestRequest($"/{id}");
                var response = client.Execute<BSRecipe>(request);
                return response.Data;
            }
            catch (Exception e)
            {
                Logger.Error(e.ToString());
            }
            return null;
        }

        public async Task<BSRecipe> GetRecipeByIdAsync(int id)
        {
            try
            {
                var request = new RestRequest($"/{id}");
                var response = await client.ExecuteTaskAsync<BSRecipe>(request);
                return response.Data;
            }
            catch (Exception e)
            {
                Logger.Error(e.ToString());
            }
            return null;
        }
    }
}