using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Http;
using System.Web.Routing;
using AutoMapper;

namespace CookbookApi
{
    public class WebApiApplication : System.Web.HttpApplication
    {
        protected void Application_Start()
        {
            Mapper.Initialize(m => m.AddProfile(typeof(MappingProfile)));
            GlobalConfiguration.Configure(WebApiConfig.Register);
        }
    }
}
