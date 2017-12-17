using Newtonsoft.Json;
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
        bool ShareResource(List<Guid> userList, List<Guid> containerList, Guid accessKeyTemplateId);
        string ShareResourceWithUserOnVeracity(Guid resourceId, Guid userId, Guid accessKeyTemplateId);
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

        public bool ShareResource(List<Guid> userList, List<Guid> containerList, Guid accessKeyTemplateId)
        {
            foreach(var u in userList)
            {
                foreach(var c in containerList)
                {
                    var success = ShareResourceWithUserOnVeracity(c, u, accessKeyTemplateId);
                    //Should we store the shareId to be able to revoke it later?

                    //What to do about the resources that have failed

                }
            }

            return true;
        }

        public string ShareResourceWithUserOnVeracity(Guid resourceId, Guid userId, Guid accessKeyTemplateId)
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

            var content = new StringContent(serializedResult, System.Text.Encoding.UTF8, "application/json");
            var result = _client.PostAsync($"/api/resources/{resourceId}/accesses?autoRefreshed={autorefreshed}", content).Result;

            var veracityShare = JsonConvert.DeserializeObject<VeracityShareVM>(result.Content.ReadAsStringAsync().Result);
            return veracityShare?.AccessSharingId;
        }

        public class VeracityShareVM
        {
            
            [JsonProperty(PropertyName = "accessSharingId")]
            public string AccessSharingId { get; set; }
            
        }

    }

    
}