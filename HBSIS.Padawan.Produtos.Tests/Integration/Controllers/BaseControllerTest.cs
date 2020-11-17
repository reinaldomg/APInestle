using HBSIS.Padawan.Produtos.Web;
using Microsoft.AspNetCore.Mvc.Testing;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;

namespace HBSIS.Padawan.Produtos.Tests.Integration.Controllers
{
    public abstract class BaseControllerTest
    {
        protected readonly HttpClient Client;
        protected readonly CustomWebApplicationFactory<Startup> Factory;
   
        protected readonly string ControllerUri;
   
        public BaseControllerTest(string controllerUri)
        {
            ControllerUri = controllerUri;
   
            Factory = new CustomWebApplicationFactory<Startup>();
            Client = Factory.CreateClient(new WebApplicationFactoryClientOptions());
        }
   
        public async Task<T> DescerializeResponse<T>(HttpResponseMessage response)
        {
            return JsonConvert.DeserializeObject<T>(await response.Content.ReadAsStringAsync());
        }
    }
}
