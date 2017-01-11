using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using API.Models;
using System.Web.Http;
using Newtonsoft.Json;
using Microsoft.AspNetCore.Cors;
using System.Collections;

namespace API.Controllers
{

    [Route("api/[controller]")]
    [EnableCors("AllowAll")]
    public class UsersController : ApiController
    {
        questionoverflowContext context = new questionoverflowContext();

        // GET api/users 
        [HttpGet]
        public ActionResult Get()
        {

            return Ok("API started");
        }

        [HttpPut("login")]
        public ActionResult Put([FromBody] Users credentials)
        {
            var user = context.Users.SingleOrDefault(u => u.Email == credentials.Email);
            if (user == null)
            {
                return BadRequest("No user with this email");
            }
            else
            {
                if (user.Password == credentials.Password) return Ok(user);
                else return BadRequest("Invalid password");
            }
        }

        // GET api/users/5
        [HttpGet("{id}")]
        public ActionResult Get(int id)
        {
            Users user =  context.Users.FirstOrDefault(u => u.UserId == id);
            return Ok(user);
        }

        // GET api/users/5/recent
        [HttpGet("{id}/recent")]
        public ActionResult GetRecent(int id)
        {
            var recentList = context.Questions.Where(q => q.UserId == id).OrderBy(q => q.Date).Take(8).ToList();

            return Ok(recentList);
        }

        // POST api/users
        [HttpPost]
        public ActionResult Post([FromBody]Users value)
        {

            int existUser = context.Users.Count(u => u.Username == value.Username);
            int  existEmail = context.Users.Count(u => u.Email == value.Email);
            if (existUser == 0 && existEmail == 0)
            {
                Users newUser = new Users
                {
                    Username = value.Username,
                    Email = value.Email,
                    Password = value.Password,
                    MemberSince = DateTime.Now
                };
                context.Add(newUser);
                context.SaveChanges();

                return Ok("User Saved");
            }
            else if (existUser > 0) return BadRequest("Username alerady taken");
            else return BadRequest("Email already taken");
            

        }

        // PUT api/users/5
        [HttpPut("{id}")]
        public ActionResult Put(int id, [FromBody]Users value)
        {

            var user = context.Users.SingleOrDefault(u => u.UserId == id);
            if (user != null)
            {
                user.Description = value.Description;
                user.Location = value.Location;
                context.SaveChanges();
                return Ok("User updated");
            }
            
            else return BadRequest("User not found");
        }

        // DELETE api/users/5
        [HttpDelete("{id}")]
        public ActionResult Delete(int id)
        {
            Users delUser = context.Users.SingleOrDefault(u => u.UserId == id);
            if (delUser != null)
            {
                context.Users.Remove(delUser);
                return Ok("User deleted");
            }
            else return BadRequest("No such user");
        }
    }
}
