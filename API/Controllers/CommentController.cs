using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using API.Models;
using API.Helpers;
using System.Web.Http;
using Newtonsoft.Json;
using System.Collections;
using Microsoft.AspNetCore.Cors;

namespace API.Controllers
{

    [Route("api/comment")]
    [EnableCors("AllowAll")]
    public class CommentControler : ApiController
    {
        // COMMENTS API CALLS --------------------------------------------

        // GET api/comment/{quiestionId}
        [HttpGet("{questionId}")]
        public ActionResult Get(int questionId)
        {
            CommentHelper ch = new CommentHelper();
            ArrayList commentList = ch.getAll(questionId);

            if (commentList == null) return BadRequest();

            else return Ok(commentList);
        }

        // POST api/comment/{questionId}
        [HttpPost("{questionId}")]
        public ActionResult Post(int questionId, [FromBody]Comments value)
        {
            CommentHelper ch = new CommentHelper();
            int commentId = (int)ch.newComment(questionId, value);
            value.CommentId = commentId;

            if (commentId <= 0) return BadRequest("Query not successful - comment not posted");

            else
            {
                return Created(new Uri(Request.RequestUri, String.Format("comment/{0}", commentId)), JsonConvert.SerializeObject(value));
            }
        }

        // PUT api/comment/{questionId}
        [HttpPut("{questionId}")]
        public ActionResult Put(int id, [FromBody]Answers value)
        {
            return NotFound();
        }
    }
}
