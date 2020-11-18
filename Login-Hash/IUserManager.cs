using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Login_Hash
{
    interface IUserManager
    {
        /// <summary>
        /// Inserts the user into the DB
        /// </summary>
        /// <param name="user"></param>
        void InsertUserToDB(User user);

        /// <summary>
        /// Verifies the user 
        /// </summary>
        /// <param name="username"></param>
        /// <param name="password"></param>
        /// <returns></returns>
        User VerifyUser(string username, string password);
    }
}
