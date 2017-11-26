using System.Threading.Tasks;
using System.Web.Http;
using System;

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
            throw new NotImplementedException();
        }

        [HttpGet]
        [Route("GetUserDetails")]
        public Task<IHttpActionResult> GetUserDetails()
        {
            throw new NotImplementedException();
        }


    }
}