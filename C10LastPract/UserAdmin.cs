using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using static Pract10.Admin;

namespace Pract10
{
    internal class Admin : User
    {
        ModelOfWorker admin = new ModelOfWorker();
        List<UserTable> allUsers = new List<UserTable>();
        internal enum Post
        {
            F1 = ConsoleKey.F1,
            F2 = ConsoleKey.F2,
            F3 = ConsoleKey.F3,
            F4 = ConsoleKey.F4,
            Enter = ConsoleKey.Enter,
            UpArrow = ConsoleKey.UpArrow,
            DownArrow = ConsoleKey.DownArrow,
        }
        public Admin(ModelOfWorker worker, List<UserTable> allUsers)
        {
            admin = worker;
            this.allUsers = allUsers;
        }
        public void Interface()
        {
            int pos = 2;
            int max = allUsers.Count() + 1;
            while (true)
            {
                Console.Clear();
                Console.WriteLine(pos);
                Console.SetCursorPosition(0, pos);
                Console.WriteLine("->");

                InterfaceForUsers.PrintInterface(admin);

                ConsoleKeyInfo key = Console.ReadKey();
                if (key.Key == (ConsoleKey)Post.F1)
                {
                    Console.Clear();
                    Create();
                }
                else if (key.Key == (ConsoleKey)Post.F2)
                {
                    Console.Clear();
                    Search();
                }
                else if (key.Key == (ConsoleKey)Post.F3)
                {
                    Console.Clear();
                    Delete();
                }
                else if (key.Key == (ConsoleKey)Post.F4)
                {
                    Console.Clear();
                    Read(pos);
                }
                else if (key.Key == (ConsoleKey)Post.UpArrow)
                {
                    if (pos <= 2)
                    {
                        pos += max - 2;
                    }
                    else
                    {
                        pos--;
                    }
                }
                else if (key.Key == (ConsoleKey)Post.DownArrow)
                {
                    if (pos >= max)
                    {
                        pos -= max - 2 ;
                    }
                    else
                    {
                        pos++;
                    }
                }
                else if (key.Key == (ConsoleKey)Post.Enter)
                {
                    Console.Clear();
                    UserTable user = allUsers[pos - 2];
                    Update(user.id);
                }
            }
        }
        public void Search()
        {
            int id; string login; string password; int role;
            Console.WriteLine("Введите ID");
            id = Convert.ToInt32(Console.ReadLine());
            Console.WriteLine("Введите логин");
            login = Console.ReadLine();
            Console.WriteLine("Пароль");
            password = Console.ReadLine();
            Console.WriteLine("Роль");
            role = Convert.ToInt32(Console.ReadLine());

            UserTable user = new UserTable();
            user.id = id;
            user.login = login;
            user.password = password;
            user.role = role;

            if (allUsers.Contains(user))
            {
                Console.Clear();
                Console.WriteLine(user.id);
                Console.WriteLine(user.login);
                Console.WriteLine(user.role);
                Console.WriteLine(user.password);
                Console.WriteLine();

                Console.WriteLine("Нажмите любую кнопку для выхода");
                Console.ReadKey();
            }
            else
            {
                Console.WriteLine("Такого пользователя нет, нажмите любую клавишу, чтобы выйти");
                Console.ReadKey();
                Console.Clear();
            }
        }
        public void Create()
        {
            string Path; int len; string json; int role; int id; string filename;
            
            Path = Directory.GetCurrentDirectory();
            len = Path.Length - 17;
            json = Path.Substring(0, len) + "\\Tables.json";
            List<UserTable> con = Converter.Des<List<UserTable>>(json);

            Console.WriteLine("Введи логин");
            string login = Console.ReadLine();
            Console.WriteLine("Введи пароль");
            string password = Console.ReadLine();
            Console.WriteLine("Введи роль");
            role = Convert.ToInt32(Console.ReadLine());

            id = con[con.Count - 1].id + 1;

            UserTable newUser = new UserTable();
            newUser.id = id;
            newUser.login = login;
            newUser.password = password;
            newUser.role = role;

            con.Add(newUser);

            Console.WriteLine("Введите название файла");
            filename = Console.ReadLine();
            Converter.Ser<List<UserTable>>(con, filename);
        }

        public void Delete()
        {
            string startupPath; int len; string json; int id; string filename;
            
            startupPath = Directory.GetCurrentDirectory();
            len = startupPath.Length - 17;
            json = startupPath.Substring(0, len) + "\\Tables.json";
            List<UserTable> con = Converter.Des<List<UserTable>>(json);
            List<int> ids = new List<int>();

            foreach (UserTable user in con)
            {
                ids.Add(user.id);
            }
            
            Console.WriteLine("Введи ID пользователя которого хочешь удалить");
            id = Convert.ToInt32(Console.ReadLine());

            if (ids.Contains(id))
            {
                UserTable user = con[ids.IndexOf(id)];
                con.Remove(user);

                Console.WriteLine("Введи название файла");
                filename = Console.ReadLine();
                Converter.Ser<List<UserTable>>(con, filename);
            }
            else
            {
                Console.WriteLine("Такого пользователя нет, нажмите любую клавишу, чтобы выйти");
                Console.ReadKey();
                Console.Clear();
            }
        }

        public void Read(int id)
        {
            List<int> ids = new List<int>();

            foreach (UserTable i in allUsers)
            {
                ids.Add(i.id);
            }
            UserTable user = allUsers[ids.IndexOf((id))];
            Console.WriteLine(user.id);
            Console.WriteLine(user.login);
            Console.WriteLine(user.role);
            Console.WriteLine(user.password);
            Console.WriteLine();

            Console.WriteLine("Нажмите на любую кнопку что бы выйти");
            Console.ReadKey();
        }
        public void Update(int userUpdate)
        {
            string login; string password; int role; string filename;
            
            List<int> ids = new List<int>();

            foreach (UserTable i in allUsers)
            {
                ids.Add(i.id);
            }

            Console.WriteLine("Введи новый логин");
            login = Console.ReadLine();
            Console.WriteLine("Введи новый пароль для пользователя");
            password = Console.ReadLine();
            Console.WriteLine("Введи новую роль для пользователя");
            role = Convert.ToInt32(Console.ReadLine());

            UserTable user = allUsers[ids.IndexOf((userUpdate))];
            allUsers.Remove(user);
            user.login = login;
            user.password = password;
            user.role = role;

            allUsers.Add(user);
            Console.WriteLine("Введи имя файла");
            filename = Console.ReadLine();
            Converter.Ser<List<UserTable>>(allUsers, filename);
              
        }
    }
}
