using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Threading.Tasks;
using System.Web.Http;
using VeracityContainerManagementAPI.DB;
using VeracityContainerManagementAPI.ViewModels;
using System.Data.Entity;
using VeracityContainerManagementAPI.Helpers;

namespace VeracityContainerManagementAPI.Controllers
{
    [RoutePrefix("api/Container")]
    public class ContainerController : ApiController
    {
        
        private readonly IDataModel _Db;
        private readonly IVeracityContainerHelper _ContainerHelper;
        public ContainerController(IDataModel db, IVeracityContainerHelper ContainerHelper)
        {
            _Db = db;
            _ContainerHelper = ContainerHelper;
        }


        [HttpPost]
        [Route("AddContainer")] 
        public Task<HttpResponseMessage> AddContainer(Guid containerId, string containerName, Guid OwnerId)
        {
            //TODO remove tightly coupled database
            Containers container = new Containers { ContainerId = containerId, ContainerName = containerName,  OwnerId = OwnerId};
            var check = _Db.Containers.Where(a => a.ContainerId == container.ContainerId);
            
            //check container in DB
            if (check.Count() < 1)
            {
                _Db.Containers.Add(container);
                _Db.SaveChanges();
            }

            HttpResponseMessage response = new HttpResponseMessage()
            {
                StatusCode = System.Net.HttpStatusCode.OK
            };
            return Task.FromResult(response);

        }

        [HttpPost]
        [Route("PopulateContainers")]
        public Task<HttpResponseMessage> PopulateContainersFromVaracity()
        { 
            _ContainerHelper.AddExistingContainersToDatabase();

            HttpResponseMessage response = new HttpResponseMessage()
            {
                StatusCode = System.Net.HttpStatusCode.OK
            };
            return Task.FromResult(response);
        }



        [HttpGet]
        [Route("GetContainerDetails")]
        public Containers GetContainerDetails(Guid containerId)
        {
            return _Db.Containers.FirstOrDefault(a => a.ContainerId == containerId);
        }

        [HttpGet]
        [Route("ListUsersWithAccessToContainer")]
        public List<ContainerAccessVM> ListUsersWithAccessToContainer(Guid containerId)
        {

            

            var ret = new List<ContainerAccessVM>();

            //Experiment retrive users by looping through navigation properties
            //TODO can this be simplified with a LinqQuery or Join?

            //_Db.Containers.Join(_Db.ContainerGroups, c => c.ContainerId, g => g.Containers, (f, b) => new { f, b });



            var containerGroups = _Db.Containers.First(a => a.ContainerId == containerId).ContainerGroups;

            foreach (var c in containerGroups)
            {
                var accessrecord = _Db.ContainerAccess.Where(a => a.ContainerGroupId == c.ContainerGroupId);

                foreach (var access in accessrecord)
                {
                    var usergroup = _Db.UserGroups.First(a => a.UserGroupId == access.UserGroupId);
                    foreach (var user in usergroup.Users)
                    {
                        ret.Add(new ContainerAccessVM {
                            Email = user.Email,
                            UserId = user.UserId,
                            UserName = user.UserName,
                            KeyTemplateId = access.AccessKeyId,
                            UserGroupName = usergroup.UserGroupName
                        });
                    }
                     
                }

                 
            }

            return ret;
        }
    }
}