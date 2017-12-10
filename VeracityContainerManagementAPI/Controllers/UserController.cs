using System.Threading.Tasks;
using System.Web.Http;
using System;
using VeracityContainerManagementAPI.DB;
using System.Net.Http;
using System.Linq;

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
        public Task<HttpResponseMessage> AddUser(string username, string email, Guid dnvglUserId)
        {
            var addUser = new Users { Email = email, UserName = username, UserId =  dnvglUserId };
            var check = _Db.Users.Where(a => a.UserId == addUser.UserId);
            //check userId  in DB
            if (check.Count() < 1)
            {
                _Db.Users.Add(addUser);
                _Db.SaveChanges();
            }

            HttpResponseMessage response = new HttpResponseMessage()
            {
                StatusCode = System.Net.HttpStatusCode.OK
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