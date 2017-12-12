using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Threading.Tasks;
using System.Web.Http;
using VeracityContainerManagementAPI.DB;
using VeracityContainerManagementAPI.Helpers;

namespace VeracityContainerManagementAPI.Controllers
{
    [RoutePrefix("api/ContainerAccess")]
    public class ContainerAccessController : ApiController
    {
        private readonly IDataModel _Db;
        private readonly IVeracityResourceSharingHelper _veracityHelper;
        public ContainerAccessController(IDataModel db, IVeracityResourceSharingHelper VeracityHelper)
        {
            _Db = db;
            _veracityHelper = VeracityHelper;
        }
         
        [Route("AllowUserGroupAccessToContainerGroup")]
        public Task<HttpResponseMessage> AllowUserGroupAccessToContainerGroup(Guid userGroupId, Guid containerGroupId)
        {
            

            var containerGroup = _Db.ContainerGroups.FirstOrDefault(a => a.ContainerGroupId == containerGroupId);
            var userGroup = _Db.UserGroups.FirstOrDefault(a => a.UserGroupId == userGroupId);

            if (!containerGroup.UserGroups.Contains(userGroup))
            {
                //we need to loop through the users in this user group and foreach share the containers in the the container group
                //_veracityHelper.ShareResource();
                
                    containerGroup.UserGroups.Add(userGroup);
                    _Db.SaveChanges();
                
            }

            HttpResponseMessage response = new HttpResponseMessage()
            {
                StatusCode = HttpStatusCode.OK
            };
            return Task.FromResult(response);

        }

        [Route("RevokeUserGroupAccessToContainerGroup")]
        public Task<HttpResponseMessage> RevokeUserGroupAccessToContainerGroup(Guid userGroupId, Guid containerGroupId)
        {
            var containerGroup = _Db.ContainerGroups.FirstOrDefault(a => a.ContainerGroupId == containerGroupId);
            var userGroup = _Db.UserGroups.FirstOrDefault(a => a.UserGroupId == userGroupId);

            if (containerGroup.UserGroups.Contains(userGroup))
            {
                containerGroup.UserGroups.Remove(userGroup);
                _Db.SaveChanges();
            }

            HttpResponseMessage response = new HttpResponseMessage()
            {
                StatusCode = HttpStatusCode.OK
            };
            return Task.FromResult(response);

        }


    }
}