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
            string bearer  = "eyJ0eXAiOiJKV1QiLCJhbGciOiJSUzI1NiIsImtpZCI6ImkxdV9uQ3B2NmNxMm1RcGZjOGdGbWVHQkltYUhWMlEzS1I3aWFMUXRHYXMifQ.eyJpc3MiOiJodHRwczovL2xvZ2luLm1pY3Jvc29mdG9ubGluZS5jb20vZWQ4MTUxMjEtY2RmYS00MDk3LWI1MjQtZTJiMjNjZDM2ZWI2L3YyLjAvIiwiZXhwIjoxNTEzMTk4NzI1LCJuYmYiOjE1MTMxMTIzMjUsImF1ZCI6ImM2MjgwODA0LTMwZjUtNDYxYi04YTE0LTcyYWExMWM1YjllYyIsInVzZXJJZCI6IjE1NzVEMzVFLUUyODctNDUwQy04QzBCLURCQUY1QTI2RDlGNCIsImdpdmVuX25hbWUiOiJTaGFkIiwiZmFtaWx5X25hbWUiOiJLaGVsIiwibmFtZSI6IktoZWwsIFNoYWQiLCJkbnZnbEFjY291bnROYW1lIjoiU0hBREsiLCJteURudmdsR3VpZCI6IjE1NzVEMzVFLUUyODctNDUwQy04QzBCLURCQUY1QTI2RDlGNCIsInN1YiI6Ik5vdCBzdXBwb3J0ZWQgY3VycmVudGx5LiBVc2Ugb2lkIGNsYWltLiIsIm9pZCI6ImQ5M2Q4NmRkLTEzYzQtNDNhZS1hM2MyLWVkNzlkYzFiZTRhZiIsImVtYWlsIjpbIlNoYWQuS2hlbEBkbnZnbC5jb20iXSwidXBuIjpbIlNoYWQuS2hlbEBkbnZnbC5jb20iXSwic2NwIjoidXNlcl9pbXBlcnNvbmF0aW9uIiwiYXpwIjoiYzUyY2RlMGYtYjQ1ZS00ZDJkLWJiMDItODk5OWE1ZmRhNmEwIiwidmVyIjoiMS4wIiwiaWF0IjoxNTEzMTEyMzI1fQ.EwN7icivx1W3teSDiAwSlYLV8H8pI-NbVE27n1rwFOCqKqJVnMy5ZTRZ6V0SClHGDqLo9Zti93Xr5P2rWJE7sHgLvZClQo7mnARIfeFdn8QiftFtwdqLQnZpH6KzXsFTKPNKuLmlBvNwS5TQBOyXv0Pfr0jSxW_fQgb483hszrwuJC-TWF37GfwPWmOHPS2FGkG3x77wvA0wRfhtMHNHAbdeKESv_QmbBUuqKpTPo10ToZidticVpR-VaS7DduTrwKGIh7S1XK-PMrSi_dX-vX44zbdjayXNued54UqCg24q0uCxBVrgjSkMGxxmeWX4dfAKJn3j5pyG_Tl6XlctHg";
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