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
        private IHttpClientHelperClass helper;
         
        public VeracityUserHelper(IHttpClientHelperClass Helper)
        {
            helper = Helper;
        }
 

        //Does user exsist on Veracity
        public bool IsValidVeracityUser(Guid DnvglUserID)
        {
              
            var response = helper.Client.GetAsync($"api/users/{DnvglUserID}").Result;

            var obj = JsonConvert.DeserializeObject<VeracityUserVM>(response.Content.ReadAsStringAsync().Result);

            if (obj?.Role != null)
            {
                return true;
            }

            return false;
        }
    }
}