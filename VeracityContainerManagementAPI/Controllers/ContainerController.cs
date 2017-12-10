using System;
using System.Linq;
using System.Net.Http;
using System.Threading.Tasks;
using System.Web.Http;
using VeracityContainerManagementAPI.DB;

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
        public Task<IHttpActionResult> GetContainerDetails()
        {
            throw new NotImplementedException();
        }


    }
}