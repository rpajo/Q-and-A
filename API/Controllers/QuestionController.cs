using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using API.Models;
using API.Helpers;
using System.Net.Http;
using System.Net.Http.Formatting;
using System.Web.Http;
using System.Net;
using Newtonsoft.Json;
using System.Collections;

namespace API.Controllers
{

    [Route("api/[controller]")]
    public class QuestionController : ApiController
    {
        // QUESTION API CALLS -------------------------------------------------
        // GET api/questions/1
        [HttpGet("{questionId}")]
        public ActionResult Get(int questionId)
        {
            QuestionHelper qh = new QuestionHelper();
            Questions question = qh.getQuestion(questionId);

            if (question == null) return NotFound();
            else return Ok(question);
        }

        // GET api/question/top/1
        [HttpGet("{order}/{page}")]
        public ActionResult Get(int page, String order)
        {
            QuestionHelper qh = new QuestionHelper();

            ArrayList questionList = qh.getAll(1, order);

            return Ok(questionList);
        }

        // POST api/users
        [HttpPost]
        public ActionResult Post([FromBody]Questions value)
        {
            QuestionHelper uh = new QuestionHelper();
            int questionId = (int)uh.newQuestion(value);
            value.QuestionId = questionId;

            if (questionId <= 0) return BadRequest("Query not successful - question not posted");

            else {
                return Created(new Uri(Request.RequestUri, String.Format("question/{0}", questionId)), JsonConvert.SerializeObject(value));
            }
        }

        // PUT api/users/5
        [HttpPut("{id}")]
        public ActionResult Put(int id, [FromBody]Questions value)
        {
            QuestionHelper qh = new QuestionHelper();
            bool success = qh.updateQuestion(id, value);

            if (success) return Ok("Question successfuly changed");

            else return BadRequest("Question not updated");
        }

        // DELETE api/users/5
        [HttpDelete("{id}")]
        public void Delete(int id)
        {
        }

    }
}
