﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Threading.Tasks;
using System.Web.Http;
using VeracityContainerManagementAPI.DB;
using VeracityContainerManagementAPI.ViewModels;

namespace VeracityContainerManagementAPI.Controllers
{
    [RoutePrefix("api/Container")]
    public class ContainerController : ApiController
    {
        
        private readonly IDataModel _Db;
        public ContainerController(IDataModel db)
        {
            _Db = db;
        }


        [HttpPost]
        [Route("AddContainer")] 
        public Task<HttpResponseMessage> AddContainer(Guid containerId, string containerName)
        {
            //TODO remove tightly coupled database
            Containers container = new Containers { ContainerId = containerId, ContainerName = containerName};
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
            var container = _Db.Containers.FirstOrDefault(a => a.ContainerId == containerId);

            var ret = new List<ContainerAccessVM>();
            
            //Experiment retrive users by looping through navigation properties
            //TODO can this be simplified with a LinqQuery or Join?
            foreach (var c in container.ContainerGroups)
            {
                foreach (var ug in c.UserGroups)
                {
                    foreach (var u in ug.Users)
                        ret.Add(new ContainerAccessVM { Email = u.Email, UserId = u.UserId, UserName = u.UserName });
                }
            }

            return ret;
        }
    }
}