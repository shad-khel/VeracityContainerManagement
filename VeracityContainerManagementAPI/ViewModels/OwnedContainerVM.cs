using Newtonsoft.Json;
using System;

namespace VeracityContainerManagementAPI.ViewModels
{
    public class OwnedContainerVM
    {
  
        [JsonProperty(PropertyName = "resourceId")]
        public Guid ResourceId { get; set; }

        [JsonProperty(PropertyName = "resourceName")]
        public string ResourceName { get; set; }

        [JsonProperty(PropertyName = "resourceUrl")]
        public string resourceUrl { get; set; }

        [JsonProperty(PropertyName = "lastModifiedUTC")]
        public DateTime LastModifiedUTC { get; set; }

        [JsonProperty(PropertyName = "resourceType")]
        public string ResourceType { get; set; }

        [JsonProperty(PropertyName = "resourceRegion")]
        public string ResourceRegion { get; set; }

        [JsonProperty(PropertyName = "ownerId")]
        public Guid OwnerId { get; set; }

    }
}