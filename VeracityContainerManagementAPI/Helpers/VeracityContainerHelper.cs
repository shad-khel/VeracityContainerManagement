using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Web;
using System.Web.Configuration;
using VeracityContainerManagementAPI.DB;
using VeracityContainerManagementAPI.ViewModels;

namespace VeracityContainerManagementAPI.Helpers
{

    public interface IVeracityContainerHelper
    {
        bool AddExistingContainersToDatabase();
    }

    public class VeracityContainerHelper: IVeracityContainerHelper
    {
        private readonly IDataModel _Db;
        private HttpClient _client;

        public VeracityContainerHelper(IDataModel db, HttpClient Client)
        {
            _Db = db;
            _client = Client;

        }

        public bool AddExistingContainersToDatabase()
        {
            var bearer = WebConfigurationManager.AppSettings["BearerToken"];
            var baseUri = WebConfigurationManager.AppSettings["ApiBaseUri"];
            var apiKey = WebConfigurationManager.AppSettings["Ocp-Apim-Subscription-Key"];
            var useApiManager = WebConfigurationManager.AppSettings["IsApiManager"];

            _client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", bearer);

            if (useApiManager.ToLower() == "true")
                _client.DefaultRequestHeaders.Add("Ocp-Apim-Subscription-Key", apiKey);

            _client.BaseAddress = new Uri(baseUri);

            var result = _client.GetAsync($"/api/resources?owned=true").Result;
            var Ownedresources = JsonConvert.DeserializeObject<VeracityResourcesVM>(result.Content.ReadAsStringAsync().Result);

            foreach (var r in Ownedresources.OwnedResources)
            {
                //Guid to lower problem
                if ( _Db.Containers.Where( a => a.ContainerId.ToString().ToUpper() == r.ResourceId.ToString().ToUpper()).Count() == 0) 
                {
                    //TODO save the owner Id details also
                    _Db.Containers.Add(new Containers { ContainerName = r.ResourceName, ContainerId = r.ResourceId, ContainerGroups =  });
                    
                }

            }
            _Db.SaveChanges();

            return true;
        }
    }
}