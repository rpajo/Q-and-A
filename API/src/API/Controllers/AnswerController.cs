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

        /// <summary>
        /// Get a list of answers based on order
        /// </summary>
        /// <param name="order"> date/-date/rating</param>
        /// <param name="questionId"></param>
        /// <returns></returns>
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

        /// <summary>
        /// Posts a new answer
        /// </summary>
        /// <param name="questionId"></param>
        /// <param name="answer"></param>
        /// <returns></returns>
        [HttpPost("{questionId}")]
        public ActionResult Post(int questionId, [FromBody]Answers answer)
        {
            MySql.Data.MySqlClient.MySqlConnection connection = null;
            string _connectionString;
            _connectionString = context.connectionString;
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

        String sqlString = String.Format("insert into answers(questionId, userId, description, rating, date, author) values ({0}, {1}, '{2}', '{3}', '{4}', '{5}')",
                questionId, answer.UserId, answer.Description.Replace("\'", "\\'"), answer.Rating != null ? answer.Rating : 0, DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss"), answer.Author);

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

            if (id < 0) return BadRequest("Answer not submited");
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
        public ActionResult Put(int id, [FromBody]Answers value)
        {
            Answers answer = context.Answers.FirstOrDefault(q => q.AnswerId == id);
            if (answer != null)
            {

                answer.Rating = answer.Rating + value.Rating;
                if (value.Solved == 1) answer.Solved = 1;
                context.SaveChanges();
                return Ok("Answer successfuly updated");
            }

            else return BadRequest("Answer not updated");
        }

        /// <summary>
        /// Deletes an answer
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        [HttpDelete("{id}")]
        public ActionResult Delete(int id)
        {
            Answers delAnswer = context.Answers.SingleOrDefault(a => a.AnswerId == id);
            if (delAnswer != null)
            {
                context.Answers.Remove(delAnswer);
                context.SaveChanges();
                return Ok("Answer deleted");
            }
            else return BadRequest("No such answer");
        }
    }
}
