using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Threading.Tasks;
using System.Web.Http;
using VeracityContainerManagementAPI.DB;
using VeracityContainerManagementAPI.ViewModels;

namespace VeracityContainerManagementAPI.Controllers
{
    [RoutePrefix("api/ContainerGroup")]
    public class ContainerGroupController : ApiController
    {
        private readonly IDataModel _Db;

        public ContainerGroupController(IDataModel db)
        {
            _Db = db;
        }


        [HttpGet]
        public List<ContainerGroupVM> GetAllContainerGroups(Guid OwnerId)
        {
            return _Db.ContainerGroups.Where(a => a.OwnerId == OwnerId)
                .Select(a => new ContainerGroupVM() {
                    ContainerGroupId = a.ContainerGroupId,
                    ContainerGroupName = a.ContainerGroupName,
                    OwnerId = a.OwnerId,
                    DefaultKeyTemplateId= a.DefaultKeyTemplateId})
                .ToList();
        }


        [HttpPost]
        [Route("CreateContainerGroup")]
        //Add a container group
        public Task<HttpResponseMessage> CreateContainerGroup(string containerGroupName,  Guid OwnerId, Guid? DefaultKeyTemplateId = null) {
            //Move the keytemplate to the Sharing Action 

            _Db.ContainerGroups.Add(new ContainerGroups {
                ContainerGroupId = Guid.NewGuid() ,
                ContainerGroupName = containerGroupName,
                DefaultKeyTemplateId = DefaultKeyTemplateId,
                OwnerId = OwnerId
            });

            _Db.SaveChanges();

            HttpResponseMessage response = new HttpResponseMessage()
            {
                StatusCode = HttpStatusCode.OK
            };
            return Task.FromResult(response);
        }

        [HttpPost]
        [Route("AddContainerToContainerGroup")]
        //Add a container to container group
        public Task<HttpResponseMessage> AddContainerToContainerGroup(Guid ContainerId, Guid containerGroupId)
        {
             
            var conainerGroup = _Db.ContainerGroups.FirstOrDefault(a => a.ContainerGroupId == containerGroupId); 
            var container = _Db.Containers.FirstOrDefault(a => a.ContainerId == ContainerId);

            var check = conainerGroup.Containers.Contains(container);

            if (!check)
            {
                conainerGroup.Containers.Add(container);
                _Db.SaveChanges();
            }

            HttpResponseMessage response = new HttpResponseMessage()
            {
                StatusCode = HttpStatusCode.OK
            };
            return Task.FromResult(response);
        }

        [HttpPut]
        [Route("RemoveContainerFromContainerGroup")]
        //Remove container from container group
        public Task<HttpResponseMessage> RemoveContainerFromContainerGroup(Guid ContainerId, Guid containerGroupId)
        {
            var conainerGroup = _Db.ContainerGroups.FirstOrDefault(a => a.ContainerGroupId == containerGroupId);
            var container = _Db.Containers.FirstOrDefault(a => a.ContainerId == ContainerId);

            var check = conainerGroup.Containers.Contains(container);

            if (check)
            {
                conainerGroup.Containers.Remove(container);
                _Db.SaveChanges();
            }

            HttpResponseMessage response = new HttpResponseMessage()
            {
                StatusCode = HttpStatusCode.OK
            };
            return Task.FromResult(response);
        }

        [HttpGet]
        [Route("ContainerGroupDetails")]
        //Return container group details
        public Task<HttpResponseMessage> ContainerGroupDetails()
        {
            throw new NotImplementedException();
        }

    }
}