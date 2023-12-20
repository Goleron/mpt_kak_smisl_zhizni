using System.Collections.Generic;
using System.Data;

namespace Practise_Task__10__Informatin_System_
{
    public class Admin : User, ICRUD   
    {
        private int number;
        private string login, password, role;

        static User user = new User();
        public List<User> users = new List<User>();
        public static List<User> output_Users = new List<User>();

        static int new_ID = user.ID;
        static string new_login = user.login;
        static string new_password = user.password;
        static string new_role = user.role;

        static string MainFolder = "\\DataBase";
        static string fileName = "\\Список всех пользователей.json";
        public Admin(string login, string role, string password, int number, List<User> users)
        {
            this.login = login;
            this.password = password;
            this.role = role;
            this.number = number;
            this.users = users;


        }
        public static void Something(List<User> users, string login, int number, string role, string password)
        {
            output_Users = JustConvert.Deserealization(); 
            Console.SetCursorPosition(20, 0);
            Console.WriteLine($"Добро пожаловать, {login}!\t\t\t\t\t\tРоль: {role}");
            Console.WriteLine(new string('-', 120));
            Console.WriteLine("\tID\t\tLogin\t\tPassword\t\tRole");

            int count = 0;
            foreach (var item in output_Users)
            {
                Console.Write("\t" + item.ID);
                Console.Write("\t\t" + item.login);
                Console.Write("\t\t" + item.password);
                Console.Write("\t\t" + item.role + "\n");

                count++;
            }

            for (int i = 0; i < output_Users.Count + 3; i++)
            {
                Console.SetCursorPosition(90, i + 2);
                Console.Write("|");

                if (i == 0) Console.WriteLine("  F1 - Добавить запись");
                if (i == 1) Console.WriteLine("  F2 - Найти запись");
            }

            Arrows.Position(3, count + 2, 3, number, login, password, role, users);
        }

        public void Create()
        {
            Console.Clear();

            Console.SetCursorPosition(20, 0);
            Console.WriteLine($"Добро пожаловать, {login}!\t\t\t\t\t\tРоль: {role}");
            Console.WriteLine(new string('-', 120));

            Console.WriteLine("  ID: ");
            Console.WriteLine("  Логин: ");
            Console.WriteLine("  Пароль: ");
            Console.WriteLine("  Роль: ");

            for (int i = 0; i < 10; i++)
            {
                Console.SetCursorPosition(90, i + 2);
                Console.Write("|");

                if (i == 0) Console.WriteLine("  1 - Администратор");
                if (i == 1) Console.WriteLine("  2 - Кассир");
                if (i == 2) Console.WriteLine("  3 - Кадровик");
                if (i == 3) Console.WriteLine("  4 - Склад-менеджер");
                if (i == 4) Console.WriteLine("  5 - Бухгалтер");

                if (i == 6) Console.WriteLine("  S - Сохранить изменения");
                if (i == 7) Console.WriteLine("  Escape - Вернуться в меню");

            }

            Arrows.Position(2, 5, 2, number, login, password, role, users);
        }
        public static void Create_Advanced(int current, int number, List<User> users, string login, string password, string role)
        {
            Console.CursorVisible = true;

            if (current == 2)           
            {
                Console.SetCursorPosition(6, current);
                Console.Write(new string(' ', 50));
                Console.SetCursorPosition(6, current);
                try
                {
                    user.ID = Convert.ToInt32(Console.ReadLine());
                }
                catch (Exception)
                {
                    Console.SetCursorPosition(20, current);
                    Console.Write("ОШИБКА! ВВЕДИТЕ ID В ФОРМЕ INT");
                    Console.ReadKey(true);
                    Create_Advanced(current, number, users, login, password, role);
                }
            }
            if (current == 3)           
            {
                Console.SetCursorPosition(9, current);
                Console.Write(new string(' ', 50));
                Console.SetCursorPosition(9, current);
                user.login = Console.ReadLine();
            }
            if (current == 4)
            {
                Console.SetCursorPosition(10, current);
                Console.Write(new string(' ', 50));
                Console.SetCursorPosition(10, current);
                user.password = Console.ReadLine();
            }
            if (current == 5)
            {
                Console.SetCursorPosition(8, current);
                Console.Write(new string(' ', 50));
                Console.SetCursorPosition(8, current);

                try
                {
                    user.role = Console.ReadLine();

                    if (user.role != ((int)Roles.Administrator).ToString() && user.role != ((int)Roles.Kassir).ToString() && user.role != ((int)Roles.Kadrovik).ToString() && user.role != ((int)Roles.Sklad_Manager).ToString() && user.role != ((int)Roles.Boughalter).ToString())
                    {
                        string ex = "ex";
                        Console.WriteLine(Convert.ToInt32(ex));
                    }
                    else
                    {
                        if (user.role == ((int)Roles.Administrator).ToString()) user.role = "Administrator";
                        if (user.role == ((int)Roles.Kassir).ToString()) user.role = "Kassir";
                        if (user.role == ((int)Roles.Kadrovik).ToString()) user.role = "Kadrovik";
                        if (user.role == ((int)Roles.Sklad_Manager).ToString()) user.role = "Sklad_Manager";
                        if (user.role == ((int)Roles.Boughalter).ToString()) user.role = "Boughalter";
                    }
                }
                catch (Exception)
                {
                    Console.SetCursorPosition(20, current);
                    Console.Write("ОШИБКА! ВВЕДИТЕ РОЛЬ В ФОРМЕ INT");
                    Console.ReadKey(true);
                    Create_Advanced(current, number, users, login, password, role);
                }
            }

            Console.SetCursorPosition(0, current);
            Console.Write("  ");

            if (current == 5) current = 1;
            current++;

            Arrows.Position(2, 5, current, number, login, password, role, users);
        }
        public static void Save(List<User> users, string login, int number, string role, string password)
        {
            users.Add(user);
            Console.WriteLine(users);
            JustConvert.Serialization(users, MainFolder, fileName);
            Console.Clear();
            number = 2;
            Something(users, login, number, "Administrator", password);
        }

        public void Delete()
        {
            
        }

        public void Read()
        {
            
        }

        public void Update()
        {
            
        }
    }
}
