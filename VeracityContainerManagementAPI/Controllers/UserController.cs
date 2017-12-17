using System.Threading.Tasks;
using System.Web.Http;
using System;
using VeracityContainerManagementAPI.DB;
using System.Net.Http;
using System.Linq;
using VeracityContainerManagementAPI.Helpers;

namespace VeracityContainerManagementAPI.Controllers
{
    [RoutePrefix("api/User")]
    public class UserController : ApiController
    {
        private readonly IDataModel _Db;
        private readonly IVeracityUserHelper _userHelper;
        public UserController(IDataModel db, IVeracityUserHelper UserHelper)
        {
            _Db = db;
            _userHelper = UserHelper;
        }

        [HttpPost]
        [Route("AddUser")]
        public Task<HttpResponseMessage> AddUser(string username, string email, Guid dnvglUserId)
        {
            HttpResponseMessage response = new HttpResponseMessage();
            var addUser = new Users { Email = email, UserName = username, UserId =  dnvglUserId };
            var veracityUser = _userHelper.IsValidVeracityUser(addUser.UserId);

            if (!veracityUser)
            {
                response.StatusCode = System.Net.HttpStatusCode.NotFound; 
                response.ReasonPhrase = "User does not exist or hold a valid role in Veracity API Identity";
            }

            var check = _Db.Users.Where(a => a.UserId == addUser.UserId);
            //check userId  in DB
            if (check.Count() < 1 && veracityUser)
            {
                _Db.Users.Add(addUser);
                _Db.SaveChanges();
                response.StatusCode = System.Net.HttpStatusCode.OK;
            }
            
            
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