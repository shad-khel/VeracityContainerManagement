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
        public Task<HttpResponseMessage> AllowUserGroupAccessToContainerGroup(Guid userGroupId, Guid containerGroupId, Guid OwnerId, Guid keyTemplateId = new Guid())
        { 
            var containerGroup = _Db.ContainerGroups.FirstOrDefault(a => a.ContainerGroupId == containerGroupId);
            Guid KeyType;

            //If no key is supplied then try default key from the container group
            if (keyTemplateId == new Guid()) {
                 KeyType = containerGroup.DefaultKeyTemplateId;
            }else
            {
                KeyType = keyTemplateId;
            }


            //Save the key in the access sharing object
            var userGroup = _Db.UserGroups.FirstOrDefault(a => a.UserGroupId == userGroupId);

            var containerAccess = _Db.ContainerAccess
                                                    .FirstOrDefault(a => a.ContainerGroupId == containerGroupId
                                                    && a.UserGroupId == userGroupId); 

            if (containerAccess == null)
            {
                _Db.ContainerAccess.Add(new ContainerAccess
                {
                    ContainerAccessId = Guid.NewGuid(),
                    ContainerGroupId = containerGroupId,
                    UserGroupId = userGroupId,
                    OwnerId = OwnerId,
                    DateTimeAdded = DateTime.Now,
                    AccessKeyId = KeyType
                });
                _Db.SaveChanges();
            }

            //Give everyone in group access to all containers in group
            var ccol = containerGroup.Containers.Select(a=>a.ContainerId).ToList();
            var ucol = userGroup.Users.Select(a => a.UserId).ToList();
            _veracityHelper.ShareResource(ucol, ccol, KeyType);

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


            var containerAccess = _Db.ContainerAccess
                                                   .FirstOrDefault(a => a.ContainerGroupId == containerGroupId
                                                   && a.UserGroupId == userGroupId);
            if (containerAccess != null) {
                _Db.ContainerAccess.Remove(containerAccess);
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