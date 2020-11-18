using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Login_Hash
{
    class User
    {
        public string Username { get; set; }
        public string Password { get; set; }
        public string Salt { get; set; }

        public User()
        {

        }
        public User(string username, string password, string salt)
        {
            Username = username;
            Password = password;
            Salt = salt;
        }
    }
}
