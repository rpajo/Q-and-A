using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using MySql.Data;
using API.Models;
using System.Data.SqlClient;
using MySql.Data.MySqlClient;

namespace API.Helpers
{
    public class UsersHelper
    {
        private MySql.Data.MySqlClient.MySqlConnection connection;

        public UsersHelper()
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

        public Users getUser(int id)
        {
            Users user = new Users();
            MySqlDataReader mySqlReader;

            String sqlString = "select * from users where userId = " + id.ToString();
            MySqlCommand cmd = new MySqlCommand(sqlString, connection);

            mySqlReader = cmd.ExecuteReader();

            if (mySqlReader.Read())
            {
                user.UserId = mySqlReader.GetInt32(0);
                user.Username = mySqlReader.GetString(1);
                user.Email = mySqlReader.GetString(2);
                user.Password = mySqlReader.GetString(3);
                user.Description = mySqlReader.GetString(4);
                user.MemberSince = mySqlReader.GetDateTime(5);
                user.Location = mySqlReader.GetString(6);
                user.Answers = mySqlReader.GetInt32(7);
                user.Questions = mySqlReader.GetInt32(8);
                user.Reputation = mySqlReader.GetInt32(9);

                return user;
            }
            else return null;
        }

        public int login(Users credentials)
        {
            Users user = new Users();
            MySqlDataReader mySqlReader;

            String sqlString = String.Format("select * from users where email = '{0}'", credentials.Email);
            MySqlCommand cmd = new MySqlCommand(sqlString, connection);

            mySqlReader = cmd.ExecuteReader();

            if (mySqlReader.Read())
            {
                user.UserId = mySqlReader.GetInt32(0);
                user.Username = mySqlReader.GetString(1);
                user.Email = mySqlReader.GetString(2);
                user.Password = mySqlReader.GetString(3);


                if (credentials.Password == user.Password) return 0;
                else return -1;
            }
            else return 404;
        }

        public bool updateUser(int id, Users user)
        {
            MySqlDataReader mySqlReader;

            String sqlString = "select * from users where userId = " + id.ToString();
            MySqlCommand cmd = new MySqlCommand(sqlString, connection);

            mySqlReader = cmd.ExecuteReader();

            Users update = new Users();
            if (mySqlReader.Read())
            {
                update.Description =  user.Description != null ? user.Description  : mySqlReader.GetString(4);
                update.Location = user.Location != null ? user.Location : mySqlReader.GetString(6);
                update.Answers = user.Answers != null ? mySqlReader.GetInt32(7) + user.Answers : mySqlReader.GetInt32(7);
                update.Questions = user.Questions != null ? mySqlReader.GetInt32(8) + user.Questions : mySqlReader.GetInt32(8);
                update.Reputation = user.Reputation != null ? mySqlReader.GetInt32(9) + user.Reputation : mySqlReader.GetInt32(9);

                mySqlReader.Close();

                sqlString = String.Format("update users set description='{0}', location='{1}', answers={2}, questions={3}, reputation={4} where userId = {5}",
                    update.Description, update.Location, update.Answers, update.Questions, update.Reputation, id);

                cmd = new MySqlCommand(sqlString, connection);
                cmd.ExecuteNonQuery();
                return true;

            }
            else return false;
        }

        public long savePerson(Users newUser)
        {
            String sqlString = String.Format("insert into users (username, email, password, description, memberSince, location) values('{0}', '{1}', '{2}', '{3}', '{4}', '{5}')",
                newUser.Username, newUser.Email, newUser.Password, newUser.Description, DateTime.Now.ToString("yyyy-MM-dd"), newUser.Location);
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
    }
}
