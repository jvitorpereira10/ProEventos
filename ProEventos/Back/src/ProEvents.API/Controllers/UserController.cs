using System.Collections.Generic;
using Microsoft.AspNetCore.Mvc;
using ProEvents.Domain;

namespace ProEvents.API.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class UserController : ControllerBase
    {
        public UserController()
        {
            
        }

        [HttpGet]
        public List<User> GetUsersAsync()
        {
            List<User> users = new List<User>();
            
            users.Add(new User{Id = 1, Name = "João"});
            users.Add(new User{Id = 2, Name = "Vitor"});
            users.Add(new User{Id = 3, Name = "Pereira"});
            users.Add(new User{Id = 4, Name = "Santos"});

            return  users;
        }
    }
}