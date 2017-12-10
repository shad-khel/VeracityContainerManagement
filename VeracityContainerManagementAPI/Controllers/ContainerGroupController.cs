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
    [RoutePrefix("api/ContainerGroup")]
    public class ContainerGroupController : ApiController
    {
        private readonly IDataModel _Db;

        public ContainerGroupController(IDataModel db)
        {
            _Db = db;
        }


        [HttpGet]
        public List<ContainerGroups> GetAllContainerGroups()
        {
            return _Db.ContainerGroups.ToList();
        }


        [HttpPost]
        [Route("CreateContainerGroup")]
        //Add a container group
        public Task<HttpResponseMessage> CreateContainerGroup(string containerGroupName) {

            _Db.ContainerGroups.Add(new ContainerGroups { ContainerGroupId = Guid.NewGuid() , ContainerGroupName = containerGroupName });
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
        public void AddContainerToContainerGroup(Guid ContainerId, Guid containerGroupId)
        {
             
            var conainerGroup = _Db.ContainerGroups.FirstOrDefault(a => a.ContainerGroupId == containerGroupId); 
            var container = _Db.Containers.FirstOrDefault(a => a.ContainerId == ContainerId);

            var check = conainerGroup.Containers.Contains(container);

            if (!check)
            {
                conainerGroup.Containers.Add(container);
                _Db.SaveChanges();
            }
        }

        [HttpPost]
        [Route("RemoveContainerFromContainerGroup")]
        //Remove container from container group
        public void RemoveContainerFromContainerGroup(Guid ContainerId, Guid containerGroupId)
        {
            var conainerGroup = _Db.ContainerGroups.FirstOrDefault(a => a.ContainerGroupId == containerGroupId);
            var container = _Db.Containers.FirstOrDefault(a => a.ContainerId == ContainerId);

            var check = conainerGroup.Containers.Contains(container);

            if (check)
            {
                conainerGroup.Containers.Remove(container);
                _Db.SaveChanges();
            }
        }

        [HttpPost]
        [Route("ContainerGroupDetails")]
        //Return container group details
        public void ContainerGroupDetails()
        {

        }

    }
}