namespace ApiTest.Controllers
{
    using System;
    using System.Collections.Generic;

    using ApiTest.Model;
    using ApiTest.Services;
    using Microsoft.AspNetCore.Mvc;

    [Route("api/[controller]")]
    [ApiController]
    public class UsersController : ControllerBase
    {
        private IUserService service;

        public UsersController(IUserService service)
        {
            this.service = service;
        }

        // GET api/users
        [HttpGet]
        public ActionResult<IEnumerable<User>> Get()
        {
            return this.Ok(this.service.GetAll());
        }

        // POST api/users
        [Route("~/api/create")]
        [HttpPost]
        public User Post([FromBody] User user)
        {
            Console.WriteLine("POST " + user);

            return this.service.Create(user);
        }

        // PUT api/users/{id}
        [HttpPut("{id}")]
        public User Put(int id, [FromBody] User user)
        {
            return this.service.Update(id, user);
        }

        // DELETE api/users/{id}
        [HttpDelete("{id}")]
        public void Delete(int id)
        {
            this.service.Delete(id);
        }

  
    }
}