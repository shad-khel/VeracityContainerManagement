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
        private IHttpClientHelperClass httpHelper;

        public VeracityContainerHelper(IDataModel db, IHttpClientHelperClass Client)
        {
            _Db = db;
            httpHelper = Client;

        }

        public bool AddExistingContainersToDatabase()
        { 
            var containerNameLength = 10;
             
            var result = httpHelper.Client.GetAsync($"/api/resources?owned=true").Result;
            var Ownedresources = JsonConvert.DeserializeObject<VeracityResourcesVM>(result.Content.ReadAsStringAsync().Result);

            foreach (var r in Ownedresources.OwnedResources)
            {
                //Guid to lower problem
                if ( _Db.Containers.Where( a => a.ContainerId.ToString().ToUpper() == r.ResourceId.ToString().ToUpper()).Count() == 0) 
                {
                    //TODO save the owner Id details also
                    _Db.Containers.Add(
                        new Containers { ContainerName = r.ResourceName.Substring(0, containerNameLength) , ContainerId = r.ResourceId}
                        );
                }

            }
            _Db.SaveChanges();

            return true;
        }
    }
}