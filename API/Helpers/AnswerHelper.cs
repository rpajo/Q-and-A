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
    public class AnswerHelper
    {

        private MySqlConnection connection;

        public AnswerHelper()
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

        public ArrayList getAnswers(int questionId, string order)
        {
            ArrayList answerList = new ArrayList();

           
            MySqlDataReader mySqlReader;

            String sqlString = String.Format("select * from answers where questionId = {0} order by {1} desc", questionId, order);
            MySqlCommand cmd = new MySqlCommand(sqlString, connection);

            mySqlReader = cmd.ExecuteReader();

            while (mySqlReader.Read())
            {
                Answers answer = new Answers();

                answer.AnswerId = mySqlReader.GetInt32(0);
                answer.Description = mySqlReader.GetString(2);
                answer.Rating = mySqlReader.GetInt32(3);
                answer.Date = mySqlReader.GetDateTime(4);

                answerList.Add(answer);
            }

            return answerList;
        }

        public long newAnswer(int questionId, Answers answer)
        {
            String sqlString = String.Format("insert into answers(questionId, description, rating, date) values ({0}, '{1}', {2}, '{3}')",
                questionId, answer.Description.Replace("\'", "\\'"), answer.Rating != null ? answer.Rating : 0, DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss"));

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

        public bool updateAnswer(int id, Answers answer)
        {
            MySqlDataReader mySqlReader;

            String sqlString = "select * from answers where answerId = " + id.ToString();
            MySqlCommand cmd = new MySqlCommand(sqlString, connection);

            mySqlReader = cmd.ExecuteReader();

            Answers update = new Answers();
            if (mySqlReader.Read())
            {
                update.Description = answer.Description != null ? answer.Description : mySqlReader.GetString(2);
                update.Rating = answer.Rating != 0 ? mySqlReader.GetInt32(3) + answer.Rating : mySqlReader.GetInt32(3);

                mySqlReader.Close();

                sqlString = String.Format("update answers set description='{0}', rating={1} where answerId = {2}",
                    update.Description.Replace("\'", "\\'"), update.Rating, id);

                cmd = new MySqlCommand(sqlString, connection);
                cmd.ExecuteNonQuery();
                return true;

            }
            else return false;
        }

    }
}
