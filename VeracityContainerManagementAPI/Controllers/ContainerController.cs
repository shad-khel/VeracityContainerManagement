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
            return Ok();
        }

        [HttpGet]
        [Route("GetContainerDetails")]
        public Task<IHttpActionResult> GetContainerDetails()
        {
            return Ok();
        }


    }
}