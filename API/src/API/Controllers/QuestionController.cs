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
    public class QuestionController : ApiController
    {
        questionoverflowContext context = new questionoverflowContext();

        /// <summary>
        /// Get question details
        /// </summary>
        /// <param name="questionId"></param>
        /// <returns></returns>
        [HttpGet("{questionId}")]
        public ActionResult Get(int questionId)
        {
            Questions question = context.Questions.FirstOrDefault(q => q.QuestionId == questionId);

            if (question == null) return NotFound();
            else return Ok(question);
        }

        /// <summary>
        /// Get a list of question based on order and page
        /// </summary>
        /// <param name="page"></param>
        /// <param name="order">date/rating/answers</param>
        /// <returns></returns>
        [HttpGet("{order}/{page}")]
        public ActionResult Get(int page, String order)
        {

            var questionList = new List<Questions>();
            if(order == "date")
            {
                questionList = context.Questions.OrderByDescending(q => q.Date).Skip((page - 1) * 5).Take(5).ToList();
            }
            else if (order == "rating")
            {
                questionList = context.Questions.OrderByDescending(q => q.Rating).Skip((page - 1) * 5).Take(5).ToList();
            }
            else if (order == "answers")
            {
                questionList = context.Questions.OrderByDescending(q => q.Answers).Skip((page - 1) * 5).Take(5).ToList();
            }

            
            return Ok(questionList);
        }

        /// <summary>
        ///  Post a new question
        /// </summary>
        /// <param name="question"></param>
        /// <response code="400">Bad request</response>
        /// <returns></returns>
        [HttpPost]
        public ActionResult Post([FromBody]Questions question)
        {
            MySql.Data.MySqlClient.MySqlConnection connection = null;
            string _connectionString;
            _connectionString = "server=localhost;database=questionoverflow;Uid =root; Pwd=admin;";
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

            String sqlString = String.Format("insert into questions (userId, author, title, description, anonymous, date) values ({0}, '{1}', '{2}', '{3}', {4}, '{5}')",
                question.UserId, question.Author, question.Title, question.Description.Replace("\'", "\\'"), question.Anonymous, DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss"));

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

            if (id <= 0) return BadRequest();
            else return Ok(id);
        }

        /// <summary>
        /// Update a question
        /// </summary>
        /// <param name="id"></param>
        /// <param name="value"></param>
        /// <response code="400">Bad request</response>
        /// <returns></returns>
        [HttpPut("{id}")]
        public ActionResult Put(int id, [FromBody]Questions value)
        {
            Questions question = context.Questions.FirstOrDefault(q => q.QuestionId == id);
            if (question != null)
            {
                question.Rating = question.Rating + value.Rating;
                context.SaveChanges();
                return Ok("Question successfuly updated");
            }

            else return BadRequest("Question not updated");
        }

        /// <summary>
        /// Delete a question
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        /// <response code="400">Bad request</response>
        [HttpDelete("{id}")]
        public ActionResult Delete(int id)
        {
            Questions delQuestion = context.Questions.SingleOrDefault(a => a.QuestionId == id);
            if (delQuestion != null)
            {
                context.Questions.Remove(delQuestion);
                context.SaveChanges();
                return Ok("Question deleted");
            }
            else return BadRequest("No such question");
        }

    }
}
