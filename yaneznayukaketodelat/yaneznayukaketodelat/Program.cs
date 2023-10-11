using System;
using System.Collections.Generic;
using System.Threading;

class Piano
{
    static Dictionary<ConsoleKey, int[]> octaves = new Dictionary<ConsoleKey, int[]>
    {
        { ConsoleKey.F1, new int[] { 261, 293, 329, 349, 392, 440, 493 } },  
        { ConsoleKey.F2, new int[] { 523, 587, 659, 698, 784, 880, 987 } }, 
        { ConsoleKey.F3, new int[] { 1046, 1175, 1318, 1397, 1568, 1760, 1976 } }  
    };

    static void Main(string[] args)
    {
        Console.WriteLine("Для игры на пианино используйте клавиши на клавиатуре.");
        Console.WriteLine("Для переключения октавы нажмите F1, F2 или F3.");
        Console.WriteLine("Для завершения программы нажмите Esc.");

        int currentOctave = 4; 
        int[] currentNotes = octaves[ConsoleKey.F1]; 

        while (true)
        {
            if (Console.KeyAvailable)
            {
                ConsoleKeyInfo keyInfo = Console.ReadKey(true);
                ConsoleKey key = keyInfo.Key;

                if (key == ConsoleKey.Escape)
                {
                    break;
                }
                else if (octaves.ContainsKey(key))
                {
                    currentOctave = int.Parse(key.ToString().Substring(1, 1)); 
                    currentNotes = octaves[key];
                    Console.WriteLine($"Переключено на октаву {currentOctave}");
                }
                else if (currentNotes != null && keyInfo.Key != ConsoleKey.F1 && keyInfo.Key != ConsoleKey.F2 && keyInfo.Key != ConsoleKey.F3)
                {
                    int noteIndex = -1;
                    switch (keyInfo.Key)
                    {
                        case ConsoleKey.A:
                            noteIndex = 0;
                            break;
                        case ConsoleKey.W:
                            noteIndex = 1;
                            break;
                        case ConsoleKey.S:
                            noteIndex = 2;
                            break;
                        case ConsoleKey.E:
                            noteIndex = 3;
                            break;
                        case ConsoleKey.D:
                            noteIndex = 4;
                            break;
                        case ConsoleKey.F:
                            noteIndex = 5;
                            break;
                        case ConsoleKey.T:
                            noteIndex = 6;
                            break;
                    }

                    if (noteIndex >= 0)
                    {
                        int frequency = currentNotes[noteIndex];
                        Console.Beep(frequency, 300); 
                }
            }
        }
    }
}
