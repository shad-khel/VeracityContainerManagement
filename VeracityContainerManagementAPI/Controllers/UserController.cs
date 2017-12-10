using System.Threading.Tasks;
using System.Web.Http;
using System;
using VeracityContainerManagementAPI.DB;
using System.Net.Http;

namespace VeracityContainerManagementAPI.Controllers
{
    [RoutePrefix("api/User")]
    public class UserController : ApiController
    {
        private readonly IDataModel _Db;
        public UserController(IDataModel db)
        {
            _Db = db;
        }

        [HttpPost]
        [Route("AddUser")]
        public Task<HttpResponseMessage> AddUser(string username, string email)
        {
             var addUser = new Users { Email = email, UserName = username, UserId = Guid.NewGuid() };

            _Db.Users.Add(addUser);
            _Db.SaveChanges();

            HttpResponseMessage response = new HttpResponseMessage()
            {
                StatusCode = System.Net.HttpStatusCode.OK,
                Content = new StringContent("")
            };
            return Task.FromResult(response);
        }

        [HttpGet]
        [Route("GetUserDetails")]
        public Task<IHttpActionResult> GetUserDetails()
        {
            throw new NotImplementedException();
        }


    }
}