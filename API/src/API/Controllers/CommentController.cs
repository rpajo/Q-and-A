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

    [Route("api/comment")]
    [EnableCors("AllowAll")]
    public class CommentControlLer : ApiController
    {
        questionoverflowContext context = new questionoverflowContext();

        // GET api/comment/{quiestionId}
        [HttpGet("{questionId}")]
        public ActionResult Get(int questionId)
        {
            var commentList = context.Comments.Where(c => c.QuestionId == questionId).ToList();

            if (commentList == null) return BadRequest();

            else return Ok(commentList);
        }

        // POST api/comment/{questionId}
        [HttpPost("{questionId}")]
        public ActionResult Post(int questionId, [FromBody]Comments comment)
        {

            MySql.Data.MySqlClient.MySqlConnection connection = null;
            string _connectionString;
            _connectionString = "server=localhost;database=questionoverflow;Uid =root; Pwd=admin;";
            try
            {
                connection = new MySql.Data.MySqlClient.MySqlConnection();
                connection.ConnectionString = _connectionString;
                connection.Open();
            }
            catch (MySql.Data.MySqlClient.MySqlException ex)
            {
                Console.WriteLine(ex.Message);
            }

            String sqlString = String.Format("insert into comments (questionId, userId, parentId, description, author, date) values ({0}, {1}, {2}, '{3}', '{4}', '{5}')",
                questionId, comment.UserId, comment.ParentId, comment.Description.Replace("\'", "\\'"), comment.Author, DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss"));

            MySql.Data.MySqlClient.MySqlCommand cmd = new MySql.Data.MySqlClient.MySqlCommand(sqlString, connection);
            try
            {
                cmd.ExecuteNonQuery();
            }
            catch (MySql.Data.MySqlClient.MySqlException sqlEx)
            {
                Console.WriteLine(sqlEx.Message);
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
            }


            long id = cmd.LastInsertedId;

            if (id > 0) return Ok("Comment posted id: " + id);
            else return BadRequest("Comment not posted");
        }

        // PUT api/comment/{questionId}
        [HttpPut("{questionId}")]
        public ActionResult Put(int id, [FromBody]Answers value)
        {
            return NotFound();
        }
    }
}
