using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Threading.Tasks;
using System.Web.Http;
using VeracityContainerManagementAPI.DB;

namespace VeracityContainerManagementAPI.Controllers
{
    [RoutePrefix("api/ContainerAccess")]
    public class ContainerAccessController : ApiController
    {
        private readonly IDataModel _Db;
        public ContainerAccessController(IDataModel db)
        {
            _Db = db;
        }
         
        [Route("AllowUserGroupAccessToContainerGroup")]
        public Task<HttpResponseMessage> AllowUserGroupAccessToContainerGroup(Guid userGroupId, Guid containerGroupId)
        {
            var containerGroup = _Db.ContainerGroups.FirstOrDefault(a => a.ContainerGroupId == containerGroupId);
            var userGroup = _Db.UserGroups.FirstOrDefault(a => a.UserGroupId == userGroupId);

            if (!containerGroup.UserGroups.Contains(userGroup))
            {
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