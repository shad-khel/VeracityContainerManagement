using System.Threading.Tasks;
using System.Web.Http;

namespace VeracityContainerManagementAPI.Controllers
{
    [RoutePrefix("api/User")]
    public class UserController : ApiController
    {

        public UserController()
        {

        }

        [HttpPost]
        [Route("AddUser")]
        public Task<IHttpActionResult> AddUser()
        { 
            return Ok();
        }

        [HttpGet]
        [Route("GetUserDetails")]
        public Task<IHttpActionResult> GetUserDetails()
        {
            return Ok();
        }


    }
}