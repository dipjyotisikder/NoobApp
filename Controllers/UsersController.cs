using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using NoobApp.Data;
using NoobApp.Models;
using NoobApp.ViewModels;

namespace NoobApp.Controllers
{
    [ApiController]
    [Route("Users")]
    public class UsersController : ControllerBase
    {
        private readonly ILogger<UsersController> _logger;
        private readonly AppDbContext _dbContext;

        public UsersController(
            ILogger<UsersController> logger,
            AppDbContext dbContext
            )
        {
            _logger = logger;
            _dbContext = dbContext;
        }

        [HttpGet]
        public ActionResult DisplayUsers()
        {
            var userObjs = _dbContext.Users.Select(x => new UserViewModel
            {
                Id = x.Id,
                FirstName = x.FirstName,
                LastName = x.LastName,
                Email = x.Email
            }).AsEnumerable();
            return Ok(userObjs);
        }

        [HttpGet("singleUser/{userId}")]
        public ActionResult DisplaySingleUser([FromRoute] long userId)
        {
            var userObj = _dbContext.Users
            .Where(x => x.Id == userId)
            .Select(x => new UserViewModel
            {
                Id = x.Id,
                FirstName = x.FirstName,
                LastName = x.LastName,
                Email = x.Email
            })
            .FirstOrDefault();

            return Ok(userObj);
        }

        [HttpDelete("{userId}")]
        public ActionResult DeleteUser([FromRoute] long userId)
        {
            var user = _dbContext.Users.Find(userId);
            _dbContext.Users.Remove(user);
            _dbContext.SaveChanges();

            return Ok("Deleted Users name is : " + user?.FirstName);
        }

        //Get the User by his Id
        //Copy the json body data
        //Change The field value
        //Set The updated data to the Update Model in request Body with The User Id in route

        [HttpPut("{userId}")]
        public ActionResult UpdateUser([FromBody] UpdateUserViewModel model, long userId)
        {
            var userObj = _dbContext.Users.Find(userId);

            userObj.FirstName = model.FirstName;
            userObj.LastName = model.LastName;
            userObj.Email = model.Email;

            _dbContext.SaveChanges();

            return Ok(userObj);
        }

        [HttpPost]
        public ActionResult CreateUser([FromBody] CreateUserViewModel model)
        {
            var createUserModel = new User
            {
                Id = model.Id,
                FirstName = model.FirstName,
                LastName = model.LastName,
                Email = model.Email
            };
            _dbContext.Users.Add(createUserModel);

            try
            {
                var result = _dbContext.SaveChanges();
                return Ok(createUserModel);
            }
            catch (System.Exception e)
            {
                throw e;
            }
        }

    }
}
