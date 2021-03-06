﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Threading.Tasks;
using System.Web.Http;
using VeracityContainerManagementAPI.DB;
using VeracityContainerManagementAPI.ViewModels;

namespace VeracityContainerManagementAPI.Controllers
{
    [RoutePrefix("api/UserGroup")]
    public class UserGroupController : ApiController
    {
        private readonly IDataModel _Db;
        public UserGroupController(IDataModel db)
        {
            _Db = db;
        }


        [HttpGet]
        public List<UserGroupVM> GetAllUserGroups(Guid OwnerId)
        {

            return _Db.UserGroups.Where(a => a.OwnerId == OwnerId)
                .Select(a => new UserGroupVM()
                {
                     OwnerId = a.OwnerId,
                     UserGroupName = a.UserGroupName,
                     UserGroupId = a.UserGroupId,
                     UsersInGroup = a.Users
                                        .Select(b => new UserVM() { Email = b.Email, UserId= b.UserId, UserName = b.UserName})
                                        .ToList()
                })
                .ToList();

        }

        [HttpPost]
        [Route("CreateUserGroup")]
        //Add a User group
        public Task<HttpResponseMessage> CreateUserGroup(string userGroupName, Guid OwnerId)
        {
            _Db.UserGroups.Add(new UserGroups { UserGroupId = Guid.NewGuid(), UserGroupName = userGroupName, OwnerId = OwnerId });
            _Db.SaveChanges();

            HttpResponseMessage response = new HttpResponseMessage()
            {
                StatusCode = HttpStatusCode.OK
            };
            return Task.FromResult(response);
        }
         

        [HttpPost]
        [Route("AddUserToUserGroup")]
        //Add a User to UserGroup
        public Task<HttpResponseMessage> AddUserToUserGroup(Guid userId, Guid userGroupId)
        {

            var userGroup = _Db.UserGroups.FirstOrDefault(a => a.UserGroupId == userGroupId);
            var user = _Db.Users.FirstOrDefault(a => a.UserId == userId);

            var check = userGroup.Users.Contains(user);

            if (!check)
            {
                userGroup.Users.Add(user);
                _Db.SaveChanges();
            }
             
            HttpResponseMessage response = new HttpResponseMessage()
            {
                StatusCode = HttpStatusCode.OK
            };
            return Task.FromResult(response);
        }


        [HttpPut]
        [Route("RemoveUserFromUserGroup")]
        //Remove User from UserGroup
        public Task<HttpResponseMessage> RemoveUserFromUserGroup(Guid userId, Guid userGroupId)
        {

            var userGroup = _Db.UserGroups.FirstOrDefault(a => a.UserGroupId == userGroupId);
            var user = _Db.Users.FirstOrDefault(a => a.UserId == userId);

            var check = userGroup.Users.Contains(user);

            if (check)
            {
                userGroup.Users.Remove(user);
                _Db.SaveChanges();
            }


            HttpResponseMessage response = new HttpResponseMessage()
            {
                StatusCode = HttpStatusCode.OK
            };
            return Task.FromResult(response);
        }

        [HttpGet]
        [Route("UserGroupDetails")]
        //Return UserGroup details
        public Task<HttpResponseMessage> UserGroupDetails(Guid userGroupId)
        {
            HttpResponseMessage response = new HttpResponseMessage()
            {
                StatusCode = HttpStatusCode.OK
            };
            return Task.FromResult(response);
        }

    }
}