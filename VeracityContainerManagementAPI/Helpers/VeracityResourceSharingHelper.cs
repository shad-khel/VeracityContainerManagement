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
using System.Web.Configuration;

namespace VeracityContainerManagementAPI.Helpers
{

    public interface IVeracityResourceSharingHelper
    {
        bool ShareResource(List<Guid> userList, List<Guid> containerList, Guid accessKeyTemplateId);
        string ShareResourceWithUserOnVeracity(Guid resourceId, Guid userId, Guid accessKeyTemplateId);
    }

    public class VeracityResourceSharingHelper: IVeracityResourceSharingHelper
    {
        private IHttpClientHelperClass helper;

        public VeracityResourceSharingHelper(IHttpClientHelperClass Helper)
        {
            helper = Helper;
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
          
            //TODO This value must be returned from the type of share which should be is a parameter taken on the access grant
            bool autorefreshed = true;
               
            var body = new ShareResourceVM
            {
                accessKeyTemplateId = accessKeyTemplateId,
                userId = userId
            };

            var serializer = new JavaScriptSerializer();
            var serializedResult = serializer.Serialize(body);

            var content = new StringContent(serializedResult, System.Text.Encoding.UTF8, "application/json");
            var result = helper.Client.PostAsync($"/api/resources/{resourceId}/accesses?autoRefreshed={autorefreshed}", content).Result;

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