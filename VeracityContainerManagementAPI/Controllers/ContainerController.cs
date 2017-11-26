using System;
using System.Threading.Tasks;
using System.Web.Http;


namespace VeracityContainerManagementAPI.Controllers
{
    [RoutePrefix("api/Container")]
    public class ContainerController : ApiController
    {
        public ContainerController()
        {

        }
        [HttpPost]
        [Route("AddContainer")]
        public Task<IHttpActionResult> AddContainer()
        {
            throw new NotImplementedException();
          
        }

        [HttpGet]
        [Route("GetContainerDetails")]
        public Task<IHttpActionResult> GetContainerDetails()
        {
            throw new NotImplementedException();
        }


    }
}