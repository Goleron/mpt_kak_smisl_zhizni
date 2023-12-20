namespace Practise_Task__10__Informatin_System_
{
    internal class Authorization : User
    {
        private static int minPos = 2;
        private static int maxPos = 4;
        private static int currentPos = minPos;
        private static string login = "";
        private static string password = "";
        private static string role;
        private static string MainFolder = "\\DataBase";
        public static int number;


        public static List<User> users = new List<User>();
        public static List<User> mistakes = new List<User>();
        public static void Start()     
        {
            if (number > 1)
            {
                users.RemoveAt(users.Count() - 1);
            }
            number = 1;
            
            Console.SetCursorPosition((Console.WindowWidth / 2) - 20, 0);
            Console.WriteLine("Добро пожаловать в последний практос в 2023!");

            Console.WriteLine(new string('-', 120));

            Console.WriteLine("  Логин: ");
            Console.WriteLine("  Пароль: ");
            Console.WriteLine("  Авторизоваться");

            Arrows.Position(minPos, maxPos, currentPos, number, login, password, role, users);
        }

        public static void Input(int current, int number)
        {
            Console.CursorVisible = true;
            if (current == minPos)
            {
                Console.SetCursorPosition(9, current);
                Console.Write(new string(' ', Console.WindowHeight));
                Console.SetCursorPosition(9, current);
                login = Console.ReadLine();
            }
            if (current == maxPos - 1)
            {
                int width = 10;
                Console.SetCursorPosition(width, current);
                Console.Write(new string(' ', Console.WindowHeight));
                Console.SetCursorPosition(width, current);

                password = "";

                while (true)
                {
                    var key = Console.ReadKey(true);

                    if (key.Key == ConsoleKey.Backspace)
                    {
                        width--;
                        Console.SetCursorPosition(width, current);
                        Console.Write(" ");
                        password = password.Remove(password.Length - 1);
                        Console.SetCursorPosition(width, current);
                        continue;
                    }
                    if (key.Key == ConsoleKey.Enter)
                    {
                        break;
                    }
                    Console.Write("*");
                    password += key.KeyChar;
                    width++;

                }
            }
            if (current == maxPos)
            {
                string fileName;

                User adm = new User();

                adm.login = "admin";
                adm.password = "admin";
                if (login == adm.login && password == adm.password)
                {
                    role = Roles.Administrator.ToString();
                    fileName = "\\Список всех пользователей.json";
                    
                    adm.ID = (int)Roles.Administrator;
                    adm.role = Roles.Administrator.ToString();

                    users.Clear();
                    users.Add(adm);

                    Console.Clear();
                    JustConvert.Serialization(users, MainFolder, fileName);
                    
                    number++;
                    Admin.Something(users, login, number, role, password);
                }
                else
                {
                    fileName = "\\Ошибки входа.json";

                    User mistake = new User();

                    mistake.login = login;
                    mistake.password = password;
                    mistakes.Add(mistake);

                    Console.WriteLine("Некорректно были введены логин или пароль пользователя. Любая клавиша - повтор.");
                    Console.ReadKey();
                    JustConvert.Serialization(mistakes, MainFolder, fileName);
                    Console.Clear();
                    Start();
                }

            }
            Console.SetCursorPosition(0, current);
            Console.Write("  ");
            current++;
            Arrows.Position(minPos, maxPos, current, number, login, password, role, users);
        }
    }
}
