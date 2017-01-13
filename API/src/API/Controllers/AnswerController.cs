using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using API.Models;
using System.Web.Http;
using Newtonsoft.Json;
using System.Collections;
using Microsoft.AspNetCore.Cors;

namespace API.Controllers
{

    [Route("api/[controller]")]
    [EnableCors("AllowAll")]
    public class AnswerController : ApiController
    {

        questionoverflowContext context = new questionoverflowContext();

        // GET api/answer/{order}/1
        [HttpGet("{order}/{questionId}")]
        public ActionResult Get(string order, int questionId)
        {
            var answerList = new List<Answers>();
            if (order == "date")
            {
                answerList = context.Answers.Where(a => a.QuestionId == questionId).OrderByDescending(q => q.Date).ToList();
            }
            else if (order == "-date")
            {
                answerList = context.Answers.Where(a => a.QuestionId == questionId).OrderBy(q => q.Date).ToList();
            }
            else if (order == "rating")
            {
                answerList = context.Answers.Where(a => a.QuestionId == questionId).OrderByDescending(q => q.Rating).ToList();
            }

            if (answerList == null) return BadRequest();

            else return Ok(answerList);
        }

        // POST api/answer/{questionId}
        [HttpPost("{questionId}")]
        public ActionResult Post(int questionId, [FromBody]Answers answer)
        {
            answer.Date = DateTime.Now;
            context.Add(answer);
            context.SaveChanges();


            return Ok("Answer submited");
        }

        // PUT api/answer/5
        [HttpPut("{id}")]
        public ActionResult Put(int id, [FromBody]Answers value)
        {
            Answers answer = context.Answers.FirstOrDefault(q => q.AnswerId == id);
            if (answer != null)
            {
                answer.Rating = answer.Rating + value.Rating;
                context.SaveChanges();
                return Ok("Answer successfuly updated");
            }

            else return BadRequest("Answer not updated");
        }

        // DELETE api/answers/:id
        [HttpDelete("{id}")]
        public ActionResult Delete(int id)
        {
            Answers delAnswer = context.Answers.SingleOrDefault(a => a.AnswerId == id);
            if (delAnswer != null)
            {
                context.Answers.Remove(delAnswer);
                return Ok("Answer deleted");
            }
            else return BadRequest("No such answer");
        }
    }
}
