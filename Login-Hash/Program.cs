using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.IO;

namespace Login_Hash
{
    class Program
    {
        static void Main(string[] args)
        {

            while (true)
            {
                Console.Clear();
                Console.WriteLine("Welcome");
                Console.WriteLine("Press '1' to create a new user");
                Console.WriteLine("Press '2' to login");
                string input = Console.ReadLine();
                if (input == "1")
                {
                    CreateUser();
                } else if (input == "2")
                {
                    Login();
                } else
                {
                    Console.WriteLine("Dumbass");
                    Console.ReadKey();
                }
            }

            

            

        }

        static void CreateUser()
        {
            Console.WriteLine("Welcome");
            Console.WriteLine("Create a new user");
            Console.WriteLine("Enter a username:");
            string username = Console.ReadLine();

            Console.WriteLine("Enter a password");
            string password = Console.ReadLine();


            HashManager hm = new HashManager();
            byte[] bytes = Encoding.ASCII.GetBytes(password);
            byte[] salt = hm.GenerateSalt();
            password = Convert.ToBase64String(hm.GenerateSHA256(bytes, salt));


            //Make a new user object
            User user = new User(username, password, Convert.ToBase64String(salt));

            UserManager um = new UserManager();
            um.InsertUserToDB(user);

            Console.WriteLine("The user has succesfully been created");

            Console.ReadKey();
        }

        static void Login()
        {
            Console.WriteLine("Welcome");
            Console.WriteLine("Please login");
            Console.WriteLine();
            Console.WriteLine();
            Console.WriteLine("Enter username");
            string username = Console.ReadLine();
            Console.WriteLine("Enter password");
            string password = Console.ReadLine();

            UserManager um = new UserManager();

            User user = um.VerifyUser(username, password);
            if (user != null)
            {
                Console.WriteLine("Welcome " + user.Username);
            }
            else
            {
                Console.WriteLine("Fuck off");
            }
            Console.ReadKey();
        }
    }
}
