using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace VeracityContainerManagementAPI.ViewModels
{
    public class ContainerAccessVM
    {
      
        public Guid UserId { get; set; }
      
        public string UserName { get; set; }

        public string Email { get; set; }

        public Guid KeyTemplateId { get; set; }
        public string UserGroupName { get; set; }
    }
}