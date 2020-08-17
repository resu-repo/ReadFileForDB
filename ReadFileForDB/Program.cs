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
    class User
    {
        string firtsName;
        string lastName;
        public string mail;
        string password;
        string phonenumber;
        string line;

        public User(Queue<string> testUser)
        {
            //testUser.Dequeue();
            line = testUser.Dequeue();
            ReadLine();
        }

        public void ReadLine()                                  //Обработка строки
        {
            Queue<string> param = new Queue<string>();
            foreach (char i in line)
            {
                while (i != 44 && i != 41)
                {
                    firtsName += i;
                    break;
                }
                if (i == 44)
                {
                    param.Enqueue(firtsName);
                    firtsName = "";
                }
                if (i == 41)
                {
                    param.Enqueue(firtsName);
                    firtsName = "";
                }

            }
            PrintInfo(param);
        }
        public void PrintInfo(Queue<string> param)              //Передеча параметров
        {
            firtsName = param.Dequeue();
            lastName = param.Dequeue();
            mail = param.Dequeue();
            password = param.Dequeue();
            phonenumber = param.Dequeue();

            InserToDb();
        }
        void InserToDb()                                        //Интерация с бд
        {


            string connectionString = "Database= vk;Datasource=localhost;User=root;Password=ghjcnjgfhjkm";
            MySqlConnection connection = new MySqlConnection(connectionString);
            connection.Open();

            MySqlCommand insertToInfo = connection.CreateCommand();
            insertToInfo.CommandText = "insert into info values (NULL," + firtsName + "," + lastName + "," + mail + "," + password + ",\'" + phonenumber + "\')";


            int countRows = insertToInfo.ExecuteNonQuery();
            Console.WriteLine("InsertToUsers. Row Count affected = {0}", countRows);

            connection.Close();
        }
    }

}
