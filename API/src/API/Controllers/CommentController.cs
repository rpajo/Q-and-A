using System;
using System.Linq;
using Microsoft.AspNetCore.Mvc;
using API.Models;
using System.Web.Http;
using Microsoft.AspNetCore.Cors;

namespace API.Controllers
{

    [Route("api/comment")]
    [EnableCors("AllowAll")]
    public class CommentControlLer : ApiController
    {
        questionoverflowContext context = new questionoverflowContext();

        /// <summary>
        /// Get a list of comment to a question thread
        /// </summary>
        /// <param name="questionId"></param>
        /// <response code="400">Bad request</response>
        /// <returns></returns>
        [HttpGet("{questionId}")]
        public ActionResult Get(int questionId)
        {
            var commentList = context.Comments.Where(c => c.QuestionId == questionId).ToList();

            if (commentList == null) return BadRequest();

            else return Ok(commentList);
        }

        /// <summary>
        /// Posts a new comment
        /// </summary>
        /// <param name="questionId"></param>
        /// <param name="comment"></param>
        /// <response code="400">Bad request</response>
        /// <returns></returns>
        [HttpPost("{questionId}")]
        public ActionResult Post(int questionId, [FromBody]Comments comment)
        {

            MySql.Data.MySqlClient.MySqlConnection connection = null;
            string _connectionString;
            _connectionString = context.connectionString;
            //_connectionString = "server=questionoverflow.ckztk2rxcoxz.us-west-2.rds.amazonaws.com;Uid =admin; Pwd=iamroot1;";
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

            if (questionId <= 0 || comment.UserId <= 0 || comment.ParentId <= 0 ||
                comment.Description.Length <= 0 || comment.Author.Length <= 0)
                return BadRequest("Request not valid");

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

            if (id > 0) return Ok(id);
            else return BadRequest("Comment not posted");
        }


        /// <summary>
        /// Deletes a comment with it's id
        /// </summary>
        /// <param name="id"></param>
        /// <response code="400">Bad request</response>
        /// <returns></returns>
        [HttpDelete("{id}")]
        public ActionResult Delete(int id)
        {
            Comments delComment = context.Comments.SingleOrDefault(c => c.CommentId == id );
            if (delComment != null)
            {
                context.Comments.Remove(delComment);
                context.SaveChanges();
                return Ok("Comment deleted");
            }
            else return BadRequest("No such comment");
        }
    }
}
