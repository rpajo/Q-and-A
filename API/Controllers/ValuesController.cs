using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using API.Models;

namespace API.Controllers
{
    [Route("api/[controller]")]
    public class ValuesController : Controller
    {
        private questionoverflowContext _context;

        public ValuesController(questionoverflowContext context)
        {
            _context = context;
            _context.Database.EnsureCreated();
        }

        // GET api/values
        [HttpGet]
        public Users Get()
        {
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

            return user;
        }

        // GET api/values/5
        [HttpGet("{id}")]
        public string Get(int id)
        {
            return "value";
        }

        // POST api/values
        [HttpPost]
        public void Post([FromBody]string value)
        {
        }

        // PUT api/values/5
        [HttpPut("{id}")]
        public void Put(int id, [FromBody]string value)
        {
        }

        // DELETE api/values/5
        [HttpDelete("{id}")]
        public void Delete(int id)
        {
        }
    }
}
