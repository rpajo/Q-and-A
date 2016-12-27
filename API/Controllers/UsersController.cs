using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using API.Models;
using API.Helpers;
using System.Web.Http;
using Newtonsoft.Json;
using Microsoft.AspNetCore.Cors;

namespace API.Controllers
{

    [Route("api/[controller]")]
    [EnableCors("AllowAll")]
    public class UsersController : ApiController
    {
        /*
        private questionoverflowContext _context;

        public UsersController(questionoverflowContext context)
        {
            _context = context;
            _context.Database.EnsureCreated();
        }
        */
        // GET api/users
        [EnableCors("AllowAll")]
        [HttpGet]
        public ActionResult Get()
        {   
            /*
            Users user = new Models.Users();
            user.UserId = 1;
            user.Username = "burek";
            user.Email = "as@gmail.com";
            user.Password = "adsds123adsads321afa";
            user.Description = "i am a giant douche";
            user.MemberSince = DateTime.Now;
            user.Location = "Ribnica, SLovenija";
            user.Answers = 11;
            user.Questions = 3;
            user.Reputation = -44;
            */
            return Ok("API successfuly started");
        }

        [HttpPut("login")]
        public ActionResult Put([FromBody] Users credentials)
        {
            UsersHelper uh = new UsersHelper();
            int logged = uh.login(credentials);

            if (logged == 404) return NotFound();
            else if (logged == -1) return BadRequest();
            else return Ok("User logged in");
        }

        // GET api/users/5
        [HttpGet("{id}")]
        public ActionResult Get(int id)
        {
            UsersHelper uh = new UsersHelper();
            Users user = uh.getUser(id);

            return Ok(user);
        }

        // POST api/users
        [HttpPost]
        public ActionResult Post([FromBody]Users value)
        {
            UsersHelper uh = new UsersHelper();
            int id = (int)uh.savePerson(value);
            value.UserId = id;

            if (id <= 0) return BadRequest("Query not successful - user not created");

            else {
                return Created(new Uri(Request.RequestUri, String.Format("users/{0}",id)), JsonConvert.SerializeObject(value));
            }
        }

        // PUT api/users/5
        [HttpPut("{id}")]
        public ActionResult Put(int id, [FromBody]Users value)
        {
            UsersHelper uh = new UsersHelper();
            bool success = uh.updateUser(id, value);

            if (success) return Ok("User with successfuly changed");

            else return BadRequest("User not found");
        }

        // DELETE api/users/5
        [HttpDelete("{id}")]
        public void Delete(int id)
        {
        }
    }
}
