using System;

class Calculator
{
    static void Main()
    {
        bool exit = false;

        while (!exit)
        {
            Console.WriteLine("Выберите операцию:");
            Console.WriteLine("1. Умножение");
            Console.WriteLine("2. Деление");
            Console.WriteLine("3. Сложение");
            Console.WriteLine("4. Вычитание");
            Console.WriteLine("5. Факториал");
            Console.WriteLine("6. Возведение в степень");
            Console.WriteLine("7. Найти 1% числа");
            Console.WriteLine("8. Выход");

            string choice = Console.ReadLine();

            switch (choice)
            {
                case "1":
                    PerformOperation("умножения", (x, y) => x * y);
                    break;
                case "2":
                    PerformOperation("деления", (x, y) => y != 0 ? x / y : double.NaN);
                    break;
                case "3":
                    PerformOperation("сложения", (x, y) => x + y);
                    break;
                case "4":
                    PerformOperation("вычитания", (x, y) => x - y);
                    break;
                case "5":
                    Console.Write("Введите число для вычисления факториала: ");
                    int num = int.Parse(Console.ReadLine());
                    if (num < 0)
                    {
                        Console.WriteLine("Ошибка: факториал отрицательного числа не определен.");
                    }
                    else
                    {
                        long result = CalculateFactorial(num);
                        Console.WriteLine($"Факториал числа {num} равен: {result}");
                    }
                    break;
                case "6":
                    PerformOperation("возведения в степень", (x, y) => Math.Pow(x, y));
                    break;
                case "7":
                    PerformOperation("нахождения 1% числа", (x, y) => 0.01 * x);
                    break;
                case "8":
                    Console.WriteLine("Выход из программы.");
                    exit = true;
                    break;
                default:
                    Console.WriteLine("Ошибка: Неверный выбор операции. Пожалуйста, выберите снова.");
                    break;
            }
        }
    }

    static void PerformOperation(string operationName, Func<double, double, double> operation)
    {
        Console.Write("Введите первое число: ");
        double num1 = double.Parse(Console.ReadLine());
        Console.Write("Введите второе число: ");
        double num2 = double.Parse(Console.ReadLine());

        double result = operation(num1, num2);

        if (double.IsNaN(result))
        {
            Console.WriteLine($"Ошибка: деление на ноль при операции {operationName}.");
        }
        else
        {
            Console.WriteLine($"Результат {operationName}: {result}");
        }
    }

    static long CalculateFactorial(int num)
    {
        if (num == 0)
            return 1;
        else
            return num * CalculateFactorial(num - 1);
    }
}
