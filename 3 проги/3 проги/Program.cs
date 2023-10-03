using System;

class Program
{
    static void Main()
    {
        while (true)
        {
            Console.WriteLine("Выберите программу:");
            Console.WriteLine("1. Игра 'Угадай число'");
            Console.WriteLine("2. Таблица умножения");
            Console.WriteLine("3. Вывод делителей числа");
            Console.WriteLine("4. Выход");

            int choice;

            if (!int.TryParse(Console.ReadLine(), out choice))
            {
                Console.WriteLine("Некорректный ввод. Пожалуйста, введите число.");
                continue;
            }

            switch (choice)
            {
                case 1:
                    PlayGuessNumberGame();
                    break;
                case 2:
                    PrintMultiplicationTable();
                    break;
                case 3:
                    PrintDivisors();
                    break;
                case 4:
                    Console.WriteLine("Выход из программы.");
                    return;
                default:
                    Console.WriteLine("Некорректный выбор. Пожалуйста, выберите существующий пункт меню.");
                    break;
            }
        }
    }

    static void PlayGuessNumberGame()
    {
        Random random = new Random();
        int randomNumber = random.Next(1, 101);
        int attempts = 0;
        int guess;

        Console.WriteLine("Добро пожаловать в игру 'Угадай число'!");
        Console.WriteLine("Я загадал число от 1 до 100. Попробуйте угадать.");

        do
        {
            attempts++;
            Console.Write("Введите вашу догадку: ");

            if (!int.TryParse(Console.ReadLine(), out guess))
            {
                Console.WriteLine("Пожалуйста, введите корректное число.");
                continue;
            }

            if (guess < randomNumber)
            {
                Console.WriteLine("Загаданное число больше.");
            }
            else if (guess > randomNumber)
            {
                Console.WriteLine("Загаданное число меньше.");
            }
        } while (guess != randomNumber);

        Console.WriteLine($"Поздравляю, вы угадали число {randomNumber} за {attempts} попыток!");
    }

    static void PrintMultiplicationTable()
    {
        int s = 10;
        int u = 10;
        int[,] multiplicationTable = new int[s, u];

        Console.WriteLine("Таблица умножения:");

        for (int i = 1; i <= s; i++)
        {
            for (int j = 1; j <= u; j++)
            {
                multiplicationTable[i - 1, j - 1] = i * j;
                Console.Write($"{multiplicationTable[i - 1, j - 1],3} ");
            }
            Console.WriteLine();
        }
    }

    static void PrintDivisors()
    {
        Console.Write("Введите число: ");
        int number;

        if (!int.TryParse(Console.ReadLine(), out number))
        {
            Console.WriteLine("Пожалуйста, введите корректное число.");
            return;
        }

        Console.Write($"Делители числа {number}: ");

        for (int i = 1; i <= number; i++)
        {
            if (number % i == 0)
            {
                Console.Write(i + " ");
            }
        }
    }
}
