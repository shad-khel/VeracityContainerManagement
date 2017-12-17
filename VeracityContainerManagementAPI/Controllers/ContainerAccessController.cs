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
        [HttpPost]
        public Task<HttpResponseMessage> AllowUserGroupAccessToContainerGroup(Guid userGroupId, Guid containerGroupId)
        { 
            var containerGroup = _Db.ContainerGroups.FirstOrDefault(a => a.ContainerGroupId == containerGroupId);
            Guid KeyType = containerGroup.KeyTemplateId;

            var userGroup = _Db.UserGroups.FirstOrDefault(a => a.UserGroupId == userGroupId);

            if (!containerGroup.UserGroups.Contains(userGroup))
            {
                containerGroup.UserGroups.Add(userGroup);
                _Db.SaveChanges();
            }

            //Give everyone in group access to all containers in group
            var ccol = containerGroup.Containers.Select(a=>a.ContainerId).ToList();
            var ucol = userGroup.Users.Select(a => a.UserId).ToList();
            _veracityHelper.ShareResource(ccol, ucol, KeyType);

            HttpResponseMessage response = new HttpResponseMessage()
            {
                StatusCode = HttpStatusCode.OK
            };
            return Task.FromResult(response);

        }

        [Route("RevokeUserGroupAccessToContainerGroup")]
        [HttpPut]
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