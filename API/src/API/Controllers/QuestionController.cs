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

        // GET api/questions/1
        [HttpGet("{questionId}")]
        public ActionResult Get(int questionId)
        {
            Questions question = context.Questions.FirstOrDefault(q => q.QuestionId == questionId);

            if (question == null) return NotFound();
            else return Ok(question);
        }

        // GET api/question/top/1
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

        // POST api/question
        [HttpPost]
        public ActionResult Post([FromBody]Questions question)
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

            String sqlString = String.Format("insert into questions (userId, author, title, description, anonymous) values ({0}, '{1}', '{2}', '{3}', {4})",
                question.UserId, question.Author, question.Title, question.Description.Replace("\'", "\\'"), question.Anonymous);

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

            return Ok(id);
        }

        // PUT api/question/5
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

        // DELETE api/question/5
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
