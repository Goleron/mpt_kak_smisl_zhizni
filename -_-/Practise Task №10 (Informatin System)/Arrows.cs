using System.Data;
using System.Diagnostics;

namespace Practise_Task__10__Informatin_System_
{
    internal class Arrows
    {
        int min, max, current, number;
        private static string login, password;
        public Arrows(int min, int max, int number)
        {
            this.min = min;
            this.max = max;
            this.number = number;
        }
        public static void Position(int min, int max, int current, int number, string login, string password, string role, List<User> users)
        {
            ConsoleKeyInfo Keypressed;

            Console.SetCursorPosition(0, current);
            Console.WriteLine(">>");
            do
            {
                Console.CursorVisible = false;
                Keypressed = Console.ReadKey(true);

                if (Keypressed.Key == ConsoleKey.DownArrow)
                {
                    Console.SetCursorPosition(0, current);
                    Console.WriteLine("  ");
                    
                    current++;

                    if (current > max)
                    {
                        current = min;
                    }

                }
                else if (Keypressed.Key == ConsoleKey.UpArrow)
                {
                    Console.SetCursorPosition(0, current);
                    Console.WriteLine("  ");
                    
                    current--;

                    if (current < min)
                    {
                        current = max;
                    }
                }
                else if (number == 2 && (Keypressed.Key == ConsoleKey.F1 || Keypressed.Key == ConsoleKey.F2))
                {
                    number = 4;
                    Admin buttons = new Admin(login, role, password, number, users);
                    buttons.Create();

                }
                
                else if (number == 4 && Keypressed.Key == ConsoleKey.S) Admin.Save(users, login, number, role, password);
                
                else if (Keypressed.Key == ConsoleKey.Escape)      
                {
                    Function_Buttons(Keypressed, number, users, login, role, password);
                    break;
                }

                Console.SetCursorPosition(0, current);
                Console.WriteLine(">>");

            } while (Keypressed.Key != ConsoleKey.Enter);

            if (number == 1) Authorization.Input(current, number);
            if (number == 2)
            {
                number++;
                Details.detailedInformation(users, current, login, role, number, password);
            }
            if (number == 3)     
            {
                Console.WriteLine(":0");
                Console.ReadLine();
            }
            if (number == 4) Admin.Create_Advanced(current, number, users, login, password, role);
        
        }
        public static void Function_Buttons(ConsoleKeyInfo Keypressed, int number, List<User> users, string login, string role, string password)
        {
            if (Keypressed.Key == ConsoleKey.Escape)
            {
                if (number == 2)
                {
                    Console.Clear();
                    Authorization.Start();
                }
                if (number == 3)
                {
                    Console.Clear();
                    number--;
                    Admin.Something(users, login, number, role, password);
                }
            }
        }
    }
}
