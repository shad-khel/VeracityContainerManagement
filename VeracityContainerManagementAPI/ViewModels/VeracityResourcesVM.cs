using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace VeracityContainerManagementAPI.ViewModels
{
    public class VeracityResourcesVM
    {

        [JsonProperty(PropertyName = "ownedResources")]
        public List<OwnedContainerVM> OwnedResources { get; set; }


        //object vm not implemented yet
        [JsonProperty(PropertyName = "sharedResources")]
        public List<string> SharedResources { get; set; }
    }
}