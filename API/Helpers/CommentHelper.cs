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
    public class CommentHelper
    {

        private MySqlConnection connection;

        public CommentHelper()
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

        public ArrayList getAll(int questionId)
        {
            ArrayList answerList = new ArrayList();

           
            MySqlDataReader mySqlReader;

            String sqlString = String.Format("select * from comments where questionId = {0}", questionId);
            MySqlCommand cmd = new MySqlCommand(sqlString, connection);

            mySqlReader = cmd.ExecuteReader();

            while (mySqlReader.Read())
            {
                Comments comment = new Comments();
                comment.CommentId = mySqlReader.GetInt32(0);
                comment.QuestionId = mySqlReader.GetInt32(1);
                comment.UserId = mySqlReader.GetInt32(2);
                comment.ParentId = mySqlReader.GetInt32(3);
                comment.Description = mySqlReader.GetString(4);
                comment.Author = mySqlReader.GetString(5);
                comment.Date = mySqlReader.GetDateTime(6);

                answerList.Add(comment);
            }

            mySqlReader.Close();

            return answerList;
        }

        public long newComment(int questionId, Comments comment)
        {
            String sqlString = String.Format("insert into comments (questionId, userId, parentId, description, author, date) values ({0}, {1}, {2}, '{3}', '{4}', '{5}')",
                questionId, comment.UserId, comment.ParentId, comment.Description.Replace("\'", "\\'"), comment.Author, DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss"));

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

        public bool updateComment(int questionId, Comments answer)
        {
            /*
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
            else
            */return false;
        }

    }
}
