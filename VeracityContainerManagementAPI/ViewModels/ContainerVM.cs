using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace VeracityContainerManagementAPI.ViewModels
{
    public class ContainerVM
    {
       public Guid ContainerId { get; set; }
       public string ContainerName { get; set; }
       public Guid OwnerId { get; set; }
    }
}