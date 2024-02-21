using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace _dodro_i_pozitiv_
{
    public class NotesManager
    {
        private List<zametki> _notesList;
        private readonly string _filePath;

        public NotesManager(string filePath)
        {
            _filePath = filePath;
            LoadNotes();
        }

        private void LoadNotes()
        {
            // Здесь будет код для загрузки заметок из файла
            _notesList = FileManager.DeserializeFromFile<List<zametki>>(_filePath) ?? new List<zametki>();
        }

        private void SaveNotes()
        {
            // Здесь будет код для сохранения заметок в файл
            FileManager.SerializeToFile(_filePath, _notesList);
        }

        // Метод для создания заметки
        public void Create(zametki note)
        {
            _notesList.Add(note);
            SaveNotes();
        }

        // Метод для чтения (получения) заметок по определенной дате
        public IEnumerable<zametki> Read(DateTime date)
        {
            return _notesList.Where(n => n.Vrema.Date == date.Date);
        }

        // Метод для обновления существующей заметки
        public void Update(zametki noteToUpdate, string newTitle, string newDescription)
        {
            var note = _notesList.FirstOrDefault(n => n == noteToUpdate);
            if (note != null)
            {
                note.Zametka = newTitle;
                note.Opisanie = newDescription;
                SaveNotes();
            }
        }

        // Метод для удаления заметки
        public void Delete(zametki noteToDelete)
        {
            _notesList.Remove(noteToDelete);
            SaveNotes();
        }
    }

}
