using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Web;
using System.Web.Configuration;

namespace VeracityContainerManagementAPI.Helpers
{
    public interface IHttpClientHelperClass
    {
        HttpClient Client { get; }
    }

    public class HttpClientHelperClass: IHttpClientHelperClass 
    {
        public HttpClient Client { get; }
        public HttpClientHelperClass(HttpClient Client)
        {
            var bearer = WebConfigurationManager.AppSettings["BearerToken"];
            var baseUri = WebConfigurationManager.AppSettings["ApiBaseUri"];
            var apiKey = WebConfigurationManager.AppSettings["Ocp-Apim-Subscription-Key"];
            var useApiManager = WebConfigurationManager.AppSettings["IsApiManager"];

            Client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", bearer);
              
            if (useApiManager.ToLower() == "true")
                Client.DefaultRequestHeaders.Add("Ocp-Apim-Subscription-Key", apiKey);

            Client.BaseAddress = new Uri(baseUri);

            this.Client = Client;
            
        } 
    }
}