using System;
using System.Diagnostics;
using System.Linq;

public static class TaskManager
{
    public static void ShowTaskList()
    {
        try
        {
            Process[] processes = Process.GetProcesses();

            Console.WriteLine("Список процессов:");
            for (int i = 0; i < processes.Length; i++)
            {
                Console.WriteLine($"{i + 1}. {processes[i].ProcessName} (PID: {processes[i].Id})");
            }

            Console.WriteLine("\nВыберите процесс (Enter для подробной информации, Esc для выхода):");
            ConsoleKeyInfo keyInfo;
            do
            {
                keyInfo = Console.ReadKey();
                if (keyInfo.Key == ConsoleKey.Enter)
                {
                    ShowProcessInfo(processes);
                }
            } while (keyInfo.Key != ConsoleKey.Escape);
        }
        catch (Exception ex)
        {
            Console.WriteLine($"Ошибка: {ex.Message}");
        }
    }

    private static void ShowProcessInfo(Process[] processes)
    {
        Console.Clear();
        Console.WriteLine("Подробная информация о процессе:");

        try
        {
            Console.WriteLine("Выберите действие (D - завершить процесс, Del - завершить все с таким именем, Backspace - назад):");
            var key = GetMenuSelection();

            if (key == MenuKey.D)
            {
                TerminateProcess(processes);
            }
            else if (key == MenuKey.Delete)
            {
                TerminateAllProcessesByName(processes);
            }
            else if (key == MenuKey.Backspace)
            {
                ShowTaskList();
            }
        }
        catch (Exception ex)
        {
            Console.WriteLine($"Ошибка: {ex.Message}");
            ShowTaskList();
        }
    }

    private static MenuKey GetMenuSelection()
    {
        while (true)
        {
            var keyInfo = Console.ReadKey();
            switch (keyInfo.Key)
            {
                case ConsoleKey.D:
                    return MenuKey.D;
                case ConsoleKey.Delete:
                    return MenuKey.Delete;
                case ConsoleKey.Backspace:
                    return MenuKey.Backspace;
            }
        }
    }

    private static void TerminateProcess(Process[] processes)
    {
        Console.Write("Введите номер процесса, который хотите завершить: ");
        if (int.TryParse(Console.ReadLine(), out int processNumber) && processNumber >= 1 && processNumber <= processes.Length)
        {
            try
            {
                processes[processNumber - 1].Kill();
                Console.WriteLine("Процесс завершен.");
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Ошибка при завершении процесса: {ex.Message}");
            }
        }
        else
        {
            Console.WriteLine("Некорректный номер процесса.");
        }
    }

    private static void TerminateAllProcessesByName(Process[] processes)
    {
        Console.Write("Введите имя процесса, который хотите завершить: ");
        string processName = Console.ReadLine();

        try
        {
            foreach (var process in processes.Where(p => p.ProcessName == processName))
            {
                process.Kill();
                Console.WriteLine($"Процесс {process.ProcessName} (PID: {process.Id}) завершен.");
            }
        }
        catch (Exception ex)
        {
            Console.WriteLine($"Ошибка при завершении процессов: {ex.Message}");
        }
    }

    private enum MenuKey
    {
        D = -1,
        Delete = -2,
        Backspace = -3
    }
}

class Program
{
    static void Main()
    {
        TaskManager.ShowTaskList();
    }
}
