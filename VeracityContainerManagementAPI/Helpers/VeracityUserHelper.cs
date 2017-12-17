using System;
using System.Net.Http;
using System.Net.Http.Headers;
using Newtonsoft.Json;
using VeracityContainerManagementAPI.ViewModels;

namespace VeracityContainerManagementAPI.Helpers
{

    public interface IVeracityUserHelper
    {
        bool IsValidVeracityUser(Guid DnvglUserID);
    }


    public class VeracityUserHelper: IVeracityUserHelper
    {
        private HttpClient _Client;
         
        public VeracityUserHelper(HttpClient Client)
        {
            _Client = Client;
        }
 

        //Does user exsist on Veracity
        public bool IsValidVeracityUser(Guid DnvglUserID)
        {
            //TODO Move these to web configuration
            string bearer = "...";
            string baseUri = "https://ne1dnvglmwappdatavcvp01.azurewebsites.net/";

            _Client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", bearer);
            _Client.BaseAddress = new Uri(baseUri);

            var response = _Client.GetAsync($"api/users/{DnvglUserID}").Result;

            var obj = JsonConvert.DeserializeObject<VeracityUserVM>(response.Content.ReadAsStringAsync().Result);

            if (obj?.Role != null)
            {
                return true;
            }

            return false;
        }
    }
}