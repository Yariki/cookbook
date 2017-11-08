using System;
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
            try
            {
                var request = new RestRequest(Method.POST);
                request.AddJsonBody(recipe);
                var response = client.Execute(request);
                if (response.StatusCode == HttpStatusCode.Created)
                {
                    return true;
                }
                Logger.Warning(response.Content);
            }
            catch (Exception e)
            {
                Logger.Error(e.ToString());
            }
            return false;
        }

        public bool UpdateRecipe(BSRecipe recipe)
        {
            try
            {
                var request = new RestRequest(Method.PUT);
                request.AddJsonBody(recipe);
                var response = client.Execute(request);
                if (response.StatusCode == HttpStatusCode.OK)
                {
                    return true;
                }
                Logger.Warning(response.Content);
            }
            catch (Exception e)
            {
                Logger.Error(e.ToString());
            }
            return false;
        }

        public bool DeleteRecipe(int id)
        {
            try
            {
                var request = new RestRequest($"/{id}", Method.DELETE);
                var responce = client.Execute(request);
                if (responce.StatusCode == HttpStatusCode.OK)
                {
                    return true;
                }
                Logger.Warning(responce.Content);
            }
            catch (Exception e)
            {
                Logger.Error(e.ToString());
            }
            return false;
        }


    }
}