using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using API.Models;
using System.Data.SqlClient;
using MySql.Data.MySqlClient;
using System.Collections;

namespace API.Helpers
{
    public class QuestionHelper
    {

        private MySqlConnection connection;

        public QuestionHelper()
        {
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
        }

        public Questions getQuestion(int questionId)
        {
            Questions question = new Questions();
            MySqlDataReader mySqlReader;

            String sqlString = "select * from questions where questionId = " + questionId.ToString();
            MySqlCommand cmd = new MySqlCommand(sqlString, connection);

            mySqlReader = cmd.ExecuteReader();

            if (mySqlReader.Read())
            {
                question.QuestionId = mySqlReader.GetInt32(0);
                question.UserId = mySqlReader.GetInt32(1);
                question.Title = mySqlReader.GetString(2);
                question.Description = mySqlReader.GetString(3);
                question.Comments = mySqlReader.GetInt32(4);
                question.Rating = mySqlReader.GetInt32(5);
                question.Date = mySqlReader.GetDateTime(6);
                question.Anonymous = mySqlReader.GetInt16(7);

                return question;
            }
            else return null;
        }

        public long newQuestion(Questions question)
        {
            String sqlString = String.Format("insert into questions (userId, title, description, date, anonymous) values ({0}, '{1}', '{2}', '{3}', {4})",
                question.UserId, question.Title, question.Description, DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss"), question.Anonymous);

            MySqlCommand cmd = new MySqlCommand(sqlString, connection);
            try
            {
                cmd.ExecuteNonQuery();
            }
            catch (SqlException sqlEx)
            {
                Console.WriteLine(sqlEx.Message);
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
            }


            long id = cmd.LastInsertedId;

            return id;
        }

        public ArrayList getAll(int page, string order)
        {
            ArrayList questionList = new ArrayList();

           
            MySqlDataReader mySqlReader;

            String sqlString = String.Format("select * from questions order by {1} desc limit {0}, 10", (page-1)*10, order);
            MySqlCommand cmd = new MySqlCommand(sqlString, connection);

            mySqlReader = cmd.ExecuteReader();

            while (mySqlReader.Read())
            {
                Questions question = new Questions();

                question.QuestionId = mySqlReader.GetInt32(0);
                question.UserId = mySqlReader.GetInt32(1);
                question.Title = mySqlReader.GetString(2);
                question.Description = mySqlReader.GetString(3);
                question.Comments = mySqlReader.GetInt32(4);
                question.Rating = mySqlReader.GetInt32(5);
                question.Date = mySqlReader.GetDateTime(6);
                question.Anonymous = mySqlReader.GetInt16(7);

                questionList.Add(question);
            }

            return questionList;
        }

        public bool updateQuestion(int id, Questions question)
        {
            MySqlDataReader mySqlReader;

            String sqlString = "select * from questions where questionId = " + id.ToString();
            MySqlCommand cmd = new MySqlCommand(sqlString, connection);

            mySqlReader = cmd.ExecuteReader();

            Questions update = new Questions();
            if (mySqlReader.Read())
            {
                update.Title = question.Title != null ? question.Title : mySqlReader.GetString(2);
                update.Description = question.Description != null ? question.Description : mySqlReader.GetString(3);
                update.Comments = question.Comments != 0 ? mySqlReader.GetInt32(4) + question.Comments : mySqlReader.GetInt32(4);
                update.Rating = question.Rating != 0 ? mySqlReader.GetInt32(5) + question.Rating : mySqlReader.GetInt32(5);
                update.Anonymous = question.Anonymous != 0 ? question.Anonymous : mySqlReader.GetInt32(7);

                mySqlReader.Close();

                sqlString = String.Format("update questions set title='{0}', description='{1}', comments={2}, rating={3}, anonymous={4} where questionId = {5}",
                    update.Title, update.Description, update.Comments, update.Rating, update.Anonymous, id);

                cmd = new MySqlCommand(sqlString, connection);
                cmd.ExecuteNonQuery();
                return true;

            }
            else return false;
        }

    }
}
