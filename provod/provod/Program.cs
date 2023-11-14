using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Linq;

public static class FileManager
{
    public static void ShowAvailableDrives()
    {
        DriveInfo[] drives = DriveInfo.GetDrives();
        Console.WriteLine("Available Drives:");
        foreach (DriveInfo drive in drives)
        {
            if (drive.IsReady)
            {
                Console.WriteLine($"{drive.Name} - {drive.TotalSize / 1024 / 1024 / 1024}GB total, {drive.TotalFreeSpace / 1024 / 1024 / 1024}GB free");
            }
        }
    }

    public static void ShowDirectoryContents(string path)
    {
        var dirInfo = new DirectoryInfo(path);
        foreach (var dir in dirInfo.GetDirectories())
        {
            Console.WriteLine($"[DIR] {dir.Name} - Created: {dir.CreationTime}");
        }
        foreach (var file in dirInfo.GetFiles())
        {
            Console.WriteLine($"[FILE] {file.Name} - Size: {file.Length / 1024}KB - Created: {file.CreationTime}");
        }
    }

    public static string ChooseDriveOrDirectory(List<string> options)
    {
        int index = 0;
        ConsoleKey key;
        do
        {
            Console.Clear();
            for (int i = 0; i < options.Count; i++)
            {
                Console.WriteLine(i == index ? $"> {options[i]}" : $"  {options[i]}");
            }
            key = Console.ReadKey().Key;
            if (key == ConsoleKey.UpArrow && index > 0) index--;
            else if (key == ConsoleKey.DownArrow && index < options.Count - 1) index++;
        } while (key != ConsoleKey.Enter);

        return options[index];
    }

    public static void OpenFile(string filePath)
    {
        Process.Start(new ProcessStartInfo(filePath) { UseShellExecute = true });
    }

    public static string GetParentDirectory(string path)
    {
        return Directory.GetParent(path)?.FullName;
    }
}

class Program
{
    static void Main()
    {
        string currentPath = string.Empty;
        while (true)
        {
            Console.Clear();
            if (string.IsNullOrEmpty(currentPath))
            {
                FileManager.ShowAvailableDrives();
                var drives = DriveInfo.GetDrives()
                                      .Where(d => d.IsReady)
                                      .Select(d => d.Name).ToList();
                currentPath = FileManager.ChooseDriveOrDirectory(drives);
            }
            else
            {
                FileManager.ShowDirectoryContents(currentPath);
                var dirInfo = new DirectoryInfo(currentPath);
                var directories = dirInfo.GetDirectories().Select(d => d.FullName).ToList();
                var files = dirInfo.GetFiles().Select(f => f.FullName).ToList();
                var options = new List<string> { ".." };
                options.AddRange(directories);
                options.AddRange(files);
                var choice = FileManager.ChooseDriveOrDirectory(options);
                if (choice == "..")
                {
                    currentPath = FileManager.GetParentDirectory(currentPath) ?? string.Empty;
                }
                else if (Directory.Exists(choice))
                {
                    currentPath = choice;
                }
                else if (File.Exists(choice))
                {
                    FileManager.OpenFile(choice);
                }
            }

            Console.WriteLine("нажми esc чтобы продолжить...");
            if (Console.ReadKey().Key == ConsoleKey.Escape)
                break;
        }
    }
}
