namespace Practise_Task__10__Informatin_System_
{
    internal class Details
    {
        public static void detailedInformation(List<User> users, int current, string login, string role, int number, string password)
        {
            Console.Clear();

            Console.SetCursorPosition(20, 0);
            Console.WriteLine($"Добро пожаловать, {login}!\t\t\t\t\t\tРоль: {role}");
            Console.WriteLine(new string('-', 120));


            foreach (var item in users)
            {
                if (item.ID == (int)Roles.Administrator & item.login == login & item.password == password & item.role == role)
                {
                    Console.Write("  ID: ");
                    Console.WriteLine(item.ID);
                    Console.Write("  Логин: ");
                    Console.WriteLine(item.login);
                    Console.Write("  Пароль: ");
                    Console.WriteLine(item.password);
                    Console.Write("  Роль: ");
                    Console.WriteLine(item.role);
                }
            }
            
            for (int i = 0; i < 10; i++)
            {
                Console.SetCursorPosition(90, i + 2);
                Console.Write("|");

                if (i == 0) Console.WriteLine("  F10 - Изменить пункт");
                if (i == 1) Console.WriteLine("  Del - Удалить запись");
                if (i == 2) Console.WriteLine("  Escape - вернуться в меню");
            }

            Arrows.Position(2, 5, 2, number, login, password, role, users);

        }
    }
}
