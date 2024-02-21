using System;
using System.Collections.Generic;
using System.Linq;
using System.Windows;
using System.Windows.Controls;

namespace _dodro_i_pozitiv_
{
    public partial class MainWindow : Window
    {
        private List<zametki> _notes;
        private const string FilePath = "notes.json";
        private NotesManager _notesManager = new NotesManager("C:\\Users\\79152\\Desktop\\notes.json");

        public MainWindow()
        {
            InitializeComponent();
            LoadNotes();
            datePicker.SelectedDate = DateTime.Now;
            datePicker.SelectedDateChanged += DatePicker_SelectedDateChanged;
            // Предполагается, что datePicker, notesListBox и другие элементы уже определены в XAML
        }

        private void LoadNotes()
        {
            _notes = FileManager.DeserializeFromFile<List<zametki>>(FilePath) ?? new List<zametki>();
            UpdateNotesList();
        }

        private void SaveNotes()
        {
            FileManager.SerializeToFile(FilePath, _notes);
        }

        private void UpdateNotesList()
        {
            if (datePicker.SelectedDate.HasValue)
            {
                var selectedDate = datePicker.SelectedDate.Value;
                var filteredNotes = _notes.Where(note => note.Vrema.Date == selectedDate.Date).ToList();
                notesListBox.ItemsSource = filteredNotes;
                notesListBox.DisplayMemberPath = "Zametka"; 
            }
        }

        private void DatePicker_SelectedDateChanged(object sender, SelectionChangedEventArgs e)
        {
            UpdateNotesList();
        }

        private void AddNoteButton_Click(object sender, RoutedEventArgs e)
        {
            var addNoteWindow = new AddNoteWindow();
            if (addNoteWindow.ShowDialog() == true)
            {
                _notes.Add(addNoteWindow.NewNote);
                SaveNotes();
                UpdateNotesList();
            }
        }

        private void EditNoteButton_Click(object sender, RoutedEventArgs e)
        {
            if (notesListBox.SelectedItem is zametki selectedNote)
            {
                // Предполагается, что у AddNoteWindow есть конструктор, который принимает объект zametki
                var editNoteWindow = new AddNoteWindow(selectedNote);
                if (editNoteWindow.ShowDialog() == true)
                {
                    // Обновление заметки в менеджере заметок
                    _notesManager.Update(selectedNote, editNoteWindow.NewNote.Zametka, editNoteWindow.NewNote.Opisanie);
                    UpdateNotesList(); // Обновление списка заметок в интерфейсе пользователя
                }
            }
        }

        private void DeleteNoteButton_Click(object sender, RoutedEventArgs e)
        {
            if (notesListBox.SelectedItem is zametki selectedNote)
            {
                // Вызов метода удаления заметки из менеджера заметок
                _notesManager.Delete(selectedNote);
                UpdateNotesList(); // Обновление списка заметок в интерфейсе
            }
            else
            {
                MessageBox.Show("Выберите заметку для удаления.", "Ошибка", MessageBoxButton.OK, MessageBoxImage.Error);
            }
        }


        private void notesListBox_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            // Здесь вы можете обновить интерфейс с информацией о выбранной заметке
            if (notesListBox.SelectedItem is zametki selectedNote)
            {
                 selectedNoteTitle.Text = selectedNote.Zametka;
                 selectedNoteDescription.Text = selectedNote.Opisanie;
            }
        }

    }
}