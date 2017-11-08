using System.Collections.Generic;
using Cookbook.Client.Module.Core.Data.Models;
using Cookbook.Client.Module.Interfaces.Data;
using RestSharp;

namespace Cookbook.Client.Module.Core.Data
{
    public class BSCookbookHistoryApiClient : IBSCookbookHistoryApiClient
    {
        protected readonly RestClient client = new RestClient("http://localhost:51697/api/history");

        public BSCookbookHistoryApiClient()
        {   
        }

        public List<BSRecipe> GetHistoryForRecipeById(int id)
        {
            var request = new RestRequest($"/{id}");
            var response = client.Execute<List<BSRecipe>>(request);
            return response.Data;
        }
        
    }
}