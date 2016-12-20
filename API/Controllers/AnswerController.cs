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

    //public class Response
    //{
    //    public String status { set; get; }
    //    public 

    //}

    [Route("api/[controller]")]
    public class AnswerController : ApiController
    {
        // ANSWERS API CALLS --------------------------------------------
        // GET api/answer/{order}/1
        [HttpGet("{order}/{questionId}")]
        public ActionResult Get(string order, int questionId)
        {
            AnswerHelper ah = new AnswerHelper();
            ArrayList answerList = ah.getAnswers(questionId, order);

            if (answerList == null) return BadRequest();

            else return Ok(answerList);
        }

        // POST api/answer/{questionId}
        [HttpPost("{questionId}")]
        public ActionResult Post(int questionId, [FromBody]Answers value)
        {
            AnswerHelper ah = new AnswerHelper();
            int answerId = (int)ah.newAnswer(questionId, value);
            value.AnswerId = answerId;

            if (answerId <= 0) return BadRequest("Query not successful - answer not posted");

            else
            {
                return Created(new Uri(Request.RequestUri, String.Format("answer/{0}", questionId)), JsonConvert.SerializeObject(value));
            }
        }

        // PUT api/answer/5
        [HttpPut("{id}")]
        public ActionResult Put(int id, [FromBody]Answers value)
        {
            AnswerHelper ah = new AnswerHelper();
            bool success = ah.updateAnswer(id, value);

            if (success) return Ok("Answer successfuly changed");

            else return BadRequest("Answer not updated");
        }
    }
}
