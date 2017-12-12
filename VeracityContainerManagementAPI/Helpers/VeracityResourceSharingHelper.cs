using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Threading;
using System.Threading.Tasks;
using System.Web;
using System.Web.Script.Serialization;

namespace VeracityContainerManagementAPI.Helpers
{

    public interface IVeracityResourceSharingHelper
    {
        bool ShareResource(Guid resourceId, Guid userId, Guid accessKeyTemplateId);
    }

    public class VeracityResourceSharingHelper: IVeracityResourceSharingHelper
    {
        private HttpClient _client;

        public VeracityResourceSharingHelper(HttpClient client)
        {
              _client = client;
        }

        public class ShareResourceVM
        {
            public Guid userId { get; set; }
            public Guid accessKeyTemplateId { get; set; }
        }

        public bool ShareResource(Guid resourceId, Guid userId, Guid accessKeyTemplateId)
        {
            //TODO Move these to web configuration
            string bearer  = "...";
            string baseUri = "https://ne1dnvglmwappdatavcvp01.azurewebsites.net/";
            //TODO This value must be returned from the type of share which should be is a parameter taken on the access grant
            bool autorefreshed = true;
             
          
            _client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", bearer);
            //_client.DefaultRequestHeaders.Add("Ocp-Apim-Subscription-Key", apiKey)

            _client.BaseAddress = new Uri(baseUri);
            
            var body = new ShareResourceVM
            {
                accessKeyTemplateId = accessKeyTemplateId,
                userId = userId
            };

            var serializer = new JavaScriptSerializer();
            var serializedResult = serializer.Serialize(body);

            var content = new StringContent(serializedResult.ToString(), System.Text.Encoding.UTF8, "application/json");
            var result = _client.PostAsync($"/resources/{resourceId}/accesses?autoRefreshed={autorefreshed}", content);


            return result.Result.IsSuccessStatusCode;
        }

    }

    
}