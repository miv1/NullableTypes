﻿namespace Jalasoft.NullableTypes.Api.Controllers
{
    using System;
    using System.Collections.Generic;

    using Jalasoft.NullableTypes.Api.Model;
    using Jalasoft.NullableTypes.Api.Services;
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

        ////[Route("~/api/user")]

        [HttpGet("{id}")]
        public ActionResult<User> GetUser(int id)
        {
            return this.Ok(this.service.GetUser(id));
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