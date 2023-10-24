using System;
using System.Collections.Generic;

namespace DailyPlanner
{
    class Note
    {
        public string Title { get; set; }
        public string Description { get; set; }
        public DateTime DueDate { get; set; }

        public override string ToString() => Title;
    }

    class Planner
    {
        private List<Note> notes;
        private int currentIndex;

        public Planner()
        {
            notes = new List<Note>
            {
                new Note { Title = "Прийти на пары", Description = "Со первой по пятую", DueDate = new DateTime(2023, 10, 09) },
                new Note { Title = "Поиграть в футбол", Description = "+вайб", DueDate = new DateTime(2023, 10, 20) },
                new Note { Title = "Погулять с другом", Description = "Опять кайф", DueDate = new DateTime(2023, 10, 11) },
                new Note { Title = "Поиграть в игры", Description = "Не поиграешь(", DueDate = new DateTime(2023, 10, 24) },
                new Note { Title = "Сделать практос", Description = "не успеешь, как обычно все дедлайны к черту", DueDate = new DateTime(2022, 10, 13) },

            };

            currentIndex = 0;
        }

        public void NextNote()
        {
            if (currentIndex < notes.Count - 1) currentIndex++;
        }

        public void PrevNote()
        {
            if (currentIndex > 0) currentIndex--;
        }

        public Note GetCurrentNote() => notes[currentIndex];

        public string GetNoteDetail()
        {
            var note = GetCurrentNote();
            return $"Название: {note.Title}\nОписание: {note.Description}\nДата выполнения: {note.DueDate:dd.MM.yyyy}";
        }
    }

    class Program
    {
        static void Main()
        {
            var planner = new Planner();

            while (true)
            {
                Console.Clear();
                Console.WriteLine($"Выбрана дата {planner.GetCurrentNote().DueDate:dd.MM.yyyy}");
                Console.WriteLine($"1. {planner.GetCurrentNote()}");
                Console.WriteLine("стрелка влево - Предыдущая заметка, стрелка вправо - Следующая заметка, Enter - Подробнее о заметке, Esc - Выход");

                var key = Console.ReadKey();
                switch (key.Key)
                {
                    case ConsoleKey.RightArrow:
                        planner.NextNote();
                        break;
                    case ConsoleKey.LeftArrow:
                        planner.PrevNote();
                        break;
                    case ConsoleKey.Enter:
                        Console.Clear();
                        Console.WriteLine(planner.GetNoteDetail());
                        Console.WriteLine("Нажмите любую клавишу, чтобы вернуться...");
                        Console.ReadKey();
                        break;
                    case ConsoleKey.Escape:
                        return;
                }
            }
        }
    }
}
