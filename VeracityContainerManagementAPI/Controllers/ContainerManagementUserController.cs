using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Threading.Tasks;
using System.Web;
using System.Web.Http;
using VeracityContainerManagementAPI.DB;
using VeracityContainerManagementAPI.Helpers;

namespace VeracityContainerManagementAPI.Controllers
{

    [RoutePrefix("api/ContainerManagementUser")]
    public class ContainerManagementUserController : ApiController
    {
        private readonly IDataModel _Db;
        IVeracityUserHelper _userHelper;
        public ContainerManagementUserController(IDataModel DB, IVeracityUserHelper UserHelper)
        {
            _Db = DB;
            _userHelper = UserHelper;
        }
    

        [HttpPost]
        [Route("AddContainerManagementUser")]
        public Task<HttpResponseMessage> AddContainerManagementUser(string username, string email, Guid dnvglUserId)
        {
            HttpResponseMessage response = new HttpResponseMessage();
            var veracityUser = _userHelper.IsValidVeracityUser(dnvglUserId);
            if (veracityUser)
            {
                var checkUser = _Db.Users.Where(a => a.UserId == dnvglUserId);

                //Add user if not added already
                if (checkUser.Count() < 1)
                {
                    _Db.Users.Add(new Users { Email = email, UserId = dnvglUserId, UserName = username });
                   
                }

                //Add user if not added already
                var checkManagementUser = _Db.ContainerManagementUser.Where(a => a.UserId == dnvglUserId);
                if(checkManagementUser.Count() < 1)
                {
                    _Db.ContainerManagementUser.Add(new ContainerManagementUsers { CMUserId = Guid.NewGuid(), UserId = dnvglUserId });
                }

                _Db.SaveChanges();
                response.StatusCode = System.Net.HttpStatusCode.OK;
            }

            return Task.FromResult(response);
             
        }

        //Other commands



        [HttpPost]
        [Route("AddBearerToken")]
        public Task<HttpResponseMessage> AddBearerToken(Guid ContainerManagementUserId, string Bearer)
        {
            //TODO Check validity of the Bearer token
            throw new NotImplementedException();
        }



    }

    

}