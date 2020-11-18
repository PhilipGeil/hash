using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Login_Hash
{
    class UserManager : IUserManager
    {
        // String that holds the connection string to the database.
        static string cs = @"Data Source=ZBC-E-PHIL2643\SQLEXPRESS;Initial Catalog=EncryptTest;Integrated Security = True";
        public void InsertUserToDB(User user)
        {
            using (SqlConnection sql = new SqlConnection(cs))
            {
                sql.Open();
                SqlCommand cmd = new SqlCommand("INSERT INTO users VALUES (@username, @password, @salt)", sql);
                cmd.Parameters.Add(new SqlParameter("@username", user.Username));
                cmd.Parameters.Add(new SqlParameter("@password", user.Password));
                cmd.Parameters.Add(new SqlParameter("@salt", user.Salt));
                cmd.ExecuteNonQuery();

            }
        }
        public User VerifyUser(string username, string password)
        {
            using (SqlConnection sql = new SqlConnection(cs))
            {
                sql.Open();
                SqlCommand cmd = new SqlCommand("SELECT salt FROM users WHERE username = @username", sql);
                cmd.Parameters.Add(new SqlParameter("@username", username));
                SqlDataReader rdr = cmd.ExecuteReader();
                string s = "";
                while (rdr.Read())
                {
                    s = (string)rdr["salt"];
                }
                rdr.Close();

                HashManager hm = new HashManager();
                byte[] pass = Encoding.UTF8.GetBytes(password);
                byte[] salt = Convert.FromBase64String(s);
                string hash = Convert.ToBase64String(hm.GenerateSHA256(pass, salt));

                SqlCommand cmd2 = new SqlCommand("SELECT * FROM users WHERE username = @username AND password = @password", sql);
                cmd2.Parameters.Add(new SqlParameter("@username", username));
                cmd2.Parameters.Add(new SqlParameter("@password", hash));
                SqlDataReader rdr2 = cmd2.ExecuteReader();
                while (rdr2.Read())
                {
                    return new User(
                        (string)rdr2["username"],
                        (string)rdr2["password"],
                        (string)rdr2["salt"]
                        );
                }
                rdr2.Close();
                return null;
            }
        }
    }
}
