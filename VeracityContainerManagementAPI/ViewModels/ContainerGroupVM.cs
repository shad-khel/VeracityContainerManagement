using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace VeracityContainerManagementAPI.ViewModels
{
    public class ContainerGroupVM
    {
       public Guid ContainerGroupId { get; set; }
       public string ContainerGroupName { get; set; }
       public Guid OwnerId { get; set; }
       public Guid? DefaultKeyTemplateId { get; set; }

       public List<ContainerVM> ContainersInGroup { get; set; }
    }
}