using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
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

          

        [HttpPost]
        [Route("CreateContainerGroup")]
        //Add a container group
        public void CreateContainerGroup()
        {

        }

        [HttpPost]
        [Route("AddContainerToContainerGroup")]
        //Add a container to container group
        public void AddContainerToContainerGroup()
        {

        }

        [HttpPost]
        [Route("RemoveContainerFromContainerGroup")]
        //Remove container from container group
        public void RemoveContainerFromContainerGroup()
        {

        }

        [HttpPost]
        [Route("ContainerGroupDetails")]
        //Return container group details
        public void ContainerGroupDetails()
        {

        }

    }
}