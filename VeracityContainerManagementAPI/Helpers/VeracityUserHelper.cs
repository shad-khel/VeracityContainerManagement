using System;
using System.Net.Http;
using System.Net.Http.Headers;
using Newtonsoft.Json;
using VeracityContainerManagementAPI.ViewModels;
using System.Web.Configuration;

namespace VeracityContainerManagementAPI.Helpers
{

    public interface IVeracityUserHelper
    {
        bool IsValidVeracityUser(Guid DnvglUserID);
    }


    public class VeracityUserHelper: IVeracityUserHelper
    {
        private HttpClient _client;
         
        public VeracityUserHelper(HttpClient Client)
        {
            _client = Client;
        }
 

        //Does user exsist on Veracity
        public bool IsValidVeracityUser(Guid DnvglUserID)
        {
            var bearer = WebConfigurationManager.AppSettings["BearerToken"];
            var baseUri = WebConfigurationManager.AppSettings["ApiBaseUri"];
            var apiKey = WebConfigurationManager.AppSettings["Ocp-Apim-Subscription-Key"];
            var useApiManager = WebConfigurationManager.AppSettings["IsApiManager"];

            _client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", bearer);

            if (useApiManager.ToLower() == "true")
                _client.DefaultRequestHeaders.Add("Ocp-Apim-Subscription-Key", apiKey);

            _client.BaseAddress = new Uri(baseUri);

            var response = _client.GetAsync($"api/users/{DnvglUserID}").Result;

            var obj = JsonConvert.DeserializeObject<VeracityUserVM>(response.Content.ReadAsStringAsync().Result);

            if (obj?.Role != null)
            {
                return true;
            }

            return false;
        }
    }
}