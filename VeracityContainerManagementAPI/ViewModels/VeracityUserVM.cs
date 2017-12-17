using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace VeracityContainerManagementAPI.ViewModels
{
    public class VeracityUserVM
    {
        [JsonProperty(PropertyName = "userId")]
        public Guid UserId { get; set; }

        [JsonProperty(PropertyName = "companyId")]
        public Guid CompanyId { get; set; }

        [JsonProperty(PropertyName = "role")]
        public string Role { get; set; }
    }
}