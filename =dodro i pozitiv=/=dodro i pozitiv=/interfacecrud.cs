using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using _dodro_i_pozitiv_;
using System.Windows.Controls;

public class NotesCRUDService : CRUD
{
    private List<zametki> _notes;
    private readonly string _filePath;

    public NotesCRUDService(string filePath)
    {
        _filePath = filePath;
        LoadNotes();
    }

    private void LoadNotes()
    {
        _notes = FileManager.DeserializeFromFile<List<zametki>>(_filePath) ?? new List<zametki>();
    }

    private void SaveNotes()
    {
        FileManager.SerializeToFile(_filePath, _notes);
    }

    public void Create(DateTime vrema1, TextBox zametka, TextBox opicanie, ListBox vivod, List<zametki> list)
    {
        var newNote = new zametki
        {
            Vrema = vrema1,
            Zametka = zametka.Text,
            Opisanie = opicanie.Text
        };
        list.Add(newNote);
        SaveNotes();
        vivod.ItemsSource = list; // Обновление отображаемого списка заметок
    }

    public void Read(List<zametki> list, DateTime vrema1, string hh, List<zametki> list2)
    {
        list2 = list.Where(note => note.Vrema.Date == vrema1.Date).ToList();
        // Тут может потребоваться обновление UI для отображения list2
    }

    public void Update(DateTime vrema1, TextBox zametka, TextBox opicanie, ListBox vivod, List<zametki> list)
    {
        var noteToUpdate = list.FirstOrDefault(z => z.Vrema == vrema1 && z.Zametka == zametka.Text);
        if (noteToUpdate != null)
        {
            noteToUpdate.Zametka = zametka.Text;
            noteToUpdate.Opisanie = opicanie.Text;
            SaveNotes();
            vivod.ItemsSource = list; // Обновление отображаемого списка заметок
        }
    }

    public void Delete(TextBox zametka, TextBox opicanie, ListBox vivod, List<zametki> list, zametki noteToDelete)
    {
        _notes.Remove(noteToDelete);
        SaveNotes();
    }

}
