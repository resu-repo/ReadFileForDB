using MySql.Data.MySqlClient;
using System;
using System.Collections.Generic;
using System.Globalization;
using System.IO;
using System.Linq;
using System.Runtime.Serialization;
using System.Text;
using System.Threading.Tasks;

namespace ReadFileForDB
{
    class Program
    {
        static void Main(string[] args)
        {
            Console.Write("Input path: ");
            string path = Console.ReadLine();
            //string path = @"C:\Users\Resu\Downloads\Telegram Desktop\21.txt";
            ReadFile(path);
            //Console.WriteLine(test1.mail);



        }

        static Queue<string> ReadFile(string path)     //Счетчик строк
        {
            Queue<string> users = new Queue<string>();
            string line = null;
            int count = 0;
            try
            {
                using (StreamReader readrer = new StreamReader(path))
                {

                    while (readrer.Peek() != -1)
                    {
                        line = readrer.ReadLine();
                        users.Enqueue(line);
                        count++;
                    }

                }

            }
            catch (Exception e)
            {
                Console.WriteLine(e);
            }
            for (int i = 0; i < count; i++)
            {
                User user = new User(users);
            }

            return users;
        }
    }

}
