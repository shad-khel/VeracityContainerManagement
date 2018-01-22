using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace VeracityContainerManagementAPI.ViewModels
{
    public class UserGroupVM
    {
        public Guid UserGroupId {get; set;}
        public string UserGroupName  {get; set;}
        public Guid OwnerId {get; set;}

        public List<UserVM> UsersInGroup { get; set; }

    }
}