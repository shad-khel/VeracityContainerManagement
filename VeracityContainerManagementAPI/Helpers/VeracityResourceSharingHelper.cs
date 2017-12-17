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
            string bearer  = "eyJ0eXAiOiJKV1QiLCJhbGciOiJSUzI1NiIsImtpZCI6ImkxdV9uQ3B2NmNxMm1RcGZjOGdGbWVHQkltYUhWMlEzS1I3aWFMUXRHYXMifQ.eyJpc3MiOiJodHRwczovL2xvZ2luLm1pY3Jvc29mdG9ubGluZS5jb20vZWQ4MTUxMjEtY2RmYS00MDk3LWI1MjQtZTJiMjNjZDM2ZWI2L3YyLjAvIiwiZXhwIjoxNTEzNTk0NTk4LCJuYmYiOjE1MTM1MDgxOTgsImF1ZCI6ImM2MjgwODA0LTMwZjUtNDYxYi04YTE0LTcyYWExMWM1YjllYyIsInVzZXJJZCI6IjE1NzVEMzVFLUUyODctNDUwQy04QzBCLURCQUY1QTI2RDlGNCIsImdpdmVuX25hbWUiOiJTaGFkIiwiZmFtaWx5X25hbWUiOiJLaGVsIiwibmFtZSI6IktoZWwsIFNoYWQiLCJkbnZnbEFjY291bnROYW1lIjoiU0hBREsiLCJteURudmdsR3VpZCI6IjE1NzVEMzVFLUUyODctNDUwQy04QzBCLURCQUY1QTI2RDlGNCIsInN1YiI6Ik5vdCBzdXBwb3J0ZWQgY3VycmVudGx5LiBVc2Ugb2lkIGNsYWltLiIsIm9pZCI6ImQ5M2Q4NmRkLTEzYzQtNDNhZS1hM2MyLWVkNzlkYzFiZTRhZiIsImVtYWlsIjpbIlNoYWQuS2hlbEBkbnZnbC5jb20iXSwidXBuIjpbIlNoYWQuS2hlbEBkbnZnbC5jb20iXSwic2NwIjoidXNlcl9pbXBlcnNvbmF0aW9uIiwiYXpwIjoiYzUyY2RlMGYtYjQ1ZS00ZDJkLWJiMDItODk5OWE1ZmRhNmEwIiwidmVyIjoiMS4wIiwiaWF0IjoxNTEzNTA4MTk4fQ.dWkrwFqfLuqGRLjIkgilrtvocDz63opZ_O5-zKTy-SYaa73MIfE5t4qZ74fGo_5GkZan2DxIyURXvYDdTzHIhdivunvE9eO3z4Ph9AByaXwlKVr0sBdc9NNMLcMuSf-KGPZJiwlrVk2tb5R9EhUQZoZcvaPeb1KlMMLwGdvB41jEdQNcntnFD6ZR4Vjfh7QNq8mbzoQzPIgUqi66cNrg-ZvVSA6GNXfaM5V6T0mFDiVgzU301svTYe8deWno11XcIKmiMadRn8euTOdZ57ZexGt7fBLMJk6Cn0O-xW64kaGt1CQriYnIgA7-P8bOsY0KItEF7da8kJFQFpyX212vPw";
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