using System.Collections.Generic;
using System.Net;
using System.Security.Policy;
using System.Threading.Tasks;
using Cookbook.Client.Module.Core.Data.Models;
using Cookbook.Client.Module.Interfaces.Data;
using RestSharp;

namespace Cookbook.Client.Module.Core.Data
{
    public class BSCookbookApiClient : BSCookbookReadApiClient, IBSCookbookApiClient
    {   
        public BSCookbookApiClient()
        {   
        }
        
        public bool CreateRecipe(BSRecipe recipe)
        {
            var request = new RestRequest(Method.POST);
            request.AddJsonBody(recipe);
            var response = client.Execute(request);
            return response.StatusCode == HttpStatusCode.Created;
        }

        public bool UpdateRecipe(BSRecipe recipe)
        {
            var request = new RestRequest(Method.PUT);
            request.AddJsonBody(recipe);
            var response = client.Execute(request);
            return response.StatusCode == HttpStatusCode.OK;
        }

        public bool DeleteRecipe(int id)
        {
            var request = new RestRequest($"/{id}", Method.DELETE);
            var responce = client.Execute(request);
            return responce.StatusCode == HttpStatusCode.OK;
        }


    }
}